using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PwdGen.Helpers;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private  bool _canExecute = true;
        public MainWindowViewModel() : this(new MessageServicece(), new FileManager())
        {
            FormLoaded = new RelayCommand(path =>
            {
                CurrentContainerUc3 = _fileManager.Load(path + "UC.xml");
                if (CurrentContainerUc3 != null) return;
                _messageService.ShowExclamation(
                    "Отсутствуют ранее сформированные данные. \nУчетный номер будет установлен в начальное значение.");
                CurrentContainerUc3 = new Container();
            });

            #region команды

            #region Вкладка Исключения

            OpenFileExceptionKeys = new RelayCommand(_ => Process.Start("exception_key.txt"));

            AddExceptionKey = new RelayCommand(_ =>
            {
                _canExecute = false;

                
                Task.Run(() =>
                {
                   
                    var cmd = new Process();
                    cmd.StartInfo.FileName = "InfoToken.exe";
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.EnableRaisingEvents = true;
                    cmd.Exited += (sender, args) =>
                    {

                      

                       OnPropertyChanged("ExceptionKeys"); 
                       
                    };//OnPropertyChanged("ExceptionKeys");
                    cmd.Start();
                }).ContinueWith(__ =>
                    OnPropertyChanged("ExceptionKeys"));
            });
            //, _ => !Process.GetProcessesByName("InfoToken").Any());

            #endregion

            OpenHistory =
                new RelayCommand(path => { Process.Start(Path.Combine(Environment.CurrentDirectory, path.ToString())); });
            StartUc = new RelayCommand(numberUc =>
            {
                CurrentContainerUc3 = new Container
                {
                    Pass = _helper.GetPass(),
                    Date = _helper.GetData(),
                    Id = _helper.GetId(_currentContainerUc3.Id)
                };
                var arguments = "eToken_SZTLS_" + CurrentContainerUc3.Date + " " + CurrentContainerUc3.Id + " " +
                                CurrentContainerUc3.Pass;
                if (HelperSettings.PrintState == State.On)
                {
                    Task.Run(() =>
                    {
                        var cmd = new Process();
                        //cmd.StartInfo.FileName = "lol.exe";
                        cmd.StartInfo.FileName = "SapiInitToken.exe";
                        cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        cmd.StartInfo.Arguments = arguments;
                        cmd.Start();
                    });
                }

                _fileManager.Save(CurrentContainerUc3, numberUc + "UC.xml");
                _fileManager.SaveHistory(CurrentContainerUc3, numberUc + "UC.txt");
            });
            //, _ =>
            //{
            //    CommandManager.InvalidateRequerySuggested();
            //    return !Process.GetProcessesByName("lol").Any();
            //});

            #endregion
        }

      

        public MainWindowViewModel(IMessageService message, IFileManager file)
        {
            _messageService = message;
            _fileManager = file;
        }
        #region Переменные
        private readonly IMessageService _messageService;
        private readonly IFileManager _fileManager;


        /// <summary>
        ///     Служебный класс для генерации паролей даты айди
        /// </summary>
        private readonly HelperGenerator _helper = new HelperGenerator();

        /// <summary>
        ///     Последний контейнер для УЦ3
        /// </summary>
        private Container _currentContainerUc3;

        public Container CurrentContainerUc3
        {
            get { return _currentContainerUc3; }
            set
            {
                _currentContainerUc3 = value;
                OnPropertyChanged("CurrentContainerUc3");
            }
        }

        private bool _isSelectedInitEToken;

        /// <summary>
        ///     Свойство Включенна ли Инициализация юсб ключа
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
                OnPropertyChanged("IsSelectedInitEToken");
            }
        }

        private bool _isSelectedPrinter;

        /// <summary>
        ///     Свойство нужно ли печатать на термопринтере
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

        /// <summary>
        ///     Список принтеров
        /// </summary>
        public IEnumerable<string> PrintersEnumerable
        {
            get { return new[] {"DYMO LabelManagerPnP"}; //HelperPrinter.SetupLabelWriterSelection();
            }
        }

        public IEnumerable<string> LabelsEnumerable
        {
            get { return Directory.GetFiles(Environment.CurrentDirectory, "*.label").Select(Path.GetFileName); }
        }

        public string ExceptionKeys
        {
            get { return File.ReadAllText("exception_key.txt"); }
        }

        #region Переменные команд

        public ICommand StartUc { get; set; }
        public ICommand OpenFileExceptionKeys { get; set; }
        public ICommand AddExceptionKey { get; set; }
        public ICommand FormLoaded { get; set; }
        public ICommand OpenHistory { get; set; }

        #endregion

        #endregion
    }
}