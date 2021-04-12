using Acr.UserDialogs;
using KcmsChallengeAPP.Models;
using KcmsChallengeAPP.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace KcmsChallengeAPP.ViewModels
{
    sealed class LoginViewModel : BaseViewModel
    {
        #region [Properties]
        public Usuario UsuarioModel { get; set; }
        public IAsyncCommand LoginCommand { get; }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new AsyncCommand(ExecuteLoginCommandAsync, allowsMultipleExecutions: false);
            UsuarioModel = new Usuario
            {
                Email = string.Empty,
                Senha = string.Empty
            };
        }

        private async Task ExecuteLoginCommandAsync()
        {
            bool isEmail = Regex.IsMatch(UsuarioModel.Email.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!ValidaLogin())
            {
                //Toast Messages
                UserDialogs.Instance.Toast("Por favor, informe os campos para Login!", TimeSpan.FromSeconds(1));
                return;
            }
            if (!isEmail)
            {
                IsError = false;
                IsEmailError = true;
                //Toast Messages
                UserDialogs.Instance.Toast("Por favor, informe um Email válido!", TimeSpan.FromSeconds(1));
                return;
            }
            if(UsuarioModel.Email != "admin@kcms.com" || UsuarioModel.Senha != "kcms")
            {
                IsError = false;
                IsEmailError = true;
                //Toast Messages
                UserDialogs.Instance.Toast("Usuário/Senha inválidos!", TimeSpan.FromSeconds(1));
                return;
            }
            await Shell.Current.GoToAsync("HomePage");
        }

        /*---------------------- ValidaLogin ----------------------*/
        private bool ValidaLogin()
        {
            if (String.IsNullOrWhiteSpace(UsuarioModel.Email.Trim()))
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            if (String.IsNullOrWhiteSpace(UsuarioModel.Senha.Trim()))
            {
                IsSuccess = false;
                IsError = true;
                IsEmailError = false;
                return false;
            }
            IsSuccess = true;
            IsError = false;
            IsEmailError = false;
            return true;
        }
    }
}
