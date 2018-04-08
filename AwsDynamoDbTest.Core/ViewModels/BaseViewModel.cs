//Code in this file is adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/BaseViewModel.cs.

//using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwsDynamoDbTest.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _title;
        private bool _isBusy;

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
        {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //If PropertyChanged != null, call PropertyChanged(this, new PropertyChangedEventArgs(propertyName))
		}
    }
}
