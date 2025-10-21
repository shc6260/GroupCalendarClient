using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GroupCalendar.Core.Common
{
    public abstract class ViewModelBase : ObservableObject, IDataErrorInfo
    {
        public ViewModelBase() { }

        public virtual void Cleanup()
        {
            WeakReferenceMessenger.Default.Cleanup();
        }

        #region IDataErrorInfo

        /// <summary>
        /// 프로퍼티 에러 여부
        /// </summary>
        public virtual bool HasPropertyError => this.propertyErrorDictionary?.Any() == true;

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public virtual string Error => this.propertyErrorDictionary?.Values.FirstOrDefault();

        /// <summary>
        /// 전체 에러
        /// </summary>
        public virtual IEnumerable<string> Errors => this.propertyErrorDictionary?.Values;

        /// <summary>
        /// 에러 propertyName
        /// </summary>
        public virtual string ErrorPropertyName => this.propertyErrorDictionary?.Keys.FirstOrDefault();

        /// <summary>
        /// 에러 전체 propertyNames
        /// </summary>
        public virtual IEnumerable<string> ErrorPropertyNames => this.propertyErrorDictionary?.Keys;


        /// <summary>
        /// 프로퍼티 에러 딕셔너리.
        /// UI 스레드에서만 사용해야 한다.
        /// </summary>
        Dictionary<string, string> propertyErrorDictionary;

        public string this[string columnName]
        {
            get
            {
                string errorBefore = Error;
                bool hasErrorBefore = errorBefore != null;

                var errorMessage = ValidateProperty(columnName);
                if (errorMessage == null)
                {
                    if (this.propertyErrorDictionary != null)
                        this.propertyErrorDictionary.Remove(columnName);
                }
                else
                {
                    if (this.propertyErrorDictionary == null)
                        this.propertyErrorDictionary = new Dictionary<string, string>();

                    this.propertyErrorDictionary[columnName] = errorMessage;
                }

                string errorAfter = Error;
                bool hasErrorAfter = errorAfter != null;

                if (hasErrorBefore != hasErrorAfter)
                    OnPropertyChanged(nameof(HasPropertyError));

                if (errorBefore != errorAfter)
                    OnPropertyChanged(nameof(Error));

                return errorMessage;
            }
        }

        /// <summary>
        /// 프로퍼티 관련 에러 메시지를 반환한다.
        /// UI 스레드에서 호출된다.
        /// </summary>
        /// <param name="propertyName">대상 프로퍼티 이름</param>
        /// <returns>프로퍼티 관련 에러 메시지; 없을 경우 null.</returns>
        protected virtual string ValidateProperty(string propertyName)
        {
            return null;
        }

        #endregion
    }
}
