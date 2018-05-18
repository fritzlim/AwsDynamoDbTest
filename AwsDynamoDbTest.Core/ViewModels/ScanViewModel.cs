using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input; //For ICommand

namespace AwsDynamoDbTest.Core.ViewModels
{
	public class ScanViewModel : BaseViewModel
    {
		private string _resultLabelText;
		private string _resultText;

		public ICommand ScanAllCommand { get; private set; }

		public string ResultLabelText
        {
            get { return _resultLabelText; }
            set
            {
                _resultLabelText = value;
                RaisePropertyChanged("ResultLabelText");
            }
        }

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
			ResultLabelText = "Retrieved information is shown here.";

			ScanAllCommand = new Command(async () =>
		    {
				ResultText = await Helpers.AwsDynamoDbHelper.Instance().ScanAllAsync();
				ResultLabelText = "Retrieved information:";

				System.Diagnostics.Debug.WriteLine("scanResult = " + ResultText);
		    });
        }
    }
}
