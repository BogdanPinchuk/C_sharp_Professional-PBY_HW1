using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
{
    class Program
    {
        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // рік
            int year = 1992;

            // створення колекції місяців
            Months listMonth = new Months(year);

            // виведення інформації по місяцях
            Show($"\n\tМісяці {year} року:\n");
            // виведення міясців
            foreach (var item in listMonth)
            {
                Console.WriteLine("\t" + item.ToString());
            }

            // створення свого місяця
            Month myMont = new Month(new DateTimeFormatInfo().GetMonthName(6), 6, 30);

            // пошук його в колекції
            Show("\n\tЧи наявний серед місяців наступний:");
            Console.WriteLine("\t" + myMont.ToString());
            Console.WriteLine("\tВідповідь: " + listMonth.Contains(myMont));

            // Переприсвоюємо
            myMont = new Month(new DateTimeFormatInfo().GetMonthName(12), 7, 30);

            // пошук його в колекції
            Show("\n\tЧи наявний серед місяців наступний:");
            Console.WriteLine("\t" + myMont.ToString());
            Console.WriteLine("\tВідповідь: " + listMonth.Contains(myMont));

            // вибір місяця по номеру
            Show("\n\tВибір місяця за номером 2:");
            Console.WriteLine("\t" + listMonth[2]);

            // вибір місяців по кільксоті днів
            Show("\n\tВибір місяців які мають 30 днів:");
            List<Month> filterMonth = listMonth.GetMountsByDays(30).ToList<Month>();
            // виведення міясців
            foreach (var item in filterMonth)
            {
                Console.WriteLine("\t" + item.ToString());
            }

            // вибір місяців по кільксоті днів
            Show("\n\tВибір місяців які мають 31 день:");
            filterMonth = listMonth.GetMountsByDays(31).ToList<Month>();
            // виведення міясців
            foreach (var item in filterMonth)
            {
                Console.WriteLine("\t" + item.ToString());
            }

            // repeat
            DoExitOrRepeat();
        }

        /// <summary>
        /// Виведення заголовків зеленим кольором
        /// </summary>
        /// <param name="s"></param>
        private static void Show(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ResetColor();
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
