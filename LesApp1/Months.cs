//#define Read    // керує "викиданням" виключень

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
{
    /// <summary>
    /// Місяці
    /// </summary>
    class Months : IList<Month>, IEnumerable<Month>, IEnumerator<Month>
    {
        /// <summary>
        /// Рік для якого розраховуватимуться параметри місяця
        /// </summary>
        private int year;
        // 13 місяців, бо перший - заглушка
        /// <summary>
        /// Масив місяців
        /// </summary>
        private Month[] array = new Month[13];

        /// <summary>
        /// Створення
        /// </summary>
        /// <param name="year"></param>
        public Months(int year)
        {
            // установка року лише при ініціалізації
            this.year = year;

            // заглушка
            array[0] = new Month(new DateTimeFormatInfo().GetMonthName(13), 0, 0);

            // формування самих місяців в масиві
            for (int i = 1; i < array.Length; i++)
            {
                array[i] = new Month(new DateTimeFormatInfo().GetMonthName(i),
                    i, DateTime.DaysInMonth(year, i));
            }
        }

        /// <summary>
        /// Кількість місяців
        /// </summary>
        public int Count=> 12;

        /// <summary>
        /// Індексатор
        /// </summary>
        /// <param name="index">індекс</param>
        /// <returns></returns>
        public Month this[int index]
        {
            get
            {
                if (0 < index && index < 13)
                {
                    return array[index];
                }
                else
                {
                    throw new Exception("Вихід за межі масиву");
                }
            }
            set
            {
                // можна кинути виняток
#if Read
                throw new Exception("Дана колекція тільки для читання."); 
#endif
            }
        }

        /// <summary>
        /// Перевірка чи дана колекція лише для читання
        /// </summary>
        public bool IsReadOnly
            => true;

        /// <summary>
        /// Ітератор/енумератор
        /// </summary>
        int position = 0;   // 0, так як місяці починаються з 1

        /// <summary>
        /// Повернення поточного значення - generic
        /// </summary>
        public Month Current
            => array[position];

        /// <summary>
        /// Повернення поточного значення
        /// </summary>
        object IEnumerator.Current
            => Current;

        /// <summary>
        /// Додавання елементів заборонено, колекція тільки для читання
        /// </summary>
        /// <param name="item"></param>
        public void Add(Month item)
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання."); 
#endif
        }

        /// <summary>
        /// Очищення елементів заборонено, колекція тільки для читання
        /// </summary>
        /// <param name="item"></param>
        public void Clear()
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання.");
#endif
        }

        /// <summary>
        /// Повернення нумератора - generic
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Month> GetEnumerator()
            => this as IEnumerator<Month>;

        /// <summary>
        /// Повернення нумератора
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Пошук індекса по елементу
        /// </summary>
        /// <param name="item">інедекс</param>
        /// <returns></returns>
        public int IndexOf(Month item)
        {
            // Шукаємо елемент
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(item))
                {
                    return i;
                }
            }

            // якщо нічого не знайдено
            return -1;
        }

        /// <summary>
        /// Перевірка наявності елемента в колекції
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Contains(Month item)
        {
            // пробуємо знайти індекс елемента
            int index = IndexOf(item);

            if (index != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Копіювання всіх елементів починаючи із заданого індекса
        /// </summary>
        /// <param name="array">масив в який треба скопіювати</param>
        /// <param name="arrayIndex">поча</param>
        public void CopyTo(Month[] array, int arrayIndex)
            => Array.Copy(this.array, array, this.array.Length - arrayIndex);

        /// <summary>
        /// Вставка елемента
        /// </summary>
        /// <param name="index">індекс куди вставити</param>
        /// <param name="item">елемент</param>
        public void Insert(int index, Month item)
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання.");
#endif
        }

        /// <summary>
        /// Видалення тільки першого елемента по вказаному значенню
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Remove(Month item)
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання.");
#endif

            return false;
        }

        /// <summary>
        /// Видалення елемента по індексу
        /// </summary>
        /// <param name="index">індекс</param>
        public void RemoveAt(int index)
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання.");
#endif
        }

        /// <summary>
        /// Звільнення пам'яті
        /// </summary>
        public void Dispose()
        {
            // можна кинути виняток
#if Read
            throw new Exception("Дана колекція тільки для читання.");
#endif
        }

        /// <summary>
        /// Крокування по масиву
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (position++ < array.Length - 1)
            {
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }

        /// <summary>
        /// Скидання (лічильника) ітератора
        /// </summary>
        public void Reset()
            => position = 0;

        /// <summary>
        /// Вивід масиву місяців, які містять вказану кількість днів
        /// </summary>
        /// <param name="days">необхідна кількість днів в місяці</param>
        /// <returns></returns>
        public Month[] GetMountsByDays(int days)
            => array.Where(t => t.Days == days)
            .Select(t => t)
            .ToArray();
    }
}
