using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using PwdGen.Helpers;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        #region Переменные
        private IMessageService messageService = new MessageServicece();
        private IFileManager fileManager = new FileManager();
        private string pathUc1 = @"uc_1_history\uc.xml";
        private string pathUc3 = @"uc_2_history\uc.xml";
        /// <summary>
        /// Служебный класс для генерации паролей даты айди
        /// </summary>
        private readonly HelperGenerator _helper = new HelperGenerator();
        /// <summary>
        /// Последний контейнер для УЦ3
        /// </summary>
        private Container _currentContainerUc3;
        public Container CurrentContainerUc3
        {
            get
            {
                return _currentContainerUc3;
            }
            set
            {
                _currentContainerUc3 = value;
                OnPropertyChanged("CurrentContainerUc3");
            }
        }
        /// <summary>
        /// Последний контейнер для УЦ1
        /// </summary>
        private Container _currentContainerUc1;
        public Container CurrentContainerUc1
        {
            get
            {
                return _currentContainerUc1;
            }
            set
            {
                _currentContainerUc1 = value;
                OnPropertyChanged("CurrentContainerUc1");
            }
        }

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
                OnPropertyChanged("IsSelectedInitEToken");
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

        /// <summary>
        /// Список принтеров
        /// </summary>
        public IEnumerable<string> PrintersEnumerable
        {
            get
            {
                return null; //HelperPrinter.SetupLabelWriterSelection();
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
        public ICommand StartUc3 { get; set; }
        public ICommand StartUc1 { get; set; }
        public ICommand OpenFileExceptionKeys { get; set; }
        public ICommand AddExceptionKey { get; set; }
        public ICommand FormLoaded { get; set; }
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            FormLoaded = new RelayCommand(_ =>
            {
                
                CurrentContainerUc1 = fileManager.Load(pathUc1);
                CurrentContainerUc3 = fileManager.Load(pathUc3);
                if (CurrentContainerUc1 == null || CurrentContainerUc3 == null)
                {
                    messageService.ShowExclamation("Отсутствуют ранее сформированные данные. \nУчетный номер будет установлен в начальное значение.");
                }
            });

            // TODO 2 Основные команды доделать логику! Разобраться с айди и запись в файл xml
            #region команды

            #region Вкладка Исключения
            OpenFileExceptionKeys = new RelayCommand(_ => Process.Start("exception_key.txt"), (_) => fileManager.IsExist("exception_key.txt"));

            AddExceptionKey = new RelayCommand(_ =>
            {
                //Process cmd = new Process();
                //cmd.StartInfo.FileName = "InfoToken.exe";
                //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //cmd.EnableRaisingEvents = true;
                //cmd.Exited += (sender, args) => OnPropertyChanged("ExceptionKeys");
                //cmd.Start();
                
                OnPropertyChanged("ExceptionKeys");
            }, (_) => !Process.GetProcessesByName("InfoToken.exe").Any());

            #endregion

            StartUc3 = new RelayCommand(numberUc =>
            {
               // Init(numberUc,CurrentContainerUc3,pathUc3);
                CurrentContainerUc3 = new Container { Pass = _helper.GetPass(), Date = _helper.GetData(), Id = _helper.GetId(numberUc.ToString()) };

                fileManager.Save(CurrentContainerUc3, pathUc3); 
            });
            StartUc1 = new RelayCommand(numberUc =>
            {
                CurrentContainerUc1 = new Container { Pass = _helper.GetPass(), Date = _helper.GetData(), Id = _helper.GetId(numberUc.ToString()) };

                fileManager.Save(CurrentContainerUc1,pathUc1); 
            });
            #endregion

            
        }

        private void Init(object arg, Container container, string path)
        {
            

            container = new Container { Pass = _helper.GetPass(), Date = _helper.GetData(), Id = _helper.GetId(arg.ToString()) };
            OnPropertyChanged("CurrentContainerUc3");
            fileManager.Save(container, path);  
        }
             
    }
}
