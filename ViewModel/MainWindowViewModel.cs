using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using IniParser;
using IniParser.Model;
using PwdGen.Helpers;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
       
        private bool _isSelectedInitEToken;
        /// <summary>
        /// Свойство Включенна ли Инициализация юсб ключа
        /// </summary>
        public bool IsSelectedInitEToken
        {
            get
            {
                _isSelectedInitEToken = HelperSettings.InitEToken == State.On;
                return _isSelectedInitEToken;
            }
            set
            {
                if (_isSelectedInitEToken == value)
                    return;
                HelperSettings.InitEToken = value ? State.On : State.Off;
            }
        }

        private bool _isSelectedPrinter;
        /// <summary>
        /// Свойство нужно ли печатать на термопринтере
        /// </summary>
        public bool IsSelectedPrinter
        {
            get
            {
                _isSelectedPrinter = HelperSettings.PrintState == State.On;
                return _isSelectedPrinter;
            }
            set
            {
                if (_isSelectedPrinter == value)
                    return;
                HelperSettings.PrintState = value ? State.On : State.Off;
                OnPropertyChanged("IsSelectedPrinter");
            }
        }










        public ICommand StartCommand { get; set; }
        public ICommand StartCommand1 { get; set; }
        public ICommand FormLoaded { get; set; }





        private readonly HelperGenerator _helper = new HelperGenerator();
        public MainWindowViewModel()
        {
            FormLoaded = new RelayCommand(_ =>
            {
                MessageBox.Show("Форма Загруженна тест!");

            });


            #region Ужас:)

            StartCommand = new RelayCommand(_ => MessageBox.Show("Логика старта тест -> "
                                                                 + Environment.NewLine + _helper.GetPass()
                                                                 + Environment.NewLine + _helper.GetData()
                                                                 + Environment.NewLine + Id3()));

            StartCommand1 = new RelayCommand(_ => MessageBox.Show("Логика старта тест -> "
                + Environment.NewLine + _helper.GetPass()
                + Environment.NewLine + _helper.GetData()
                + Environment.NewLine + Id1()));

            //InitUsb = new RelayCommand(_ =>
            //{
            //    var parser = new FileIniDataParser();
            //    IniData data = parser.ReadFile("config.ini", Encoding.UTF8);

            //    // var initflag = data["Options"]["InitUsb"];
            //    // Debug.Print(initflag + " " + initflag.GetType());
            //    // Необходимо выпилить лишние. Так же нужно оффать команду когда будет запущен процесс с инициализацией ключа eToken
            //    data["Options"]["InitUsb"] = data["Options"]["InitUsb"] == "true" ? "false" : "true";
            //    parser.WriteFile("config.ini",data,Encoding.UTF8);
            //});

            #endregion

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
