using System;
using System.Collections.Generic;
using System.Linq;
using DYMO.Label.Framework;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.Helpers
{
    class HelperPrinter
    {

        static readonly IMessageService Message = new MessageService();

        // TODO : документация http://www.labelwriter.com/software/dls/sdk/docs/DYMOLabelFrameworkdotNETHelp/Index.html

        // выбор принтера
        static public IEnumerable<string> SetupLabelWriterSelection()
        {
            try
            {
                return Framework.GetPrinters().Select(printer => printer.Name);
            }
            catch (Exception)
            {
                
                Message.ShowError("Обнаружена ошибка при обращении к драйверам DYMO. \nПереустановите или Обновите ПО для принтера DYMO");
            }
            return null;
        }

        public static void Print(string text, string printerName,string configFile)
        {
            var printers = Framework.GetPrinters();
            var label = Framework.Open(configFile);
            label.SetObjectText("TEXT", text);
            label.Print(printers[printerName]);
        }
    }
}