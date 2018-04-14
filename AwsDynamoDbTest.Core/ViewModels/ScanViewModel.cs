using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
	public class ScanViewModel : BaseViewModel
    {
		private string _resultText;

		public ICommand ScanCommand;

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

		   });
        }
    }
}
