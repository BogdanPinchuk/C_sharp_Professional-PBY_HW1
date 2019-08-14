using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LesApp2.People;

namespace LesApp2
{
    /// <summary>
    /// Непараметризована колекція
    /// </summary>
    class MyList : IList<Citizen>, IEnumerable<Citizen>, IEnumerator<Citizen>
    {
        /// <summary>
        /// Масив громадян спільний
        /// </summary>
        Citizen[] array = new Citizen[16];
        /// <summary>
        /// Масив пенсіонерів
        /// </summary>
        Retiree[] arrayR = new Retiree[4];
        /// <summary>
        /// Масив робітників
        /// </summary>
        Employee[] arrayE = new Employee[4];
        /// <summary>
        /// Масив студентів
        /// </summary>
        Student[] arrayS = new Student[4];

        /// <summary>
        /// Кількість елементів внесених користувачем
        /// </summary>
        public int Count => CountR + CountE + CountS;
        /// <summary>
        /// Кількість внесених пенсіонерів
        /// </summary>
        public int CountR { get; private set; }
        /// <summary>
        /// Кількість внесених робітників
        /// </summary>
        public int CountE { get; private set; }
        /// <summary>
        /// Кількість внесених студентів
        /// </summary>
        public int CountS { get; private set; }

        /// <summary>
        /// Ємність масиву громадян
        /// </summary>
        public int Capacity => array.Length;
        /// <summary>
        /// Ємність масиву пенсіонерів
        /// </summary>
        public int CapacityR => arrayR.Length;
        /// <summary>
        /// Ємність масиву робітників
        /// </summary>
        public int CapacityE => arrayE.Length;
        /// <summary>
        /// Ємність масиву студентів
        /// </summary>
        public int CapacityS => arrayS.Length;

        #region Exeption
        /// <summary>
        /// Вихыд за межы масиву
        /// </summary>
        string outOfRange = "\n\tСпроба вийти за межі масиву."
        #endregion

        /// <summary>
        /// Ітератор/енумератор
        /// </summary>
        int position = -1;

        /// <summary>
        /// Перевірка чи дана колекція тільки для читання
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Повернення нумератора - generic
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Citizen> GetEnumerator()
            => this as IEnumerator<Citizen>;

        /// <summary>
        /// Повернення нумератора
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Скидання (лічильника) ітератора
        /// </summary>
        public void Reset() => position = -1;

        /// <summary>
        /// Повернення поточного значення - generic
        /// </summary>
        public Citizen Current => array[position];

        /// <summary>
        /// Повернення поточного значення
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Видалення всіх елементів
        /// </summary>
        public void Clear()
        {
            CountE = 0;
            CountR = 0;
            CountS = 0;
            array = new Citizen[16];
        }

        /// <summary>
        /// Звільнення пам'яті
        /// </summary>
        public void Dispose()
        {
            // скидання лічильника
            Reset();
            // TODO: Перевірити очищення значень Dispose()
            // чистать дані
            Clear();
        }

        /// <summary>
        /// Крокування по масиву
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (position++ < Count - 1)
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
        /// Додавання масиву елеменів
        /// </summary>
        /// <param name="items">Масив вхідних елементів</param>
        public void AddRange(params Citizen[] items)
        {
            // перевірка чи не пусті вхідні параметри
            if (items.Length < 1)
            {
                return;
            }

            #region вибір розміру масиву
            // в даному випадку для керування об'ємом масиву необхідно
            // розв'язати рівняння: capacity = 2^n > count
            // 2^n > count
            // log_2(2^n) > log_2(count)
            // n > log_2(count)
            // n = ln(count) / ln(2)
            // а так як передається певна кількість елементів length,
            // які необхідно доадти в масив, то рівняння прийме вигляд
            // n = ln(count + length) / ln(2)
            // якщо count + length >= capacity то міняємо розмір
            #endregion

            // розрахунок степіня двійки, який визначатиме ємність
            int power = (int)Math.Ceiling(
                Math.Log(Count + items.Length) / Math.Log(2));

            if (Count + items.Length >= Capacity)
            {
                Resize((int)Math.Pow(2, power));
            }

            // додавання нових авто
            for (int i = 0; i < items.Length; i++)
            {
                array[Count++] = items[i];
            }
        }
        




        /// <summary>
        /// Зміна розміру масиву
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="capacity">Нова ємність масиву</param>
        /// <param name="coutn">Кількість чоловік певного типу</param>
        /// <param name="array">Масив певного типу</param>
        private void Resize<T>(int capacity, int coutn, ref T[] array)
        {
            // створення нового масиву
            T[] temp = new T[capacity];
            // копіювання елементів
            for (int i = 0; i < coutn; i++)
            {
                temp[i] = array[i];
            }
            // змінна ссилки на новий масив
            array = temp;
        }

        /// <summary>
        /// Доступ до масиву по індексу
        /// </summary>
        /// <param name="index">індекс</param>
        /// <returns></returns>
        public Citizen this[int index]
        {
            get
            {
                if (0 <= index && index < Count)
                {
                    return array[index];
                }

                // вихід за межі
                throw new Exception(outOfRange);
            }
            set
            {
                if (0 <= index && index < Count)
                {
                    array[index] = value;
                }

                // вихід за межі
                throw new Exception(outOfRange);
            }
        }

        public void Add(Citizen item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Citizen item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Citizen[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(Citizen item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Citizen item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Citizen item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }


    }
}
