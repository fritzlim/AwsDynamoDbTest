using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
	public class ScanViewModel : BaseViewModel
    {
		private string _resultText;

		public ICommand ScanCommand { get; private set; }

        public string ResultText
		{
			get { return _resultText; }
			set
			{
				_resultText = value;
				RaisePropertyChanged("ResultText");
			}
		}
        
        public ScanViewModel()
        {
			ScanCommand = new Command(async () =>
		    {
			    var scanResult = await Helpers.AwsDynamoDbHelper.Instance().ScanAsync();
				System.Diagnostics.Debug.WriteLine("scanResult = " + scanResult);
		    });
        }
    }
}
