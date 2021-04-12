using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace KcmsChallengeAPP.ViewModels.Base
{
    public abstract class BaseViewModel : BindableObject 
    {
        #region [ Properties ]
        /*---------------------- IsBusy Properties ----------------------*/
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        /*---------------------- IsRefresh Properties ----------------------*/
        private bool _isRefresh;
        public bool IsRefresh
        {
            get { return _isRefresh; }
            set { SetProperty(ref _isRefresh, value); }
        }
        /*---------------------- IsSuccess Properties ----------------------*/
        private bool _isSuccess;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { SetProperty(ref _isSuccess, value); }
        }
        /*---------------------- IsError Properties ----------------------*/
        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }
        /*---------------------- IsEmailError Properties ----------------------*/
        private bool _isEmailError;
        public bool IsEmailError
        {
            get { return _isEmailError; }
            set { SetProperty(ref _isEmailError, value); }
        }
        /*---------------------- IsVisible Properties ----------------------*/
        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }
        /*---------------------- IsAvailable Properties ----------------------*/
        private bool _isAvailable;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { SetProperty(ref _isAvailable, value); }
        }
        /*---------------------- IsRunning Properties ----------------------*/
        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        /*---------------------- IsLoading Properties ----------------------*/
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        /*---------------------- DisplayAlert Properties ----------------------*/
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
        /*---------------------- DisplayAlert Actions Properties ----------------------*/
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
        #endregion

        /*---------------------- Command On Exception ----------------------*/
        protected void CommandOnException(Exception obj)
        {
            //throw new NotImplementedException();
        }

        /*---------------------- Init Async ----------------------*/
        //public virtual Task InitializeAsync() => Task.CompletedTask;

        /*---------------------- Back Async ----------------------*/
        //public virtual Task BackAsync(object args) => Task.CompletedTask;

        /*---------------------- Xamarin Essentials Connectivity ----------------------*/
        public static bool InternetConnectionActive()
        {
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular))
                // Active Internet Connection.
                return true;
            else
                // No Internet Connection.
                return false;
        }
    }
}
