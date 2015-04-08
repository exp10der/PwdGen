using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PwdGen.Helpers
{
    class HelperGenerator
    {
        // Если файлов нету то стартуем с этих значений
        // TODO : Уточнить дефолтныйе номера у Владимира
        private static string IdDefaultUc1 = "СЗ-0000";
        private static string IdDefaultUc3 = "СЗИ-0000";

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

        // Может стоит сделать конструктор если файла xml нету то ставим дефолт 
        // А текущии значения брать через свойства?
        // А может лучше передать сам объект куррентУц1 или Уц3?
        // Потом необходимо будет сделать рефактор ----
        public string GetId(string currentId)
        {
            #region Инфа потом можно делит
            // TODO Примерные типы айди СЗ-0005(уц1) и СЗИ-0002(это уц3) необходимо уточнить формат у товарища Владимира
            //Константин
            //ей если файлов нету xml то на УЦ1 айди СЗ-0000
            //а на УЦ3 СЗИ-0000 ?
            //Владимир
            //ахуеть+
            //Константин
            //т.е не с СЗ-0001?
            //а именно 0000 четрые нуля
            //Владимир
            //-
            //+
            //Константин
            //ок заебись
            #endregion

            // Если файлов нету то начинаем с дефолтных айди
            if (currentId == "UC3" || currentId == "UC1")
                return currentId == "UC3" ? IdDefaultUc3 : IdDefaultUc1;

            var tmp = currentId.Split('-').Select((n, i) => i == 1 ? string.Format("{0:0000}", Convert.ToInt32(n)+1) : n).ToArray();
            return tmp[0] + "-" + tmp[1];
        }
    }
}