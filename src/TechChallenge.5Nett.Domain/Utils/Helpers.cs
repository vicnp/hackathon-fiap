using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Helpers
    {
        /// <summary>
        /// Retorna true se a string for vazia, nula ou espaço em branco.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool InvalidOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}