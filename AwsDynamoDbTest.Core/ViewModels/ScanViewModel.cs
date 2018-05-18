using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
	public class ScanViewModel : BaseViewModel
    {
		private string _resultText;

		public ICommand ScanAllCommand { get; private set; }

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
			ResultText = "The scan result will be shown here";

			ScanAllCommand = new Command(async () =>
		    {
				ResultText = await Helpers.AwsDynamoDbHelper.Instance().ScanAllAsync();
				System.Diagnostics.Debug.WriteLine("scanResult = " + ResultText);
		    });
        }
    }
}
