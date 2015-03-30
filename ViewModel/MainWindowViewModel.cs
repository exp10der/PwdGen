using System;
using System.Windows;
using System.Windows.Input;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public ICommand StartCommand { get; set; }
        public ICommand StartCommand1 { get; set; }
        private readonly HelperGenerator _helper = new HelperGenerator();
        public MainWindowViewModel()
        {
            // TODO: Необходимо разобрать логику кнопки старта у вована слишком много не описуемого говнокода!
            
            StartCommand = new RelayCommand(obj => MessageBox.Show("Логика старта тест -> " 
                + Environment.NewLine + _helper.GetPass() 
                + Environment.NewLine + _helper.GetData()
                + Environment.NewLine + Id3()));

            StartCommand1 = new RelayCommand(obj => MessageBox.Show("Логика старта тест -> "
                + Environment.NewLine + _helper.GetPass()
                + Environment.NewLine + _helper.GetData()
                + Environment.NewLine + Id1()));

        }

         string Id3()
         {
             return _helper.GetId();
         }

         string Id1()
         {
             return _helper.GetId();
         }

    }
}
