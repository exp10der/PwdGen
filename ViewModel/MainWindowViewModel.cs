//#define EnableConsole

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PwdGen.Helpers;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
            : this(new MessageService(), new FileManager())
        {
            #region Загрузка формы

            FormLoaded = new RelayCommand<string>(path =>
            {
                IsAvailability = true;
                try
                {
                    ExceptionKeys = File.ReadAllText("exception_key.txt");
                }
                catch (Exception ex)
                {
                    _messageService.ShowError(ex.Message);
                }

                CurrentContainerUc = _fileManager.Load(path + "UC.xml");
                if (CurrentContainerUc != null) return;
                _messageService.ShowExclamation(
                    "Отсутствуют ранее сформированные данные. \nУчетный номер будет установлен в начальное значение.");
                CurrentContainerUc = new Container();
            });

            #endregion

            #region команды

            #region Вкладка Исключения

            OpenFileExceptionKeys = new RelayCommand(() => Process.Start("exception_key.txt"));

            AddExceptionKey = new RelayCommand(() =>
            {
#if (EnableConsole)
                IsAvailability = false;
                var cmd = new Process
                {
                    StartInfo =
                    {
                        FileName = "InfoToken.exe",
                        WindowStyle = ProcessWindowStyle.Hidden
                    },
                    EnableRaisingEvents = true
                };
                cmd.Exited += (sender, args) =>
                {
                    IsAvailability = true;
                    ExceptionKeys = File.ReadAllText("exception_key.txt");
                };
                cmd.Start();
#endif
            });

            #endregion

            OpenHistory =
                new RelayCommand<string>(path => Process.Start(Path.Combine(Environment.CurrentDirectory, path)));

            Init = new RelayCommand<string>(path =>
            {
                IsAvailability = false;
                CurrentContainerUc = new Container
                {
                    Pass = _helper.GetPass(),
                    Date = _helper.GetData(),
                    Id = _helper.GetId(_currentContainerUc.Id)
                };
#if EnableConsole
                if (IsSelectedInitEToken)
                {
                    var arguments = "eToken_SZTLS_" + CurrentContainerUc.Date + " " + CurrentContainerUc.Id + " " +
                                    CurrentContainerUc.Pass;
                    var cmd = new Process
                    {
                        StartInfo =
                        {
                            FileName = "SapiInitToken.exe",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Arguments = arguments
                        }
                    };
                    cmd.Exited += (sender, args) =>
                    {
                        IsAvailability = true;
                        switch (File.ReadAllText("errorList.txt"))
                        {
                            case "1":
                                _messageService.ShowExclamation("Вставьте eToken для инициализации.");
                                break;
                            case "2":
                                _messageService.ShowExclamation("Не могу открыть файл конфигураци.");
                                break;
                            case "3":
                                _messageService.ShowExclamation("Вставленно больше одного токена для инициализации.");
                                break;
                            case "4":
                                _messageService.ShowExclamation("Вставьте eToken для инициализации.");
                                break;
                            case "5":
                                _messageService.ShowExclamation("Инициализация прошла не удачно.");
                                break;
                        }
                    };
                    cmd.Start();
                }
                if (IsSelectedPrinter)
                {
                    try
                    {
                        HelperPrinter.Print(CurrentContainerUc.Pass,SelectedNamePrinter,SelectedConfigFile);
                    }
                    catch (Exception ex)
                    {
                        _messageService.ShowError(ex.Message);
                    }
                    return;
                }
#else
                IsAvailability = true;
#endif

                _fileManager.Save(CurrentContainerUc, path + "UC.xml");
                _fileManager.SaveHistory(CurrentContainerUc, path + "UC.txt");
            });

            #endregion
        }

        public MainWindowViewModel(IMessageService message, IFileManager file)
        {
            _messageService = message;
            _fileManager = file;
        }

        #region Переменные и свойства

        private readonly IMessageService _messageService;
        private readonly IFileManager _fileManager;

        /// <summary>
        ///     Служебный класс для генерации паролей даты айди
        /// </summary>
        private readonly HelperGenerator _helper = new HelperGenerator();

        /// <summary>
        ///     Последний контейнер для УЦ3
        /// </summary>
        private Container _currentContainerUc;

        public Container CurrentContainerUc
        {
            get { return _currentContainerUc; }
            set { Set(() => CurrentContainerUc, ref _currentContainerUc, value); }
        }

        private bool _isSelectedInitEToken;

        /// <summary>
        ///     Свойство Включенна ли Инициализация юсб ключа
        /// </summary>
        public bool IsSelectedInitEToken
        {
            get { return _isSelectedInitEToken = HelperSettings.InitEToken == State.On; }
            set
            {
                if (_isSelectedInitEToken == value)
                    return;
                HelperSettings.InitEToken = value ? State.On : State.Off;
                Set(() => IsSelectedInitEToken, ref _isSelectedInitEToken, value);
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
                Set(() => IsSelectedPrinter, ref _isSelectedPrinter, value);
            }
        }

        /// <summary>
        ///     Список принтеров
        /// </summary>
        public IEnumerable<string> PrintersEnumerable
        {
            get
            {
#if enableconsole
           HelperPrinter.SetupLabelWriterSelection(); 
#else
                return new[] {"DYMO LabelManagerPnP"};
#endif
            }
        }

        public IEnumerable<string> LabelsEnumerable
        {
            get { return Directory.GetFiles(Environment.CurrentDirectory, "*.label").Select(Path.GetFileName); }
        }

        private string _exceptionKeys;

        public string ExceptionKeys
        {
            get { return _exceptionKeys; }
            set { Set(() => ExceptionKeys, ref _exceptionKeys, value); }
        }

        private bool _isAvailability;

        public bool IsAvailability
        {
            get { return _isAvailability; }
            set { Set(() => IsAvailability, ref _isAvailability, value); }
        }

        private string _selectedNamePrinter;

        public string SelectedNamePrinter
        {
            get { return _selectedNamePrinter; }
            set { Set(() => SelectedNamePrinter, ref _selectedNamePrinter, value); }
        }

        private string _selectedConfigFile;

        public string SelectedConfigFile
        {
            get { return _selectedConfigFile; }
            set { Set(() => SelectedConfigFile, ref _selectedConfigFile, value); }
        }

        #region Команды

        public ICommand Init { get; set; }
        public ICommand OpenFileExceptionKeys { get; set; }
        public ICommand AddExceptionKey { get; set; }
        public ICommand FormLoaded { get; set; }
        public ICommand OpenHistory { get; set; }

        #endregion

        #endregion
    }
}