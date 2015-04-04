using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace PwdGen.Helpers
{
    class HelperGenerator
    {

        // TODO : Это вроде можно оставить
        #region Password
        private const string CapitalLetters = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string SmallLetters = "abcdefghijkmnpqrstuvwxyz";
        private const string Digits = "23456789";
        private const string SpecialCharacters = "+#$%@?!";
        private static Random rnd = new Random();
        private StringBuilder password;

        private void GenPass()
        {
            password = new StringBuilder();
            for (int i = 1; i <= 2; i++)
            {
                char capitalLeter = GenerateChar(CapitalLetters);
                InsertAtRandomPosition(password, capitalLeter);
            }
            for (int i = 1; i <= 2; i++)
            {
                char smallLetter = GenerateChar(SmallLetters);
                InsertAtRandomPosition(password, smallLetter);
            }
            char digit = GenerateChar(Digits);
            InsertAtRandomPosition(password, digit);

            for (int i = 1; i <= 3; i++)
            {
                char specialChar = GenerateChar(SpecialCharacters);
                InsertAtRandomPosition(password, specialChar);
            }
        }

        private void InsertAtRandomPosition(StringBuilder password, char character)
        {
            int randomPosition = rnd.Next(password.Length + 1);
            password.Insert(randomPosition, character);
        }

        private char GenerateChar(string availaleChars)
        {
            int randomIndex = rnd.Next(availaleChars.Length);
            char randomChar = availaleChars[randomIndex];
            return randomChar;
        }

        public string GetPass()
        {
            GenPass();
            string pass = Convert.ToString(password);
            return pass;
        }

        #endregion

        public string GetData()
        {
            return DateTime.Today.ToShortDateString();
        }

        public string GetId([CallerMemberName]string id = "")
        {
            // TODO Примерные типы айди СЗ-0005(уц1) и СЗИ-0002(это уц2) необходимо уточнить формат у товарища Владимира
            return id;
        }
    }
}
