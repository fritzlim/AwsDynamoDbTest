using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
	public class RegistrationPageViewModel : BaseViewModel
	{
		//string itemName;

		//****** Adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/MainPageViewModel.cs.
		private string _userNameText;
		private string _userEmailText;
		private string _userPasswordText;
        //******

		//****** Adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/MainPageViewModel.cs.
		public string UserNameText
		{
			get { return _userNameText; }
			set
			{
				_userNameText = value;
				RaisePropertyChanged();
			}
		}

        public string UserEmailText
		{
			get { return _userEmailText; }
			set
			{
				_userEmailText = value;
				RaisePropertyChanged();
			}
		}

        public string UserPasswordText
		{
			get { return _userPasswordText; }
			set
			{
				_userPasswordText = value;
				RaisePropertyChanged();
			}
		}
        //******

		public ICommand RegisterPersonCommand { get; private set; }

		public RegistrationPageViewModel()
		{
			//RegisterPersonCommand = new Command<string>(async (itemName) =>
			RegisterPersonCommand = new Command(async () =>
			{
				Item itemToSave = new Item
				{
					Name = _userNameText,
					Email = _userEmailText,
					Password = _userPasswordText

				};

				//await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(itemName);
				//await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(_userNameText);

				await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(itemToSave);
			});
		}
    }
}
