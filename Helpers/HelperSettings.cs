

using System.Text;
using IniParser;
using IniParser.Model;

namespace PwdGen.Helpers
{
    public enum State
    {
        Off = 0, On = 1
    }
    static public class HelperSettings
    {

        public const string PathConfigurationFile = "config.ini";
        public static State InitEToken
        {
            get
            {

                return ReadState("InitUsb");
            }

            set
            {
                WriteState(value, "InitUsb");
            }
        }

        public static State PrintState
        {
            get
            {
                return ReadState("Printer");
            }

            set
            {
                WriteState(value, "Printer");
            }
        }

        private static State ReadState(string nameParam)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(PathConfigurationFile, Encoding.UTF8);
            return data["Options"][nameParam] == "true" ? State.On : State.Off;
        }

        private static void WriteState(State val, string nameParam)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(PathConfigurationFile, Encoding.UTF8);
            data["Options"][nameParam] = val == State.On
                ? data["Options"][nameParam] = "true"
                : data["Options"][nameParam] = "false";
            parser.WriteFile("config.ini", data, Encoding.UTF8);
        }
    }


}