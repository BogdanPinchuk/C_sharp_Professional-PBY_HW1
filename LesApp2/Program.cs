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

            var kortej = (5, "Bohdan");

            Console.WriteLine(kortej.GetType());

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
