using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LesApp2.People;

namespace LesApp2
{
    class Program
    {
        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // створення колекції
            MyList citizens = new MyList();

            #region Занесення даних в базу
            // лічильник для ID
            int countID = 0;

            // занесення по n громадян кожного типу
            for (int i = 0; i < 10; i++)
            {
                citizens.Add(new Retiree() { ID = countID++, FullName = typeof(Retiree).Name + " " + i });
                citizens.Add(new Employee() { ID = countID++, FullName = typeof(Employee).Name + " " + i });
                citizens.Add(new Student() { ID = countID++, FullName = typeof(Student).Name + " " + i });
            }

            // додавання групи
            citizens.AddRange(
                new Retiree() { ID = countID += 27, FullName = typeof(Retiree).Name + " " + countID },
                new Retiree() { ID = ++countID, FullName = typeof(Retiree).Name + " " + countID },
                new Retiree() { ID = ++countID, FullName = typeof(Retiree).Name + " " + countID },
                new Employee() { ID = ++countID, FullName = typeof(Employee).Name + " " + countID },
                new Employee() { ID = ++countID, FullName = typeof(Employee).Name + " " + countID },
                new Employee() { ID = ++countID, FullName = typeof(Employee).Name + " " + countID },
                new Student() { ID = ++countID, FullName = typeof(Student).Name + " " + countID },
                new Student() { ID = ++countID, FullName = typeof(Student).Name + " " + countID },
                new Student() { ID = ++countID, FullName = typeof(Student).Name + " " + countID });

            Console.WriteLine();
            // виведення
            foreach (var j in citizens)
            {
                Console.WriteLine(j.ToString());
            }
            Console.WriteLine(citizens.ToString());
            #endregion

            #region Спроба додати елемент із наявним ID
            try
            {
                citizens.Add(new Retiree() { ID = 10, FullName = typeof(Retiree).Name + " " + 10 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion

            #region Доступ через індексатор
            // аналіз, спроба замінити елемент в якісь групі іншим типом
            try
            {
                citizens[5] = new Student() { ID = ++countID, FullName = typeof(Student).Name + " " + countID };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // аналіз, спроба ввести з уже наявним ID
            try
            {
                citizens[5] = new Retiree() { ID = 12, FullName = typeof(Retiree).Name + " " + 100 };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // поміняємо Employee 1 ID 4
            // на Employee 
            try
            {
                citizens[14] = new Employee() { ID = 70, FullName = typeof(Employee).Name + " " + 150 };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // звичайне виведення
            Console.WriteLine("\n\t" + citizens[14].ToString());

            #endregion

            #region Видалення
            // по індексу
            citizens.RemoveAt(14);
            // по самому елементу
            citizens.Remove(new Student() { ID = 2, FullName = typeof(Student).Name + " " + 0 });
            // витягування (перед-передостанній елемент)
            Console.WriteLine("\n\t" + citizens.RemoveExtend(citizens.Count - 3).ToString());
            #endregion

            #region Наявність елемента
            Console.WriteLine($"\n\tПеревірка на наявність: " + new Retiree() { ID = 9, FullName = typeof(Retiree).Name + " " + 3 }.ToString());
            var query = citizens.ContainsAnon(new Retiree() { ID = 9, FullName = typeof(Retiree).Name + " " + 3 });
            Console.WriteLine($"\tНаявність: {query.Contain}, позиція {query.Number};");
            Console.WriteLine($"\n\tПеревірка на наявність: " + new Retiree() { ID = 9, FullName = typeof(Retiree).Name + " " + 5 }.ToString());
            Console.WriteLine($"\tНаявність: {citizens.Contains<bool>(new Retiree() { ID = 9, FullName = typeof(Retiree).Name + " " + 5 })}," +
                $" позиція {citizens.Contains<int>(new Retiree() { ID = 9, FullName = typeof(Retiree).Name + " " + 5 })};");
            #endregion

            #region Повернення останього елемента
            Console.WriteLine($"\n\tПовернення останього елемента:");
            query = citizens.ReturnLastAnon();
            Console.WriteLine($"\tІндекс останього елемента: {query.Number},\n\tсам елемент: {query.Element.ToString()};");
            #endregion

            #region Вставка елемента
            // вставка в середину своєї групи
            Console.WriteLine($"\n\tВставка в позицію 15: " + new Employee() { ID = 200, FullName = typeof(Employee).Name + " " + 200 }.ToString());
            citizens.Insert(15, new Employee() { ID = 200, FullName = typeof(Employee).Name + " " + 200 });
            // вставка не в свою групу - перед
            Console.WriteLine($"\n\tВставка в позицію 30: " + new Employee() { ID = 300, FullName = typeof(Employee).Name + " " + 300 }.ToString());
            citizens.Insert(5, new Employee() { ID = 300, FullName = typeof(Employee).Name + " " + 300 });
            // вставка не в свою групу - після
            Console.WriteLine($"\n\tВставка в позицію 30: " + new Employee() { ID = 250, FullName = typeof(Employee).Name + " " + 250 }.ToString());
            citizens.Insert(30, new Employee() { ID = 250, FullName = typeof(Employee).Name + " " + 250 });

            #endregion


#if true
            Console.WriteLine();
            // повторне виведення
            foreach (var j in citizens)
            {
                Console.WriteLine(j.ToString());
            }
            Console.WriteLine(citizens.ToString()); 
#endif

            // repeat
            DoExitOrRepeat();
        }

        /// <summary>
        /// Метод виходу або повторення методу Main()
        /// </summary>
        static void DoExitOrRepeat()
        {
            Console.WriteLine("\n\nСпробувати ще раз: [т, н]");
            Console.Write("\t");
            var button = Console.ReadKey(true);

            if ((button.KeyChar.ToString().ToLower() == "т") ||
                (button.KeyChar.ToString().ToLower() == "n")) // можливо забули переключити розкладку клавіатури
            {
                Console.Clear();
                Main();
                // без використання рекурсії
                //Process.Start(Assembly.GetExecutingAssembly().Location);
                //Environment.Exit(0);
            }
            else
            {
                // закриває консоль
                Environment.Exit(0);
            }
        }
    }
}
