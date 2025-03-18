namespace Hackathon.Fiap.Domain.Utils.Helpers
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