﻿using System;
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
    class MyList : IList<Citizen>, IEnumerable<Citizen>, IEnumerator<Citizen>, ICollection<Citizen>
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
        /// Змінні для свойств, які відображають кількість внесених елементів
        /// </summary>
        private int countR, countE, countS;

        /// <summary>
        /// Кількість елементів внесених користувачем
        /// </summary>
        public int Count => CountR + CountE + CountS;
        /// <summary>
        /// Кількість внесених пенсіонерів
        /// </summary>
        public int CountR
        {
            get { return countR; }
            private set { countR = value; }
        }
        /// <summary>
        /// Кількість внесених робітників
        /// </summary>
        public int CountE
        {
            get { return countE; }
            private set { countE = value; }
        }
        /// <summary>
        /// Кількість внесених студентів
        /// </summary>
        public int CountS
        {
            get { return countS; }
            private set { countS = value; }
        }

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
        string outOfRange = "\n\tСпроба вийти за межі масиву.";
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
        /// Додавання одного елемента
        /// </summary>
        /// <param name="item"></param>
        public void Add(Citizen item)
            => AddRange(item);

        /// <summary>
        /// Додавання масиву елеменів
        /// </summary>
        /// <param name="data">Масив вхідних елементів</param>
        public void AddRange(params Citizen[] data)
        {
            // перевірка чи не пусті вхідні параметри
            if (data.Length < 1)
            {
                return;
            }

            // Оскільки за умовою в нас 3 види громадян то пофільтруємо їх

            // фільтр по пенсіонерам
            Citizen[] filter = data.Where(t => t.GetType() == typeof(Retiree))
                .Select(t => t).ToArray();
            // заносимо дані в масив пенсіонерів
            ChangeArray<Retiree>((Retiree[])filter, ref arrayR, ref countR);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, 0, CountR);
            // заносимо дані в спільний масив
            Array.Copy(arrayR, 0, array, 0, CountR);

            // фільтр по робітникам
            filter = data.Where(t => t.GetType() == typeof(Employee))
                .Select(t => t).ToArray();
            // заносимо дані в масив робітників
            ChangeArray<Employee>((Employee[])filter, ref arrayE, ref countE);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, CountR, CountE);
            // заносимо дані в спільний масив
            Array.Copy(arrayE, 0, array, CountR, CountE);

            // фільтр по студентам
            filter = data.Where(t => t.GetType() == typeof(Student))
                .Select(t => t).ToArray();
            // заносимо дані в масив студентів
            ChangeArray<Student>((Student[])filter, ref arrayS, ref countS);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, CountR + CountE, CountS);
            // заносимо дані в спільний масив
            Array.Copy(arrayS, 0, array, CountR + CountE, CountS);
        }

        /// <summary>
        /// Присвоєння даних відповідному масиву
        /// </summary>
        /// <typeparam name="T">Тип даних</typeparam>
        /// <param name="inputArray">Вхідний масив даних, який необхідно внести</param>
        /// <param name="baseArray">Внутрішній масив даних</param>
        /// <param name="count">Кількість елементів базового масиву</param>
        /// <param name="capacity">Ємність базового масиву</param>
        private void ChangeArray<T>(T[] inputArray, ref T[] baseArray, ref int count)
        {
            // перевірка чи не пустий масив передається для присвоєння
            if (inputArray.Length < 1)
            {
                return;
            }

            // аналіз розмірів і зміна ємності при необхідності
            AnaliseSize<T>(ref baseArray, count, inputArray.Length);

            // додавання нових елементів
            for (int i = 0; i < inputArray.Length; i++)
            {
                baseArray[count++] = inputArray[i];
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
        /// Аналіз і зміна розмірів масивe за необхідністю
        /// </summary>
        /// <typeparam name="T">Тип елементів</typeparam>
        /// <param name="array">вхідний масив</param>
        /// <param name="ownCount">Кількість власних елементів</param>
        /// <param name="newCount">Кількість вхідних нових елементів</param>
        private void AnaliseSize<T>(ref T[] array, int ownCount, int newCount)
        {
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
                Math.Log(ownCount + newCount) / Math.Log(2));

            if (ownCount + newCount >= array.Length)
            {
                Resize<T>((int)Math.Pow(2, power), ownCount, ref array);
            }
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


        /// <summary>
        /// Копіює ICollection в Array, починаючи з певного ындексу
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Citizen[] array, int arrayIndex)
            => this.array.CopyTo(array, arrayIndex);

        /// <summary>
        /// Пошук індекса елемента по значенню
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns>індекс елемента, якщо нічого не знайдено то -1</returns>
        public int IndexOf(Citizen item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Перевірка наявності елемента в колекції
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Contains(Citizen item)
            => IndexOf(item) == -1;








        /// <summary>
        /// Видалення елемента по індексу
        /// </summary>
        /// <param name="index">індекс</param>
        public void RemoveAt(int index)
        {
            if ((0 <= index && index < Count) == false)
            {
                // вихід за межі
                throw new Exception(outOfRange);
            }

            // Аналізує де знаходиться індекс видалення
            if (0 <= index && index < CountR)   // пенсіонери
            {
                // корегуємо масив пенсіонерів
                Array.Copy(arrayR, index + 1, arrayR, index, CountR-- - index + 1);
                //TODO: Написати метод корекції масива
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Citizen item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Citizen item)
        {
            throw new NotImplementedException();
        }
    }
}
