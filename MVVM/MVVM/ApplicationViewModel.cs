using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace MVVM
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            //............
        }

        Phone selectedPhone;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<Phone> Phones { get; set; }

        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                (saveCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            fileService.Save(dialogService.FilePath, Phones.ToList());
                            dialogService.ShowMessage("Файл сохранен");
                        }
                    }
                    catch (Exception ex)
                    {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }

        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                (openCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            var phones = fileService.Open(dialogService.FilePath);
                            Phones.Clear();
                            foreach (var p in phones)
                                Phones.Add(p);
                            dialogService.ShowMessage("Файл открыт");
                        }
                    }
                    catch (Exception ex)
                    {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new RelayCommand(obj =>
                {
                    Phone phone = new Phone();
                    Phones.Insert(0, phone);
                    SelectedPhone = phone;
                }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                (removeCommand = new RelayCommand(obj =>
                {
                    Phone phone = obj as Phone;
                    if (phone != null)
                    {
                        Phones.Remove(phone);
                    }
                },
                (obj) => Phones.Count > 0));
            }
        }
        private RelayCommand doubleCommand;
        private DefaultDialogService defaultDialogService;
        private JsonFileService jsonFileService;

        public RelayCommand DoubleCommand
        {
            get
            {
                return doubleCommand ??
                (doubleCommand = new RelayCommand(obj =>
                {
                    Phone phone = obj as Phone;
                    if (phone != null)
                    {
                        Phone phoneCopy = new Phone
                        {
                            Company = phone.Company,
                            Price = phone.Price,
                            Title = phone.Title
                        };
                        Phones.Insert(0, phoneCopy);
                    }
                }));
            }
        }

        public Phone SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            // данные по умлолчанию
            Phones = new ObservableCollection<Phone>
{
new Phone { Title="iPhone 7", Company="Apple", Price=56000 },
new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
new Phone {Title="Elite x3", Company="HP", Price=56000 },
new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
};
        }

        public ApplicationViewModel(DefaultDialogService defaultDialogService, JsonFileService jsonFileService)
        {
            this.defaultDialogService = defaultDialogService;
            this.jsonFileService = jsonFileService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public interface IDialogService
    {
    }
}