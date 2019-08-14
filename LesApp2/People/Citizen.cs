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
        public int ID { get; set; }
        /// <summary>
        /// ПІБ
        /// </summary>
        public string FullName { get; set; }


    }
}
