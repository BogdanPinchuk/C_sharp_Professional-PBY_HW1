using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2.People
{
    /// <summary>
    /// Пенсіонер
    /// </summary>
    class Retiree : Citizen
    {
        #region Чомусь не працює
#if false
        public static explicit operator Retiree[] (Citizen[] array)
        {
            Retiree[] tempArray = new Retiree[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = (Retiree)array[i];
            }

            return tempArray;
        }

        public static explicit operator Citizen[] (Retiree[] array)
        {
            Citizen[] tempArray = new Citizen[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = (Citizen)array[i];
            }

            return tempArray;
        } 
#endif 
        #endregion
    }
}
