using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using PwdGen.Helpers;
using PwdGen.Infrastructure;
using PwdGen.Model;

namespace PwdGen.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
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
        /// Список принтеров TODO НИРАБОТАЕТ((( ПОЧИНИ МЕНЯ!
        /// </summary>
        public IEnumerable<string> PrintersEnumerable
        {
            get
            {
                return HelperPrinter.SetupLabelWriterSelection();
            }
        }








        public ICommand StartCommand3 { get; set; }
        public ICommand StartCommand1 { get; set; }

        /// <summary>
        /// Загрузка формы 
        /// TODO Сюда надо будет вставить загрузку с файла xml последних айди паролей ну и да нужно разобраться когда и как удалять файлы раз в 3 дня? какой принцип
        /// </summary>
        public ICommand FormLoaded { get; set; }





        
        public MainWindowViewModel()
        {
            FormLoaded = new RelayCommand(_ => MessageBox.Show("Форма Загруженна тест!"));

            // TODO 2 Основные команды доделать логику! Разобраться с айди и запись в файл xml
            #region команды

            StartCommand3 = new RelayCommand(_ =>
            {
                CurrentContainerUc3 = new Container { Pass = _helper.GetPass(), Date = _helper.GetData(), Id = Id3() }; 
            });

            StartCommand1 = new RelayCommand(_ =>
            {
                CurrentContainerUc1 = new Container {Pass = _helper.GetPass(),Date = _helper.GetData(), Id = Id1()};
            });

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
