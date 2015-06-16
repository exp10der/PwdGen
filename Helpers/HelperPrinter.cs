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

        static readonly IMessageService Message = new MessageServicece();
        private static ILabel _label;

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
        //выбор принтера
        //public void SetupLabelWriterSelection()
        //{
        //    // Это верхний комбо бокс
        //    LabelWriterCmb.Items.Clear();

        //    foreach (IPrinter printer in Framework.GetPrinters())
        //        LabelWriterCmb.Items.Add(printer.Name);

        //    if (LabelWriterCmb.Items.Count > 0)
        //        LabelWriterCmb.SelectedIndex = 0;

        //    LabelWriterCmb.IsEnabled = LabelWriterCmb.Items.Count > 0;
        //    _label = Framework.Open("test.label");
        //    UpdateControls();
        //}

        ////печать при нажатии кнопки
        //private void Print()
        //{
        //    // ObjectNameCmb это хуйня с низу 
        //    _label.SetObjectText(ObjectNameCmb.Text, PassBox.Text);
        //    IPrinter printer = Framework.GetPrinters()[LabelWriterCmb.Text];
        //    _label.Print(printer); // print with default params
        //}

        //private void Print3()
        //{
        //    _label.SetObjectText(ObjectNameCmb.Text, PassBox3.Text);
        //    IPrinter printer = Framework.GetPrinters()[LabelWriterCmb.Text];
        //    _label.Print(printer); // print with default params
        //}

        //private void UpdateControls()
        //{
        //    ObjectNameCmb.Items.Clear();

        //    if (_label == null)
        //        return;

        //    foreach (string objName in _label.ObjectNames)
        //        if (!string.IsNullOrEmpty(objName))
        //            ObjectNameCmb.Items.Add(objName);

        //    if (ObjectNameCmb.Items.Count > 0)
        //        ObjectNameCmb.SelectedIndex = 0;


        //    ObjectNameCmb.IsEnabled = ObjectNameCmb.Items.Count > 0;
        //    PassBox.IsEnabled = ObjectNameCmb.Items.Count > 0 && !string.IsNullOrEmpty(ObjectNameCmb.Text);
        //}
    }
}