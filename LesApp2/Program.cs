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

#if false
            // створення колекції
            MyList citizens = new MyList();

            // лічильник для ID
            int countID = 0;

            // занесення по 5 громадян кожного типу
            for (int i = 0; i < 5; i++)
            {
                citizens.Add(new Retiree() { ID = countID++, FullName = typeof(Retiree).Name + " " + i });
                citizens.Add(new Employee() { ID = countID++, FullName = typeof(Employee).Name + " " + i });
                citizens.Add(new Student() { ID = countID++, FullName = typeof(Student).Name + " " + i });

                Console.WriteLine();
                // виведення
                foreach (var j in citizens)
                {
                    Console.WriteLine(j.ToString());
                }
            } 
#endif
#if true
            Citizen[] citizen = new Retiree[]
                {
                new Retiree()
                {
                    ID = 1, FullName = "Retiree 1"
                },
                new Retiree()
                {
                    ID = 2, FullName = "Retiree 2"
                },
                new Retiree()
                {
                    ID = 3, FullName = "Retiree 3"
                },
                new Retiree()
                {
                    ID = 4, FullName = "Retiree 4"
                },
                };
            //Citizen[] citizen = retiree;
            Retiree[] retiree1 = (Retiree[])citizen;
            //foreach (var i in retiree)
            //{
            //    Console.WriteLine(i.ToString());
            //}
            Console.WriteLine(retiree1[0].GetType());
            foreach (var i in citizen)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine(citizen[0].GetType());
            foreach (var i in retiree1)
            {
                Console.WriteLine(i.ToString());
            } 
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
