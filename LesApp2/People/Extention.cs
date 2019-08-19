using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2.People
{
    /// <summary>
    /// Методи розширення
    /// </summary>
    static class MyExtention
    {
        /// <summary>
        /// Конвертація в масив похідний від базового Citizen[]
        /// </summary>
        /// <param name="array">масив типу Citizen[]</param>
        /// <returns></returns>
        public static T[] ToDerived<T>(this Citizen[] array)
            where T : Citizen
        {
            // створення тимчасового масиву
            T[] tempArray = new T[array.Length];
            // поелементне конвертування
            for (int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i] as T;
            }

            return tempArray;
        }

    }

}
