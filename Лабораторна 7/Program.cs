using System;
using System.Collections.Generic;

namespace lab7 
{
    class Hash_table
    {
        private int Size;
        private double loadness;
        private int Capacity;
        private Entry[] table;
        public Hash_table()
        {
            this.Size = 0;
            this.loadness = 0;
            this.Capacity = 17;
            this.table = new Entry[this.Capacity];
        }
        public void InsertEntry(Key key, Value value, Add_Hash_table addTable, ref int index)
        {
            if (this.loadness > 0.55)
                Rehashing();
            int hash_i = getHash(key);
            for (int i = hash_i; i <= this.Capacity; i++)
            {
                if (i == this.Capacity) i = 0;
                if (table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                {
                    CheckLimit(addTable, key, value, i);
                    addTable.RemovePatient(table[i].key, table[i].value);
                    table[i].value.familyDoctor = value.familyDoctor;
                    table[i].value.adress = value.adress;
                    addTable.InsertPatient(key, table[i].value);
                    break;
                }
                if (table[i].key.firstName == "DELETED" || table[i].key.firstName == null)
                {
                    Entry toCheck = findEntry(key);
                    if (toCheck.key.firstName != null)
                        continue;
                    CheckLimit(addTable, key, value, i);
                    table[i].key = key;
                    table[i].value = value;
                    index++;
                    this.Size++;
                    this.loadness = (double)this.Size / this.Capacity;
                    addTable.InsertPatient(key, value);
                    break;
                }
            }
        }
        public bool RemoveEntry(Key key, Add_Hash_table addTab)
        {
            int hash_i = getHash(key);
            for (int i = hash_i; i <= this.Capacity; i++)
            {
                if (i == this.Capacity) i = 0;
                if (table[i].key.firstName == null)
                    return false;
                if (table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                {
                    addTab.RemovePatient(table[i].key, table[i].value);
                    table[i].key.firstName = "DELETED";
                    table[i].key.lastName = null;
                    table[i].value.patientID = 0;
                    table[i].value.adress = null;
                    table[i].value.familyDoctor = null;
                    this.Size--;
                    this.loadness = (double)this.Size / this.Capacity;
                    break;
                }
            }
            return true;
        }
        
        public void Print()
        {
            if (this.Size == 0)
            {
                Console.WriteLine("Table is empty ");
                return;
            }
            for (int i = 0; i < this.Capacity; i++)
            {
                if (table[i].key.firstName != null && table[i].key.firstName != "DELETED")
                {
                    int ID = this.table[i].value.patientID;
                    string FN = this.table[i].key.firstName;
                    string LN = this.table[i].key.lastName;
                    string FDoc = this.table[i].value.familyDoctor;
                    string ADDR = this.table[i].value.adress;
                    Console.WriteLine($"[{ID}] {FN} {LN} [doc: {FDoc}] [adress: {ADDR}]");
                }
                
            }
            
        }
        private long HashCode(Key key)
        {
            long hash = 0;
            string hashable = key.firstName + key.lastName;
            for (int i = 0; i < hashable.Length; i++)
                hash = hashable[i] * i * i + hash;
            return hash;
        }
        private int getHash(Key key)
        {
            return (int)(HashCode(key) % Capacity);
        }
        private void Rehashing()
        {
            Console.WriteLine("Overloaded 55% - Rehashing");
            int PrevCap = this.Capacity;
            this.Capacity *= 2;
            Entry[] newTab = new Entry[this.Capacity];
            for (int i = 0; i < PrevCap; i++)
            {
                if (table[i].key.firstName == null || table[i].key.firstName == "DELETED")
                    continue;
                int hash_i = getHash(table[i].key);
                for (int j = hash_i; j <= this.Capacity; j++)
                {
                    if (j == this.Capacity) j = 0;
                    if (newTab[j].key.firstName == null)
                    {
                        newTab[j] = table[i];
                        break;
                    }
                }
            }
            this.loadness = (double)this.Size / this.Capacity;
            table = newTab;
            Console.WriteLine("Rehashing done");
        }
        public Entry findEntry(Key key)
        {
            Entry noNote = new Entry();
            int hash_i = getHash(key);
            for (int i = hash_i; i <= this.Capacity; i++)
            {
                if (i == this.Capacity) i = 0;
                if (table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                    return table[i];
                if (table[i].key.firstName == null)
                    break;
            }
            return noNote;
        }
        private void CheckLimit(Add_Hash_table addTab, Key key, Value value, int i)
        {
            SecEntry doctor = addTab.FindDoctor(value.familyDoctor);
            if (doctor.doctor != null && doctor.patients.Count == 5)
            {
                if (table[i].value.familyDoctor == doctor.doctor)
                    return;
                else
                    throw new Exception("This doctor already have 5 patients");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Add_Hash_table addTable = new Add_Hash_table();
            Hash_table table = new Hash_table();
            int id = 41800;
            bool exit = false;
            Console.WriteLine("Create control example? IF yes - enter 'YES' else enter 'NO')");
            string control = Console.ReadLine();
            switch (control)
            {
                case "YES":
                    Control(table, ref id, addTable);
                    break;
                case "NO":
                    break;
                default:
                    Console.WriteLine("Use commands to work");
                    break;
            }
            while (!exit)
            {
                Console.WriteLine(@"
                1. InsertPatient
                2. RemovePatient
                3. FindPatient
                4. PrintTable
                5. FindFamilyDoctorPatients
                6. Exit");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.WriteLine("Enter your name and surname, address and surname of the family doctor with whom you are going to contract. " +

                        "\nFor example:\nMaria,Nikolina / Dnipro / Lobanow");
                        string input = Console.ReadLine();
                        string result = ProcessInsertion(table, input, ref id, addTable);
                        Console.WriteLine(result);
                        break;
                    case "2":
                        Console.WriteLine("To delete a patient from table, enter his first and last name " +
                      "\nFor example:\nMaria / Nikolina");
                        string toRemove = Console.ReadLine();
                        string resultRemove = ProcessRemoving(table, toRemove, addTable);
                        Console.WriteLine(resultRemove);
                        break;
                    case "3":
                        Console.WriteLine("To find out the information of the patient, enter his name and surname" +
                       "\nFor example:\nMaria / Nikolina");
                        string toFind = Console.ReadLine();
                        string resultFind = ProcessFind(table, toFind);
                        if (resultFind != "")
                            Console.WriteLine(resultFind);
                        break;
                    case "4":
                        table.Print();
                        break;
                    case "5":
                        Console.WriteLine("Enter the name of a doctor:");
                        string doc = Console.ReadLine();
                        string resultPatients = ProcessFindFamilyDoctorPatients(addTable, doc);
                        if (resultPatients != "")
                            Console.WriteLine(resultPatients);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Use commands to work");
                        break;
                }
            }
        }
        static string ProcessInsertion(Hash_table table, string input, ref int id, Add_Hash_table addTable)
        {
            string[] arguments = input.Split(" / ");
            if (arguments.Length != 4)
                return "ERROR";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            string pAdress = CheckNames(arguments[2]);
            string doctor = CheckNames(arguments[3]);
            if (name == "ERROR" || surname == "ERROR" || pAdress == "ERROR" || doctor == "ERROR")
                return "ERROR";
            Key key = new Key
            {
                firstName = name,
                lastName = surname
            };
            Value value = new Value
            {
                patientID = id,
                adress = pAdress,
                familyDoctor = doctor
            };
            try
            {
                table.InsertEntry(key, value, addTable, ref id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try to find other doctor");
                string AvDoc = addTable.FindAvailableDoctor();
                if (AvDoc == null)
                {
                    Console.WriteLine("All doctors are busy now. Enter the name of another doctor");
                    value.familyDoctor = DocName(addTable, key, table);
                }
                else
                {
                    Console.WriteLine($"You can choose doctor: {AvDoc}. Enter 'YES'  or  'NO'");
                    while (true)
                    {
                        string command = Console.ReadLine();
                        if (command == "YES")
                        {
                            value.familyDoctor = AvDoc;
                            break;
                        }
                        else if (command == "NO")
                        {
                            Console.WriteLine("Enter the name of another doctor.");
                            value.familyDoctor = DocName(addTable, key, table);
                            break;
                        }
                        else
                            Console.WriteLine("Use commands to work");
                    }
                }
                table.InsertEntry(key, value, addTable, ref id);
            }
            return "Patient was inserted ";
        }
        static string ProcessRemoving(Hash_table table, string input, Add_Hash_table addTable)
        {
            string[] arguments = input.Split(" / ");
            if (arguments.Length != 2)
                return "ERROR";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            if (name == "ERROR" || surname == "ERROR")
                return "ERROR";
            Key key = new Key
            {
                firstName = name,
                lastName = surname
            };
            bool deleted = table.RemoveEntry(key, addTable);
            if (deleted)
                return "Removed";
            else
                return "Patient wasn`t found";
        }
        static string ProcessFind(Hash_table table, string input)
        {
            string[] arguments = input.Split(" / ");
            if (arguments.Length != 2)
                return "ERROR";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            if (name == "ERROR" || surname == "ERROR")
                return "ERROR";
            Key key = new Key
            {
                firstName = name,
                lastName = surname
            };
            Entry found = table.findEntry(key);
            if (found.key.firstName == null)
                return "Patient wasn`t found";
            else
            {
                Console.WriteLine("Patient was found:");
                Console.WriteLine("[{0}] {1} {2} [doc.{3}][adress:{4}]",
                found.value.patientID, found.key.firstName, found.key.lastName,
                found.value.familyDoctor, found.value.adress);
            }
            return "";
        }
        static string ProcessFindFamilyDoctorPatients(Add_Hash_table table, string doc)
        {
            string doctor = CheckNames(doc);
            if (doctor == "ERROR")
                return "ERROR";
            SecEntry patients = table.FindDoctor(doctor);
            if (patients.doctor == null)
                return "Doctor wasn`t found";
            if (patients.patients.Count == 0)
                return $"Doc.{doctor} have no patients";
            else
            {
                Console.WriteLine($"Doc.{doctor}:");
                Patient[] array = new Patient[patients.patients.Count];
                patients.patients.CopyTo(array);
                for (int j = 0; j < array.Length; j++)
                {
                    string fn = array[j].firstName;
                    string ln = array[j].lastName;
                    string adress = array[j].adress;
                    int id = array[j].patientID;
                    Console.WriteLine($"Patient№{j + 1}: [{id}] {fn} {ln} [{adress}]");
                }
            }
            return "";
        }
        static void Control(Hash_table table, ref int id, Add_Hash_table addTable)
        {
            string[] names = new string[]{
                "Maria", "Gala", "Andriy", "Olga", "Dmytro", "Olga", "Serg", "Stepan", "Ivan", "Tamara"};
            string[] surnames = new string[]{
                "Nikolina", "Logvina", "Makovel", "Troyan", "Koshevar", "Lodochnik", "Teplakov", "Vilvar", "Logvinenko", "Rasquta"};
            string[] adress = new string[]{
                "Dnipro", "Kyiv", "Dnipro","Odessa", "Lviv", "Kyiv", "Lviv","Odessa", "Dnipro", "Kharkiw"};
            string[] doctors = new string[]{
                "Lobanow", "Wariwoda", "Gakpentar"};
            Key key = new Key();
            Value value = new Value();
            for (int i = 0; i < 10; i++)
            {
                key.firstName = names[i];
                key.lastName = surnames[i];
                value.patientID = id;
                value.adress = adress[i];
                if (i < 4)
                    value.familyDoctor = doctors[0];
                else if (i < 7)
                    value.familyDoctor = doctors[1];
                else
                    value.familyDoctor = doctors[2];
                table.InsertEntry(key, value, addTable, ref id);
            }
            table.Print();
            
            Console.WriteLine("Insert\nVolodimir / Sevlak / Odessa / Nedir");
            table.InsertEntry(new Key { firstName = "Volodimir", lastName = "Sevlak" },
                 new Value { patientID = id, familyDoctor = "Nedir", adress = "Odessa" }, addTable, ref id);
            Console.WriteLine("Patient was inserted");
           
            Console.WriteLine("Insert\nIvan / Archan / Lviv / Wariwoda");
            SecEntry doc = addTable.FindDoctor("Wariwoda");
            try
            {
                table.InsertEntry(new Key { firstName = "Ivan", lastName = "Archan" },
                 new Value { patientID = id, familyDoctor = "Wariwoda", adress = "Lviv" }, addTable, ref id);
                Console.WriteLine("Patient was inserted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try to find other doctor");
                string AvDoc = addTable.FindAvailableDoctor();
                Console.WriteLine($"You can choose doctor: {AvDoc}. Enter 'YES'  or  'NO'");
                Console.WriteLine("YES");
                table.InsertEntry(new Key { firstName = "Ivan", lastName = "Archan" },
                 new Value { patientID = id, familyDoctor = AvDoc, adress = "Lviv" }, addTable, ref id);
                Console.WriteLine("Patient was inserted ");
            }
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nFind\nIvan / Archan");
            Console.WriteLine(ProcessFind(table, "Ivan / Archan"));
            Console.WriteLine("Remove\nIvan / Archan");
            Console.WriteLine(ProcessRemoving(table, "Ivan / Archan", addTable));
            Console.WriteLine("\nFind\nIvan / Archan");
            Console.WriteLine(ProcessFind(table, "Ivan / Archan"));
            Console.WriteLine("\nPrint");
            table.Print();
            Console.WriteLine("\nFindFamilyDoctorPatients\nWariwoda");
            Console.WriteLine(ProcessFindFamilyDoctorPatients(addTable, "Wariwoda"));
        }
        static string CheckNames(string input)
        {
            string correct = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                    return "ERROR";
                if (i == 0)
                    correct += char.ToUpper(input[i]);
                else
                    correct += char.ToLower(input[i]);
            }
            return correct;
        }
        static string DocName(Add_Hash_table addTable, Key key, Hash_table table)
        {
            while (true)
            {
                string inputDoc = Console.ReadLine();
                string newDoc = CheckNames(inputDoc);
                if (newDoc == "ERROR")
                {
                    Console.WriteLine("Incorrect Input");
                    continue;
                }
                SecEntry doc = addTable.FindDoctor(newDoc);
                if (doc.doctor != null && doc.patients.Count == 5)
                {
                    Entry entity = table.findEntry(key);
                    if (entity.value.familyDoctor == newDoc)
                        return newDoc;
                    Console.WriteLine("All doctors are busy now. Enter the name of another doctor");
                    continue;
                }
                else
                    return newDoc;
            }
        }
    }
    struct Key
    {
        public string firstName;
        public string lastName;
    }
    struct Value
    {
        public int patientID;
        public string familyDoctor;
        public string adress;
    }
    struct Entry
    {
        public Key key;
        public Value value;
    }
    struct Patient
    {
        public int patientID;
        public string firstName;
        public string lastName;
        public string adress;
    }
    struct SecEntry
    {
        public List<Patient> patients;
        public string doctor;
    }
    class Add_Hash_table
    {
        private int _size;
        private double _loadness;
        private int _capacity;
        private SecEntry[] table;
        public Add_Hash_table()
        {
            this._capacity = 17;
            this._loadness = 0;
            this._size = 0;
            this.table = new SecEntry[this._capacity];
        }
        public void InsertPatient(Key key, Value value)
        {
            if (this._loadness > 0.55)
                Rehashing();
            int hash_i = getHash(value.familyDoctor);
            SecEntry doctor = FindDoctor(value.familyDoctor);
            bool DoctorIs = doctor.doctor != null;
            Patient patient = new Patient
            {
                patientID = value.patientID,
                firstName = key.firstName,
                lastName = key.lastName,
                adress = value.adress
            };
            for (int i = hash_i; i <= this._capacity; i++)
            {
                if (i == this._capacity) i = 0;
                if (DoctorIs)
                {
                    if (table[i].doctor == value.familyDoctor)
                    {
                        table[i].patients.Add(patient);
                        break;
                    }
                }
                else
                {
                    if (table[i].doctor == null)
                    {
                        table[i].doctor = value.familyDoctor;
                        table[i].patients = new List<Patient>();
                        table[i].patients.Add(patient);
                        this._size++;
                        this._loadness = (double)this._size / this._capacity;
                        break;
                    }
                }
            }
        }
        public void RemovePatient(Key key, Value value)
        {
            Patient patient = new Patient
            {
                patientID = value.patientID,
                firstName = key.firstName,
                lastName = key.lastName,
                adress = value.adress
            };
            int hash_index = getHash(value.familyDoctor);
            for (int i = hash_index; i <= this._capacity; i++)
            {
                if (i == this._capacity) i = 0;
                if (table[i].doctor == value.familyDoctor)
                {
                    table[i].patients.Remove(patient);
                    break;
                }
            }
        }
        private long HashCode(string key)
        {
            long hash = 0;
            for (int i = 0; i < key.Length; i++)
                hash = key[i] * i  * i + hash;
            return hash;
        }
        private int getHash(string key)
        {
            return (int)(HashCode(key) % this._capacity);
        }
        public SecEntry FindDoctor(string doctor)
        {
            SecEntry nullEntry = new SecEntry();
            int hash_index = getHash(doctor);
            for (int i = hash_index; i <= this._capacity; i++)
            {
                if (i == this._capacity) i = 0;
                if (table[i].doctor == doctor)
                    return table[i];
                if (table[i].doctor == null)
                    break;
            }
            return nullEntry;
        }
        public string FindAvailableDoctor()
        {
            for (int i = 0; i < this._capacity; i++)
            {
                if (table[i].doctor != null && table[i].patients.Count < 5)
                    return table[i].doctor;
            }
            return null;
        }
        private void Rehashing()
        {
            int PrevCap = this._capacity;
            this._capacity *= 2;
            SecEntry[] newTab = new SecEntry[this._capacity];
            for (int i = 0; i < PrevCap; i++)
            {
                if (table[i].doctor == null)
                    continue;
                int hash_index = getHash(table[i].doctor);
                for (int j = hash_index; j <= this._capacity; j++)
                {
                    if (j == this._capacity) j = 0;
                    if (newTab[j].doctor == null)
                    {
                        newTab[j] = table[i];
                        break;
                    }
                }
            }
            this._loadness = (double)this._size / this._capacity;
            table = newTab;
        }
        
    }
}
