using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LesApp2.People;

// Примітка.
// 1. "Удаление – с начала коллекции." тобто? - Що малося точно на увазі? 
// Чом не можна скористатися RemoveAt(0)?
// 2. "Возможно удаление с передачей экземпляра Гражданина" - тобто?
// тобто витянути елемент? видалити із колекції і повернути вихідним параметром?
// 3. "Метод Contains возвращает true/false при налчичии/отсутствии элемента 
// в коллекции и номер в очереди." - для другого є метод IndexOf()
// 4. Аналогічно 3-му? Чи необхідно реалізувати через dynamic? Тому що такого не
// реалізують зазвичай
// 3 і 4 - можна скористатисся кортежами і анонімними типами


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
        /// Вихід за межі масиву
        /// </summary>
        string outOfRange = "\n\tСпроба вийти за межі масиву.";
        string copyID = "\n\tСпроба повторно ввести наявний ID.";
        string errorType = "\n\tСпроба ввести недопустимий тип.";
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

            #region Аналіз унікальності ID
            // аналізуємо паспортні дані і можливість додавання елемента
            // робимо вибірку даних паспортів по всіх громадянах
            int[] baseID = array.Select(t => t.ID).ToArray();
            int[] enterID = data.Select(t => t.ID).ToArray();

            // аналізуэмо вхідний масив чи в ньому немає копій
            // просто видалямо копії і порівнюємо кількість елементів
            if (enterID.Distinct().ToArray().Length != enterID.Length)
            {
                throw new Exception(copyID);
            }
            
            // на основі теорії множин робимо перетин (переріз) даних
            int[] inter = baseID.Intersect(enterID).Select(t => t).ToArray();
            // якщо довжина масиву більше  значить є копії
            if (inter.Length > 0)
            {
                throw new Exception(copyID);
            }
            #endregion

            // Оскільки за умовою в нас 3 види громадян то пофільтруємо їх

            #region обробка пенсіонерів
            // фільтр по пенсіонерам
            Citizen[] filter = data.Where(t => t.GetType() == typeof(Retiree))
                    .Select(t => t).ToArray();
            // заносимо дані в масив пенсіонерів
            ChangeArray<Retiree>((Retiree[])filter, ref arrayR, ref countR);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, 0, CountR);
            // заносимо дані в спільний масив
            Array.Copy(arrayR, 0, array, 0, CountR);
            #endregion

            #region обробка робітників
            // фільтр по робітникам
            filter = data.Where(t => t.GetType() == typeof(Employee))
                .Select(t => t).ToArray();
            // заносимо дані в масив робітників
            ChangeArray<Employee>((Employee[])filter, ref arrayE, ref countE);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, CountR, CountE);
            // заносимо дані в спільний масив
            Array.Copy(arrayE, 0, array, CountR, CountE);
            #endregion

            #region обробка студентів
            // фільтр по студентам
            filter = data.Where(t => t.GetType() == typeof(Student))
                .Select(t => t).ToArray();
            // заносимо дані в масив студентів
            ChangeArray<Student>((Student[])filter, ref arrayS, ref countS);
            // аналіз і зміна єсності за потребою
            AnaliseSize<Citizen>(ref array, CountR + CountE, CountS);
            // заносимо дані в спільний масив
            Array.Copy(arrayS, 0, array, CountR + CountE, CountS); 
            #endregion
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
        /// Аналіз і зміна розмірів масива за необхідністю
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
        /// Перевірка наявності елемента
        /// </summary>
        /// <typeparam name="T">int - повертає індекс першого елемента,
        /// bool - вказує чи наявний елемент в колекції</typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public dynamic Contains<T>(Citizen item)
        {
            if (typeof(T) == typeof(bool))
            {
                return Contains(item);
            }
            else if (typeof(T) == typeof(int))
            {
                return IndexOf(item);
            }
            else
            {
                throw new Exception(errorType);
            }
        }

        /// <summary>
        /// Перевірка наявності елемента, 
        /// з одночасним виведенням через анонімний тип
        /// умови наявності і позиції в масиві
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public dynamic ContainsAnon(Citizen item)
            => new
            {
                Contain = Contains(item),
                Number = IndexOf(item)
            };

        /// <summary>
        /// Перевірка наявності елемента, з одночасним виведенням через кортеж
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public (bool contains, int number) ContainsTuple(Citizen item)
            => (Contains(item), IndexOf(item));

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
                // аналізує розмір і змінює за потребою (якщо багато пустих комірок,
                // то для економії місця змінити масив з меншою ємністю)
                AnaliseSize<Retiree>(ref arrayR, CountR, 0);
            }
            else if (CountR <= index && index < CountR + CountE)
            {
                // корегуємо масив робітників
                Array.Copy(arrayE, (index - CountR) + 1, arrayE, index - CountR, 
                    CountE-- - (index - CountR) + 1);
                // зміна розмірів масиву
                AnaliseSize<Employee>(ref arrayE, CountE, 0);
            }
            else if (CountR + CountE <= index && index < Count)
            {
                // корегуємо масив студентів
                Array.Copy(arrayS, (index - (CountR + CountE)) + 1, arrayS, 
                    index - (CountR + CountE), CountS-- - (index - (CountR + CountE)) + 1);
                // зміна розмірів масиву
                AnaliseSize<Student>(ref arrayS, CountS, 0);
            }

            // аналогічно AddRange() з'єднуємо масиви різних типів в один
            Array.Copy(arrayR, 0, array, 0, CountR);
            Array.Copy(arrayE, 0, array, CountR, CountE);
            Array.Copy(arrayS, 0, array, CountR + CountE, CountS);
        }

        /// <summary>
        /// Видалення тільки першого елемента по вказаному значенню і повернення результату успішності
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Remove(Citizen item)
        {
            // якщо використати змінну для індексу, то буде швидше виконання
            int index = IndexOf(item);

            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Видалення першого елемента колекції
        /// </summary>
        public void RemoveFirst()
            => RemoveAt(0);

        /// <summary>
        /// Видалення останнього елемента колекції
        /// </summary>
        public void RemoveLast()
            => RemoveAt(Count - 1);

        /// <summary>
        /// Витягування елемента - видалення з колекції і повернення його вихідним параметром
        /// </summary>
        /// <param name="index">індекс елемента</param>
        /// <returns></returns>
        public Citizen RemoveExtend(int index)
        {
            if ((0 <= index && index < Count) == false)
            {
                // вихід за межі
                throw new Exception(outOfRange);
            }

            // кокпіювання елемента
            Citizen temp = array[index];

            // видалення елемента
            RemoveAt(index);

            // повернення елемнета
            return temp;
        }

        /// <summary>
        /// Повернути останній елемент
        /// </summary>
        /// <returns></returns>
        public dynamic ReturnLast<T>()
        {
            // перевірка чи введений тип є типом який наслідує даний клас
            if (typeof(T).IsSubclassOf(typeof(Citizen)) ||
                typeof(T) == typeof(Citizen))
            {
                return array[Count - 1];
            }
            else if (typeof(T) == typeof(int))
            {
                return Count - 1;
            }
            else
            {
                throw new Exception(errorType);
            }
        } 

        /// <summary>
        /// Вставка елемента в певну позицію, яка визначається індексом
        /// </summary>
        /// <param name="index">індекс вставки елемента</param>
        /// <param name="item">елемент який необхідно вставити</param>
        public void Insert(int index, Citizen item)
        {
            // якщо буде вказано індекс який рівняється кількості елементів
            // тобто останній індекс + 1, то це вважатимето правильним заданням,
            // так як не утворюється проміжку між елементами
            
            //TODO: доробити вставку
        }

    }
}
