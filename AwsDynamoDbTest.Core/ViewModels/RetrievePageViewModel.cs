using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
    public class RetrievePageViewModel : BaseViewModel
    {
        //string itemName;

        //****** Adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/MainPageViewModel.cs.
		private string _userNameToRetrieveText;
		private string _userNameText;
        private string _userEmailText;
        private string _userPasswordText;
        //******

        //public ICommand RegisterPersonCommand { get; private set; }
        //public ICommand RetrievePersonCommand { get; private set; }
        public ICommand ReadPersonEqualCommand { get; private set; }

		//****** Adapted from https://github.com/humbertojaimes/Forms-chatbot/blob/master/ChatBotClient/ViewModel/MainPageViewModel.cs.
		public string UserNameToRetrieveText
		{
			get
			{
				if (!string.IsNullOrEmpty(_userNameToRetrieveText))
					return _userNameToRetrieveText;
				else return "";
			}
			set
			{
				if (value != null)
				{
					_userNameToRetrieveText = value;
					RaisePropertyChanged("UserNameToRetrieveText");
				}
			}
		}

		public string UserNameText
        {
            get { return _userNameText; }
            set
            {
                _userNameText = value;
                RaisePropertyChanged("UserNameText");
            }
        }

        public string UserEmailText
        {
            get { return _userEmailText; }
            set
            {
                _userEmailText = value;
                RaisePropertyChanged("UserEmailText");
            }
        }

        public string UserPasswordText
        {
            get { return _userPasswordText; }
            set
            {
                _userPasswordText = value;
                RaisePropertyChanged("UserPasswordText");
            }
        }
        //******

        public RetrievePageViewModel()
        {
            ////RegisterPersonCommand = new Command<string>(async (itemName) =>
            //RegisterPersonCommand = new Command(async () =>
            //{
            //    //if (string.IsNullOrEmpty(_userNameText))
            //    //    _userNameText = "";
            //    //if (string.IsNullOrEmpty(_userEmailText))
            //    //    _userEmailText = "";
            //    //if (string.IsNullOrEmpty(_userPasswordText))
            //    //_userPasswordText = "";

            //    Item itemToSave = new Item
            //    {
            //        Name = _userNameText,
            //        Email = _userEmailText,
            //        Password = _userPasswordText
            //    };

            //    //await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(itemName);
            //    //await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(_userNameText);

            //    IsBusy = true;
            //    await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(itemToSave);
            //    IsBusy = !IsBusy;
            //});

            //RetrievePersonCommand = new Command(async () =>
            //{
            //    IsBusy = true;
            //    await Helpers.AwsDynamoDbHelper.Instance().ReadItemAsync("20180401162245+08:00#d28a0288-18ab-49ce-964d-ccdca8738fc9");
            //    IsBusy = !IsBusy;
            //});

            ReadPersonEqualCommand = new Command(async () =>
            {
                IsBusy = true;
				var readResult = await Helpers.AwsDynamoDbHelper.Instance().ReadItemEqualAsync("Name", UserNameToRetrieveText);
                IsBusy = !IsBusy;

				if (readResult.Count >= 1)
				{
					readResult.ForEach((Item itemResult) =>
					{
						_userNameText = itemResult.Name;
						_userEmailText = itemResult.Email;
						_userPasswordText = itemResult.Password;
					});

					//****** Adapted from https://forums.xamarin.com/discussion/comment/280634/#Comment_280634 (taken from NMackay's June 2017 answer in https://forums.xamarin.com/discussion/97734/how-to-update-label-in-asynctask-from-the-viewmodel).
					UserNameText = _userNameText;
					UserEmailText = _userEmailText;
					UserPasswordText = _userPasswordText;
					//******
				}
				else
				{
					UserNameText = "No result found";
                    UserEmailText = "No result found";
                    UserPasswordText = "No result found";
				}
                
                //System.Diagnostics.Debug.WriteLine("Name = " + _userNameText + ", Email = " + _userEmailText + ", Password = " + _userPasswordText);
				System.Diagnostics.Debug.WriteLine("Name = " + UserNameText + ", Email = " + UserEmailText + ", Password = " + UserPasswordText);
            });
        }
    }
}
