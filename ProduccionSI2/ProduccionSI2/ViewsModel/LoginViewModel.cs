

namespace ProduccionSI2.ViewsModel
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Attributes
        private string email;
        private string password;
        #endregion
        #region Constructor
        public LoginViewModel()
        {
            this.IsRemembered = true;
        }
        #endregion

        #region Properties
        public string User
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool IsRinnung
        {
            get;
            set;
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.User))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "error",
                    "ingresar un usuario",
                    "aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "error",
                    "ingresar un Password",
                    "aceptar");
                this.Password = string.Empty;
                return;
            }
        }
        #endregion
    }
}
