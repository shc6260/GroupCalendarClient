using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using GroupCalendar.Core.Common;
using System;

namespace GroupCalendar.ViewModels.Account
{
    public class AccountWindowViewModel : ViewModelBase
    {
        public AccountWindowViewModel()
        {
            _loginViewModel = Ioc.Default.GetService<LoginViewModel>();
            _loginViewModel.SuccessLogin += _loginViewModel_SuccessLogin;
            _registerViewModel = Ioc.Default.GetService<RegisterViewModel>();
            _registerViewModel.SuccessRegister += _registerViewModel_SuccessRegister;
            ViewModel = _loginViewModel;
        }

        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterViewModel _registerViewModel;

        #region Bindable Properties

        private ViewModelBase _viewModel;

        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set => SetProperty(ref _viewModel, value);
        }

        #endregion


        #region Commands

        private RelayCommand _goToResisterCommand;
        public RelayCommand GoToResisterCommand => _goToResisterCommand ??= new RelayCommand(OnGotoResister);

        private RelayCommand _goToLoginCommand;
        public RelayCommand GoToLoginCommand => _goToLoginCommand ??= new RelayCommand(OnGotoLogin);

        #endregion


        #region Helpers

        private void OnGotoResister()
        {
            ViewModel = _registerViewModel;
        }

        private void OnGotoLogin()
        {
            ViewModel = _loginViewModel;
        }

        private void _loginViewModel_SuccessLogin(object sender, System.EventArgs e)
        {
            OnSucessLogin();
        }

        private void _registerViewModel_SuccessRegister(object sender, EventArgs e)
        {
            OnGotoLogin();
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
