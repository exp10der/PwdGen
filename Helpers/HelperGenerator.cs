using System;
using System.Linq;
using System.Text;

namespace PwdGen.Helpers
{
    internal class HelperGenerator
    {
        // Если файлов нету то стартуем с этого значения
        private static readonly string IdDefaultUc3 = "СЗИ-0000";

        public string GetData()
        {
            return DateTime.Today.ToShortDateString();
        }

        public string GetId(string currentId)
        {
            // Если файлов нету то начинаем с дефолтных айди
            if (currentId == null)
            {
                return IdDefaultUc3;
            }
            var tmp =
                currentId.Split('-')
                    .Select((n, i) => i == 1 ? string.Format("{0:0000}", Convert.ToInt32(n) + 1) : n)
                    .ToArray();
            return tmp[0] + "-" + tmp[1];
        }

        #region Password

        private const string CapitalLetters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string SmallLetters = "abcdefghijkmnpqrstuvwxyz";
        private const string Digits = "23456789";
        private const string SpecialCharacters = "+#$%@?!";
        private static readonly Random Rnd = new Random();
        private StringBuilder _password;

        private void GenPass()
        {
            _password = new StringBuilder();
            for (var i = 1; i <= 2; i++)
            {
                var capitalLeter = GenerateChar(CapitalLetters);
                InsertAtRandomPosition(_password, capitalLeter);
            }
            for (var i = 1; i <= 2; i++)
            {
                var smallLetter = GenerateChar(SmallLetters);
                InsertAtRandomPosition(_password, smallLetter);
            }
            var digit = GenerateChar(Digits);
            InsertAtRandomPosition(_password, digit);

            for (var i = 1; i <= 3; i++)
            {
                var specialChar = GenerateChar(SpecialCharacters);
                InsertAtRandomPosition(_password, specialChar);
            }
        }

        private void InsertAtRandomPosition(StringBuilder password, char character)
        {
            var randomPosition = Rnd.Next(password.Length + 1);
            password.Insert(randomPosition, character);
        }

        private char GenerateChar(string availaleChars)
        {
            var randomIndex = Rnd.Next(availaleChars.Length);
            var randomChar = availaleChars[randomIndex];
            return randomChar;
        }

        public string GetPass()
        {
            GenPass();
            var pass = Convert.ToString(_password);
            return pass;
        }

        #endregion
    }
}