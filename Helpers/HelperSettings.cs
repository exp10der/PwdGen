using System.Text;
using IniParser;

namespace PwdGen.Helpers
{
    public enum State
    {
        Off = 0,
        On = 1
    }

    public static class HelperSettings
    {
        // TODO Дока по этой библиотеке https://github.com/rickyah/ini-parser
        public const string PathConfigurationFile = "config.ini";

        public static State InitEToken
        {
            get { return ReadState("InitUsb"); }

            set { WriteState(value, "InitUsb"); }
        }

        public static State PrintState
        {
            get { return ReadState("Printer"); }

            set { WriteState(value, "Printer"); }
        }

        private static State ReadState(string nameParam)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(PathConfigurationFile, Encoding.UTF8);
            return data["Options"][nameParam] == "true" ? State.On : State.Off;
        }

        private static void WriteState(State val, string nameParam)
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(PathConfigurationFile, Encoding.UTF8);
            data["Options"][nameParam] = val == State.On
                ? data["Options"][nameParam] = "true"
                : data["Options"][nameParam] = "false";
            parser.WriteFile("config.ini", data, Encoding.UTF8);
        }
    }
}