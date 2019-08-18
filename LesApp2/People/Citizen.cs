using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2.People
{
    /// <summary>
    /// Громадянин
    /// </summary>
    abstract class Citizen
    {
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// ПІБ
        /// </summary>
        public virtual string FullName { get; set; }

        public Type GetTypeThis()
            => this.GetType();

        public override string ToString()
            => "FullName: " + FullName + " ID: " + ID;

        /// <summary>
        /// Порівняння елементів
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var temp = obj as Citizen;

            if (temp == null)
            {
                return false;
            }

            return this.ID == temp.ID &&
                FullName == temp.FullName;
        }

        /// <summary>
        /// Хеш-код екземпляру
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => ID ^ FullName.GetHashCode();
    }
}
