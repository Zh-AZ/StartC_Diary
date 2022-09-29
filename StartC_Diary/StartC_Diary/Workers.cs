using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StartC_Diary
{
    struct Workers
    {
        /// <summary>
        /// Создание файла и запись данных
        /// </summary>
        public void FileAppend()
        {
            string patch = @"d:\Employees.txt";
            using (FileStream file = new FileStream(patch, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    char key = '2';
                    do
                    {
                        string ID = String.Empty;
                        Console.Write("Введите ID: ");
                        ID += $"{Console.ReadLine()}#";

                        DateTime now = DateTime.Now;
                        Console.WriteLine($"Время записи: {now.ToString("g")}");
                        ID += $"{now.ToString("g")}#";

                        string FullName = String.Empty;
                        Console.Write("Введите Ф.И.О.: ");
                        ID += $"{Console.ReadLine()}#";

                        Console.Write("Введите возраст: ");
                        string age = Console.ReadLine();
                        ID += $"{age}#";

                        Console.Write("Введите рост: ");
                        string growth = Console.ReadLine();
                        ID += $"{growth}#";

                        Console.Write("Введите дату рождения: ");
                        string birthsday = Console.ReadLine();
                        ID += $"{birthsday}#";

                        Console.Write("Введите место рождения: ");
                        string place = Console.ReadLine();
                        ID += $"{place}";

                        sw.WriteLine(ID);
                        Array.Resize<Employee>(ref this.employees, this.employees.Length * 10);
                    } while (key != '2');
                }
            }
        }
        
        /// <summary>
        /// Выбор методов 
        /// </summary>
        public void ChooseFile()
        {
            if (File.Exists(patch) == false) Console.WriteLine("ФАЙЛ ЕЩЕ НЕ СОЗДАН! ЧТОБЫ СОЗДАТЬ НАЧНИТЕ ЗАПОЛНЯТЬ ДАННЫЕ!\n");
            string help_1 = "Для того чтобы заполнить данные введите > 2\nДля того чтобы читать введите > 1";
            string help_2 = help_1 + "\nПоиск по диапазону дат > 3\nДля сортировки > 4\nДля удаления или редактировании > 5";
            string help_3 = help_2 + "\nДля списка команд введите > h\nЧтобы закрыть введите 'n'";
            Console.WriteLine(help_3);
            while (true)
            {
                Console.WriteLine("\nОжидание команды: ");
                char hide = Console.ReadKey(true).KeyChar;
                string wrote = Console.ReadLine();
                if (hide == '2') FileAppend();
                else if (hide == '1')
                {
                    Load();
                    PrintToConsole();
                }
                else if (hide == '3') LookingForTime();
                else if (hide == '4') Sort();
                else if (hide == '5') PrintOneWorker();
                else if (hide == 'n')
                {
                    Console.WriteLine("Вы закрыли файл 'Сотрудники'");
                    break;
                }
                else if (hide == 'h') Console.WriteLine(help_3);
                else Console.WriteLine("Неверная команда(!)\n");
            }
            Console.ReadKey();
        }
        
        private Employee[] employees;
        private List<string> diaryses = new List<string>();
        private List<Employee> emplist = new List<Employee>();
        private string patch;
        int index;
        
        /// <summary>
        /// Конструктор для присваивания значений 
        /// </summary>
        /// <param name="Patch"></param>
        public Workers(string Patch)
        {
            this.patch = Patch;
            this.index = 0;
            this.employees = new Employee[6];
            if (File.Exists(patch) == true)
            {
                diaryses = File.ReadAllLines(patch).ToList();
            }
            for (int i = 0; i < employees.Length; i++)
            {
                emplist.Add(employees[i]);
            }
        }
        
        /// <summary>
        /// Увеличения пространства для хранения в массиве сотрудников
        /// </summary>
        /// <param name="Flag"></param>
        public void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.employees, this.employees.Length * 10);
            }
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        public void Add(Employee ConcreteWorkers)
        {
            Array.Resize<Employee>(ref this.employees, this.employees.Length + 1);
            bool exist = employees.Contains(ConcreteWorkers);
            if (exist == false)
            {
                this.Resize(index >= this.employees.Length);
                this.Resize(index >= this.emplist.Count);
                this.employees[index] = ConcreteWorkers;
                index++;
            }
            for (int k = 0 + 1; k < employees.Length; k++)
            {
                if (employees[k].id == 0)
                {
                    DeleteEmployee(k);
                    k--;
                }
            }
        }

        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintToConsole()
        {
            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(this.employees[i].Print());
                if (employees[i].id == 0)
                {
                    DeleteEmployee(i);
                    i--;
                }
            }
        }
        
        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        public void Load()
        {
            if (File.Exists(patch) == true)
            {
                using (StreamReader sr = new StreamReader(this.patch))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] args = sr.ReadLine().Split('#');
                        Add(new Employee(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), Convert.ToDateTime(args[5]), args[6]));
                    }
                }
            }
            else Console.WriteLine("Файл еще не создан!");
        }

        /// <summary>
        /// Поиск в диапазоне дат
        /// </summary>
        public void LookingForTime()
        {
            Console.WriteLine("Введите дату в формате (день - месяц - год)");
            Console.Write("\nОт: ");
            string beforedate = Console.ReadLine();
            DateTime beforeDate = DateTime.Parse(beforedate);
            Console.Write("До: ");
            string afterdate = Console.ReadLine();
            DateTime afterDate = DateTime.Parse(afterdate);
            bool boole = false;
            for (int i = 0; i < this.employees.Length; i++)
            {
                if (beforeDate <= employees[i].date && employees[i].date <= afterDate.AddDays(1))
                {
                    Console.WriteLine(employees[i].Print());
                    boole = true;
                }
            }
            if (boole == false)
            {
                Console.WriteLine("\nУбедитесь что диапазон дат был правильным!");
            }
        }

        /// <summary>
        /// Удаление или редактирование  
        /// </summary>
        public void PrintOneWorker()
        {
           
            Console.WriteLine("\nЧтобы выйти назад оставьте строку пустой");
            for ( ; ; )
            {
                Load();
                Console.WriteLine("\nВведите ID сотрудника: ");
                string work = Console.ReadLine();
                if (work == String.Empty)
                {
                    break;
                }
                int worker = int.Parse(work);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                for (int i = 0; i < employees.Length; i++)
                {
                    if (worker == employees[i].id)
                    {
                        Console.WriteLine(employees[i].Print());
                        Console.WriteLine("\nЧтобы удалить введите 1");
                        Console.WriteLine("Чтобы изменить введите 2\n");
                        char delete = Console.ReadKey(true).KeyChar;
                        if (delete == '1')
                        {
                            for (int j = 0; j < employees.Length; j++)
                            {
                                if (j == i)
                                {
                                    diaryses = File.ReadAllLines(patch).ToList();
                                    DeleteEmployee(i);
                                    diaryses.RemoveAt(j);
                                    Console.WriteLine("Удалено");
                                    File.WriteAllLines(patch, diaryses);
                                }
                            }
                        }
                        else if (delete == '2')
                        {

                            for (int j = 0; j < employees.Length; j++)
                            {
                                if (j == i)
                                {
                                    FileAppend();                                         
                                    diaryses = File.ReadAllLines(patch).ToList();
                                    string[] readText = File.ReadAllLines(patch);         
                                    diaryses[j] = String.Format(readText[^1]);            
                                    diaryses.RemoveAt(diaryses.Count - 1);
                                    File.WriteAllLines(patch, diaryses);                  
                                    Load();                                               

                                    for (int r = j; r < employees.Length - 1; r++)       
                                    {
                                        employees[r] = employees[r + 1];                 
                                        employees[r] = employees[^1];                    
                                    }
                                    Array.Resize(ref employees, employees.Length - 1);   
                                    Array.Resize<Employee>(ref this.employees, this.employees.Length * 10);
                                    for (int r = 0; r < emplist.Count - 1; r++)
                                    {
                                        for (int q = r + 1; q < emplist.Count - 1; q++)
                                        {
                                            if (emplist[r].id == emplist[q].id)
                                            {
                                                emplist.RemoveAt(q);
                                                q--;
                                            }
                                        }
                                    }
                                    for (int t = 0; t < emplist.Count; t++)
                                    {
                                        if (emplist[t].id == 0)
                                        {
                                            emplist.RemoveAt(t);
                                        }
                                        employees = emplist.ToArray();
                                    }
                                    for (int k = 0 + 1; k < employees.Length; k++)
                                    {
                                        if (employees[k].id == 0)
                                        {
                                            DeleteEmployee(k);
                                            k--;
                                        }
                                    }
                                    Console.WriteLine("Изменено");
                                    Load();
                                }
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="n"></param>
        public void DeleteEmployee(int n)
        {
            for (int i = n + 1; i < employees.Length; i++)
            {
                employees[i - 1] = employees[i];
            }
            Array.Resize(ref employees, employees.Length - 1);
        }
        
        /// <summary>
        /// Сортировка по определенным параметрам
        /// </summary>
        public void Sort()
        {
            string writeLine_1 = "\n'1' Для сортировки по ID";
            string writeLine_2 = writeLine_1 + "\n'2' Для сортировки по времени записи";
            string writeLine_3 = writeLine_2 + "\n'3' Для сортировки по Ф.И.О";
            string writeLine_4 = writeLine_3 + "\n'4' Для сотрировки по возрасту";
            string writeLine_5 = writeLine_4 + "\n'5' Для сортировки по росту";
            string writeLine_6 = writeLine_5 + "\n'6' Для сортировки по дате рождения";
            string writeLine_7 = writeLine_6 + "\n'7' Для сортировки по месту рождения";
            string exit = writeLine_7 + "\nПропустите строку для выхода назад";
            Console.WriteLine(exit);
            char plus = '+';
            char minus = '-';
            for ( ; ; )
            {
                char chars = Console.ReadKey(true).KeyChar;
                string written = Console.ReadLine();
                if (chars == '1')
                {
                    Console.WriteLine("Сортировка по ID");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.id);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.id);
                        Console.WriteLine("По убыванию (-)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '2')
                {
                    Console.WriteLine("Сортровка по времени записи");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.date);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.date);
                        Console.WriteLine("По убыванию (+)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '3')
                {
                    Console.WriteLine("Сортировка по Ф.И.О");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.fullName);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.fullName);
                        Console.WriteLine("По убыванию (+)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '4')
                {
                    Console.WriteLine("Сортировка по возрасту");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.age);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.age);
                        Console.WriteLine("По убыванию (-)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '5')
                {
                    Console.WriteLine("Сортировка по росту");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.growth);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.growth);
                        Console.WriteLine("По убыванию (-)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '6')
                {
                    Console.WriteLine("Сортировка по дате рождения");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.birthsday);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.birthsday);
                        Console.WriteLine("По убыванию (-)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (chars == '7')
                {
                    Console.WriteLine("Сортировка по месту рождения");
                    Console.WriteLine("По возрастанию '+'");
                    Console.WriteLine("По убыванию '-'");
                    char chare = Console.ReadKey(true).KeyChar;
                    string writtens = Console.ReadLine();
                    if (chare == plus)
                    {
                        var sortedPlus = employees.OrderBy(e => e.place);
                        Console.WriteLine("По возрастанию (+)");
                        foreach (var i in sortedPlus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (chare == minus)
                    {
                        var sortedMinus = employees.OrderByDescending(e => e.place);
                        Console.WriteLine("По убыванию (-)");
                        foreach (var i in sortedMinus)
                        {
                            Console.WriteLine(i.Print());
                        }
                    }
                    else if (writtens == String.Empty)
                    {
                        Console.WriteLine("Назад");
                        break;
                    }
                }
                else if (written == String.Empty)
                {
                    Console.WriteLine("Назад");
                    break;
                }
                else
                {
                    Console.WriteLine("Неверно");
                }
                Console.WriteLine(exit);
            }
        }
    } 
}


