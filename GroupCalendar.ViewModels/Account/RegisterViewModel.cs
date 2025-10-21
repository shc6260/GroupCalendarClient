using CommunityToolkit.Mvvm.Input;
using GroupCalendar.Core.Common;
using GroupCalendar.Core.Data;
using GroupCalendar.Core.Services;
using System;

namespace GroupCalendar.ViewModels.Account
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly MemberService _memberService;

        public RegisterViewModel(MemberService memberService)
        {
            _memberService = memberService;
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


        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        #endregion

        #region Commands

        private RelayCommand _registerCommand;
        public RelayCommand RegisterCommand => _registerCommand ??= new RelayCommand(OnRegister);

        #endregion


        #region Helpers

        private async void OnRegister()
        {
            try
            {
                await _memberService.AddMemberAsync(new MemberRequest(Id, Password, Name));
                OnSuccessRegister();
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
                return String.IsNullOrWhiteSpace(Id) ? "비밀번호를 입력해주세요" : null;
            }

            if (nameof(Name) == propertyName)
            {
                return String.IsNullOrWhiteSpace(Id) ? "이름을 입력해주세요" : null;
            }

            return null;
        }

        #endregion

        #region Events

        public event EventHandler SuccessRegister;

        protected virtual void OnSuccessRegister()
        {
            SuccessRegister?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
