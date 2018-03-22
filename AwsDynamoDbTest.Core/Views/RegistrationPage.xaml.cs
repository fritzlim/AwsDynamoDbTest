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
        }

        async Task RegisterPerson()
        {
            await AwsDynamoDbHelper.Instance().SaveItemAsync("Called SaveItemAsync() from RegisterPerson()");
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await AwsDynamoDbHelper.Instance().SaveItemAsync("Called SaveItemAsync() from Handle_Clicked()");
        }
    }
}
