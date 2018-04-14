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
            
			if (string.IsNullOrEmpty(RetrievedItemDataStore.Instance().id))
			{
				id.Text = "Retrieve records first";
				timeStamp.Text = "Retrieve records first";
				userName.Text = "Retrieve records first";
				userEmail.Text = "Retrieve records first";
				userPassword.Text = "Retrieve records first";
			}
			else
			{
				id.Text = RetrievedItemDataStore.Instance().id;
				timeStamp.Text = RetrievedItemDataStore.Instance().savedTimeStamp;
				userName.Text = RetrievedItemDataStore.Instance().name;
				userEmail.Text = RetrievedItemDataStore.Instance().email;
				userPassword.Text = RetrievedItemDataStore.Instance().password;
			}
		}
	}
}
