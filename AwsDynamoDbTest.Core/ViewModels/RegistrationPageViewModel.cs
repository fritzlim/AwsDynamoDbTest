using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
    public class RegistrationPageViewModel
	{
		//string itemName;
		public ICommand RegisterPersonCommand { get; private set; }

		public RegistrationPageViewModel()
		{
			RegisterPersonCommand = new Command<string>(async (itemName) =>
			{
				await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync(itemName);
				//await Helpers.AwsDynamoDbHelper.Instance().SaveItemAsync("Hello world");
			});
		}
    }
}
