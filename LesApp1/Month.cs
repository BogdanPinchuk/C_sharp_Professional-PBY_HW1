using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
{
    /// <summary>
    /// Місяць
    /// </summary>
    struct Month
    {
        /// <summary>
        /// Назва місяця
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Порядковий номер місяця
        /// </summary>
        public int Number { get; }
        /// <summary>
        /// Кількість днів в місяці
        /// </summary>
        public int Days { get; }

        /// <summary>
        /// Конструктор місяця
        /// </summary>
        /// <param name="name">Назва</param>
        /// <param name="number">Порядковий номер</param>
        /// <param name="days">Кількість днів</param>
        public Month(string name, int number, int days)
        {
            this.Name = name;
            this.Number = number;
            this.Days = days;
        }

        /// <summary>
        /// Порівняння двох об'єктів
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            // кастимо до необхідного типу
            Month temp = (Month)obj;

            // повертаємо результат
            return Name == temp.Name &&
                Number == temp.Number &&
                Days == temp.Days;
        }

        /// <summary>
        /// Отримання HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => Number ^ Days;

        /// <summary>
        /// Виведення даних про місяць
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Number} - {Name}, {Days} days;";

    }
}
