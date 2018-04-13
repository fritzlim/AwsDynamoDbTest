//using System;
//using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using AwsDynamoDbTest.Core.Helpers;
using AwsDynamoDbTest.Core.DataStore;
                     
namespace AwsDynamoDbTest.Core.Views
{
    public partial class UpdateAndDeletePage : ContentPage
    {
        public UpdateAndDeletePage()
        {
            InitializeComponent();
        }

		async Task RegisterPerson()
        {
            await AwsDynamoDbHelper.Instance().SaveItemUsingGivenNameAsync("Called SaveItemAsync() from RegisterPerson()");
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await AwsDynamoDbHelper.Instance().SaveItemUsingGivenNameAsync("Called SaveItemAsync() from Handle_Clicked(). userName=" + userName.Text);
        }

		protected async override void OnAppearing()
        {
			base.OnAppearing();
                     
            timeStamp.Text = RetrievedItemDataStore.Instance().SavedTimeStamp;
            userName.Text = RetrievedItemDataStore.Instance().Name;
            userEmail.Text = RetrievedItemDataStore.Instance().Email;
            userPassword.Text = RetrievedItemDataStore.Instance().Password;
		}
	}
}
