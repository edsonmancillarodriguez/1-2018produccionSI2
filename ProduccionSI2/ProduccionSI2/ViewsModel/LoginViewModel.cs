

namespace ProduccionSI2.ViewsModel
{
    using GalaSoft.MvvmLight.Command;
    using ProduccionSI2.Views;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private string user;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
        }
        #endregion

        #region Properties
        public string User
        {
            get
            {
                return this.user;
            }
            set
            {
                if (this.user != value)
                {
                    this.user = value;
                    PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs(nameof(this.User)));
                }
            }
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs(nameof(this.Password)));
                }
            }
        }
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs(nameof(this.isRunning)));
                }
            }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(
                        this, new PropertyChangedEventArgs(nameof(this.IsEnabled)));
                }
            }
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
            

            IsRunning = true;
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("https://si2uagrm.ml");
            string url = string.Format("/api_rest_log/log_rest/find/"+User);
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;
            

            if (string.IsNullOrEmpty(result))
            {
                await Application.Current.MainPage.DisplayAlert("Error","servidor no responde","acceptar");
                this.User = string.Empty;
                this.Password = string.Empty;
                IsRunning = false;
                return;
            }
            else
            {
                string pass = GetSHA1(Password);
                if (result.Contains(pass))
                {
                    MainViewModel.GetInstance().PerfilUser = new PerfilUserViewModels();
                    await Application.Current.MainPage.Navigation.PushAsync(new PerfilUserPage());
                    await Application.Current.MainPage.DisplayAlert("Welcome", "Te doy la bienvenida "+User, "acceptar");
                    IsRunning = false;
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("validacion", "User o Password incorrectos", "acceptar");
                    IsRunning = false;
                    return;
                }
                
                
            }

            


        }
        #endregion

        #region MisMetodosInventados
        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        #endregion
    }
}
