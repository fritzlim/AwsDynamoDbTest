﻿//Code in this file is adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/BaseViewModel.cs.

//using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwsDynamoDbTest.Core.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
        {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //If PropertyChanged != null, call PropertyChanged(this, new PropertyChangedEventArgs(propertyName))
		}

        string title;
        bool isBusy;

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}