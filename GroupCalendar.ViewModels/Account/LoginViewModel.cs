using CommunityToolkit.Mvvm.Input;
using GroupCalendar.Core.Common;
using GroupCalendar.Core.Entity;
using GroupCalendar.Core.Helpers;
using GroupCalendar.Core.Services;
using System;

namespace GroupCalendar.ViewModels.Account
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly LoginService _loginService;

        public LoginViewModel(LoginService loginService)
        {
            _loginService = loginService;
        }


        #region Bindable Properties

        private string _id;

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        #endregion


        #region Commands

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand => _loginCommand ??= new RelayCommand(OnLogin);

        #endregion


        #region Helpers

        private async void OnLogin()
        {
            try
            {
                var member = await _loginService.LoginAsync(Id, Password);
                if (member == null)
                    return;

                UserManager.Instance.SetCurrentUser(member);
                OnSucessLogin();
            }
            catch (Exception)
            {

            }
        }

        protected override string ValidateProperty(string propertyName)
        {
            if (nameof(Id) == propertyName)
            {
                return String.IsNullOrWhiteSpace(Id) ? "값을 입력해주세요" : null;
            }

            if (nameof(Password) == propertyName)
            {
                return String.IsNullOrWhiteSpace(Password) ? "비밀번호를 입력해주세요" : null;
            }

            return null;
        }

        #endregion

        #region Events

        public event EventHandler SuccessLogin;

        protected virtual void OnSucessLogin()
        {
            SuccessLogin?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
