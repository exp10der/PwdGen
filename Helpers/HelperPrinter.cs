using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DYMO.Label.Framework;

namespace PwdGen.Helpers
{
    class HelperPrinter
    {

        private static ILabel _label;

        // TODO : В общем курить мануалы http://www.labelwriter.com/software/dls/sdk/docs/DYMOLabelFrameworkdotNETHelp/Index.html жесть на поиски доки тупо убил минут 30

        // выбор принтера
        static public IEnumerable<string> SetupLabelWriterSelection()
        {
#if !DEBUG 
            try
            {
                // TODO : Гребаные дрова тупо вылетает :(
                var printers = Framework.GetPrinters().Select(printer => printer.Name);
            }
            catch (Exception)
            {

                MessageBox.Show("GG");
            }
#endif


            //_label = Framework.Open("test.label");

            // IEnumerable<string> printers = new[] { "хуевый", "принтер" };

            return new[] { "хуевый", "принтер", "как пофиксить sdk" };

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