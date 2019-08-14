using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // join unicode
            Console.OutputEncoding = Encoding.Unicode;

            // testing
            var mas = Method<byte>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            //var mas = Method<byte>();

            // вивід типу
            Console.WriteLine($"\n\tДані про колекцію: {mas.GetType()}");
            Console.WriteLine($"\tКількість елементів: {mas.Count()}\n");

            // вивід результату
            foreach (var i in mas)
            {
                Console.WriteLine(i);
            }

            // delay
            Console.ReadKey(true);
        }

        /// <summary>
        /// Повернення колекції квадратів
        /// </summary>
        /// <typeparam name="T">тип даних</typeparam>
        /// <param name="array">масив даних</param>
        /// <returns></returns>
        private static IEnumerable<T> Method<T>(params T[] array)
        {
            // перевірка на тип - цілі числа
            int type = (int)Type.GetTypeCode(typeof(T));
            // яущо вказано, що можна лише цылий тип, то тільки цілий,
            // якщо закинути в блок try...catch ми повинні повернути якесь 
            // значення, а null - це пуста колекція, і цей результат не підходить
            // тому лише викидання виключення
            if ((5 <= type && type <= 12) == false)
            {
                throw new Exception("Невірний тип даних, необхідно вказати тип цілих чисел.");
            }

            // якщо в масиві є якісь дані
            for (int i = 0; i < array.Length; i++)
            {
                yield return (T)((dynamic)array[i] * (dynamic)array[i]);
            }

            // якщо масив пустий
            yield break;
        }
    }
}
