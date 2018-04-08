//using System;
//using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using AwsDynamoDbTest.Core.Helpers;

namespace AwsDynamoDbTest.Core.Views
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
			BindingContext = new ViewModels.RegistrationPageViewModel();
        }

        async Task RegisterPerson()
        {
            await AwsDynamoDbHelper.Instance().SaveItemUsingGivenNameAsync("Called SaveItemAsync() from RegisterPerson()");
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await AwsDynamoDbHelper.Instance().SaveItemUsingGivenNameAsync("Called SaveItemAsync() from Handle_Clicked(). userName=" + userName.Text);
        }
    }
}
