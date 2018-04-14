using System;
namespace AwsDynamoDbTest.Core.DataStore
{
    public class RetrievedItemDataStore
    {
		private static RetrievedItemDataStore _instance;
		private static object _locker = new object();

		public static RetrievedItemDataStore Instance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
						_instance = new RetrievedItemDataStore();
                    }
                }
            }
            return _instance;
        }

		//public struct RetrievedItem
		//{
		//	public string SavedTimeStamp;
		//	public string Name;
		//	public string Email;
		//	public string Password;
		//}

		public string retrievedName;
		public string id;
        public string savedTimeStamp;
        public string name;
        public string email;
        public string password;
    }
}
