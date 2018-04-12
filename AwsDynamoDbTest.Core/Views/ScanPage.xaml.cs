//using System;
//using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using AwsDynamoDbTest.Core.Helpers;

namespace AwsDynamoDbTest.Core.Views
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
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
    }
}
