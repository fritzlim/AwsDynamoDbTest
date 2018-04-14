//with corrections adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html.

//****** Taken from https://forums.xamarin.com/discussion/81722/aws-dynamodb
using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
//using System.Threading;
using Amazon; //For RegionEndpoint
using Amazon.Runtime; //For AmazonServiceException. Taken from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel; //For QueryOperationConfig()
using Amazon.DynamoDBv2.Model;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync; //For AmazonCognitoSyncConfig
//using Amazon.CognitoSync.Model; //For ListRecordsRequest and ResourceNotFoundException
//using Amazon.CognitoSync.SyncManager; //For CognitoSyncManager. Found this out by looking at https://github.com/awslabs/aws-sdk-net-samples/blob/master/XamarinSamples/CognitoSync/TODOListPortableLibrary/CognitoSyncUtils.cs.
using Xamarin.Forms;
//******

using AwsDynamoDbTest.Core.DataStore;

namespace AwsDynamoDbTest.Core.Helpers
{
    public class AwsDynamoDbHelper
    {
        private static object _locker = new object(); //For singleton
        private static AwsDynamoDbHelper _instance; //For singleton

		private CognitoAWSCredentials _credentials;
        //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
        private static AmazonDynamoDBClient _client;
        DynamoDBContext _context;
        //private static string _tableName = "ExampleTable";
        bool _isStatusOk = true;
		//******

		private Item _retrievedItem;

        public static AwsDynamoDbHelper Instance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new AwsDynamoDbHelper();
                    }
                }
            }
            return _instance;
        }

        private AwsDynamoDbHelper() //Made private for singleton
        {
            //****** Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            // Get AWS credentials. Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            _credentials = new CognitoAWSCredentials(
                //"us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0", // Identity pool ID
                //"us-east-2:fc497215-b4e7-48f6-b4b4-ae4618026857", // Identity pool ID for DynamoDbIdentityPool
                CodeConstants.AWS.IDENTITY_POOL_ID,
                RegionEndpoint.USEast2 // Region
            );

            //****** Adapted from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/getting-started-store-retrieve-data.html.
            _client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.USEast2);
            _context = new DynamoDBContext(_client);
            //******

            // Initialize the Cognito Sync client. Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            //CognitoSyncManager syncManager = new CognitoSyncManager(
            Amazon.CognitoSync.AmazonCognitoSyncClient syncClient = new AmazonCognitoSyncClient( //Found out this line through trial and error.
                _credentials,
                new AmazonCognitoSyncConfig
                {
                    RegionEndpoint = RegionEndpoint.USEast2 // Region
                    }
            );

            //AmazonCognitoSyncConfig clientConfig = new AmazonCognitoSyncConfig
            //{
            //    RegionEndpoint = RegionEndpoint.USEast2
            //};
            //Amazon.CognitoSync.AmazonCognitoSyncClient syncClient = new AmazonCognitoSyncClient(credentials, clientConfig);

            // Create a record in a dataset and synchronize with the server.
            //Dataset dataset = syncClient. OpenOrCreateDataset("myDataset");
            //dataset.OnSyncSuccess += SyncSuccessCallback;
            //dataset.Put("myKey", "myValue");
            //dataset.SynchronizeAsync();
            //******

            //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
            //try
            //{
            //    SaveItemAsync();
            //    //CreateTableAsync("ExampleTable");
            //    //WaitUntilTableReadyAsync("ExmapleTable");
            //    //ListTables(100);
            //    //GetTableInformation("ExmapleTable");
            //    //GetTableInformation("DynamoDBTest");
            //}
            //catch (AmazonDynamoDBException e)
            //{
            //    _isStatusOk = false;
            //    System.Diagnostics.Debug.WriteLine("Overall AmazonDynamoDBException = " + e.Message);
            //}
            //catch (AmazonServiceException e)
            //{
            //    _isStatusOk = false;
            //    System.Diagnostics.Debug.WriteLine("Overall AmazonServiceException = " + e.Message);
            //}
            //catch (Exception e)
            //{
            //    _isStatusOk = false;
            //    System.Diagnostics.Debug.WriteLine("Overall Exception = " + e.Message);
            //}
            ////******
            //finally
            //{
            //    if(_isStatusOk)
            //        System.Diagnostics.Debug.WriteLine("Overall status = OK");

            //    _isStatusOk = true;
            //}

            //******Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItemsDocumentClasses.html.
            //Table table = Table.LoadTable(_client, "DynamoDBTest");
            //******

            //var currentTime = DateTime.Now;

            //****** Adapted from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/getting-started-store-retrieve-data.html.
            //Item testItem = new Item()
            //{
            //    //Id = "1",
            //    //SavedTimeStamp = DateTime.Now.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
            //    Id = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET_NO_SPACES_NO_SEPARATORS) + "#" + Guid.NewGuid().ToString(),
            //    SavedTimeStamp = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
            //    Name = "Testing_AwsDynamoDbTestPage()"
            //};
            //_context.SaveAsync(testItem);
            ////******

        }

        //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
        public async static Task CreateTableAsync(string tableName) //Made private for singleton
        {
            System.Diagnostics.Debug.WriteLine("\n*** Creating table ***");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH" //Partition key
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = "RANGE" //Sort key
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 6
                },
                TableName = tableName
            };

            var response = _client.CreateTableAsync(request);
            System.Diagnostics.Debug.WriteLine("CreateTableAsync() response = " + await response);

            //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html.
            var result = response.Result;
            var tableDescription = result.TableDescription;
            //******

            System.Diagnostics.Debug.WriteLine("{1}: {0} \t ReadsPerSec: {2} \t WritesPerSec: {3}",
                      tableDescription.TableStatus,
                      tableDescription.TableName,
                      tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                      tableDescription.ProvisionedThroughput.WriteCapacityUnits);

            string status = tableDescription.TableStatus;
            System.Diagnostics.Debug.WriteLine(tableName + " - " + status);

            await WaitUntilTableReadyAsync(tableName);
        }

        public static void ListTables(int maximumNumberOfTablesToReturn) //Made private for singleton
        {
            System.Diagnostics.Debug.WriteLine("\n*** listing tables ***");
            string lastEvaluatedTableName = null;
            do
            {
                var request = new ListTablesRequest
                {
                    Limit = maximumNumberOfTablesToReturn,
                    ExclusiveStartTableName = lastEvaluatedTableName
                };

                var response = _client.ListTablesAsync(request);

                //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html.
                var result = response.Result;
                foreach (string name in result.TableNames)
                    System.Diagnostics.Debug.WriteLine(name);

                lastEvaluatedTableName = result.LastEvaluatedTableName;

            } while (lastEvaluatedTableName != null);
            //******
        }

        public static void GetTableInformation(string tableName) //Made private for singleton
        {
            System.Diagnostics.Debug.WriteLine("\n*** Retrieving table information ***");
            var request = new DescribeTableRequest
            {
                TableName = tableName
            };

            var response = _client.DescribeTableAsync(request);

            //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html.
            var result = response.Result;
            var tableDescription = result.Table; //Deduced this code from auto-completion.
            //******

            System.Diagnostics.Debug.WriteLine("Name: {0}", tableDescription.TableName);
            System.Diagnostics.Debug.WriteLine("# of items: {0}", tableDescription.ItemCount);
            System.Diagnostics.Debug.WriteLine("Provision Throughput (reads/sec): {0}",
                                               tableDescription.ProvisionedThroughput.ReadCapacityUnits);
            System.Diagnostics.Debug.WriteLine("Provision Throughput (writes/sec): {0}",
                                               tableDescription.ProvisionedThroughput.WriteCapacityUnits);
        }

        public async static Task UpdateTableAsync(string tableName) //Made private for singleton
        {
            System.Diagnostics.Debug.WriteLine("\n*** Updating table ***");
            var request = new UpdateTableRequest()
            {
                TableName = tableName,
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 6,
                    WriteCapacityUnits = 7
                }
            };

            var response = _client.UpdateTableAsync(request);

            await WaitUntilTableReadyAsync(tableName);
        }

        public static void DeleteTable(string tableName) //Made private for singleton
        {
            System.Diagnostics.Debug.WriteLine("\n*** Deleting table ***");
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            var response = _client.DeleteTableAsync(request);

            System.Diagnostics.Debug.WriteLine("Table is being deleted...");
        }

        public async static Task WaitUntilTableReadyAsync(string tableName) //Made private for singleton
        {
            string status = null;
            // Let us wait until table is created. Call DescribeTable.
            do
            {
                await Task.Delay(5000); // Wait 5 seconds.
                try
                {
                    var response = _client.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html.
                    var result = response.Result;
                    var tableDescription = result.Table; //Deduced this code from auto-completion.
                    //******

                    System.Diagnostics.Debug.WriteLine("Table name: {0}, status: {1}",
                                                       tableDescription.TableName,
                                                       tableDescription.TableStatus);
                    status = tableDescription.TableStatus;
                }
                catch (Amazon.DynamoDBv2.Model.ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
                }
                finally
                {
                    System.Diagnostics.Debug.WriteLine("WaitUntilTableReadyAsync() status = " + status);
                }
            } while (status != "ACTIVE");
        }
        //******

        //****** Adapted from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
        //void SyncSuccessCallback(object sender, SyncSuccessEventArgs e)
        //{
        //    // Your handler code here
        //}
        //******

        /// <summary>
        /// Saves the item using the given name. This method is async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="itemName">Item name.</param>
		public async Task<bool> SaveItemUsingGivenNameAsync(string itemName) //Made private for singleton //TODO: Remove this method eventually, because it isn't needed anymore.
        {
            System.Diagnostics.Debug.WriteLine("\n*** Saving item into DynamoDBTest table ***");

            var currentTime = DateTime.Now;

            Item testItem = new Item()
            {
                //Id = "1",
                //SavedTimeStamp = DateTime.Now.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
                Id = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET_NO_SPACES_NO_SEPARATORS) + "#" + Guid.NewGuid().ToString(),
                SavedTimeStamp = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
                //Name = "Testing_SaveItemAsync()"
                Name = itemName
            };

            //_status = _context.SaveAsync(testItem).Status.ToString();

            //var status = await Task.FromResult(_context.SaveAsync(testItem));
            //System.Diagnostics.Debug.WriteLine ("Status = " +  _context.SaveAsync(testItem).Status);

            try
            {
                await _context.SaveAsync(testItem);
            }

            catch (AmazonDynamoDBException e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            catch (AmazonServiceException e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            finally
            {
                if (_isStatusOk)
                    System.Diagnostics.Debug.WriteLine("SaveItemAsync() status = OK");

                _isStatusOk = true;
            }
            return _isStatusOk;
        }

        /// <summary>
        /// Saves the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="item">Item.</param>
		public async Task<bool> SaveItemAsync(Item item) //Made private for singleton
		{
			System.Diagnostics.Debug.WriteLine("\n*** Saving item into DynamoDBTest table ***");

            if (string.IsNullOrEmpty(item.Name))
                item.Name = "";
            if (string.IsNullOrEmpty(item.Email))
                item.Email = "";
            if (string.IsNullOrEmpty(item.Password))
                item.Password = "";

            var currentTime = DateTime.Now;

			item.Id = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET_NO_SPACES_NO_SEPARATORS) + "#" + Guid.NewGuid().ToString();
			item.SavedTimeStamp = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET);
			item.Email = item.Email.ToLower();

			try
			{
				await _context.SaveAsync(item);
			}

			catch (AmazonDynamoDBException e)
			{
				_isStatusOk = false;
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			catch (AmazonServiceException e)
			{
				_isStatusOk = false;
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				_isStatusOk = false;
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			finally
			{
				if (_isStatusOk)
					System.Diagnostics.Debug.WriteLine("SaveItemAsync() status = OK");

				_isStatusOk = true;
			}
			return _isStatusOk;
		}
        
		public async Task<Item> ReadItemAsync(object Id) //Made private for singleton
		{
			try
			{
				_retrievedItem = await _context.LoadAsync<Item>(Id);
			}
           
			catch (AmazonDynamoDBException e)
            {
                _isStatusOk = false;
				System.Diagnostics.Debug.WriteLine("ReadItemAsync() AmazonDynamoDBException = " + e.Message);
		    }
            catch (AmazonServiceException e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine("ReadItemAsync() AmazonServiceException = " + e.Message);
            }
            catch (Exception e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine("ReaditemAsync() Exception = " + e.Message);
            }

            finally
            {
				if (_isStatusOk)
				{
#if DEBUG
					System.Diagnostics.Debug.WriteLine("ReadItemAsync() status = OK");

					System.Diagnostics.Debug.WriteLine("ReadItemAsync() Id = " + _retrievedItem.Id);
					System.Diagnostics.Debug.WriteLine("ReadItemAsync() Name = " + _retrievedItem.Name);
					System.Diagnostics.Debug.WriteLine("ReadItemAsync() Email = " + _retrievedItem.Email);
					System.Diagnostics.Debug.WriteLine("ReadItemAsync() Password = " + _retrievedItem.Password);
					System.Diagnostics.Debug.WriteLine("ReadItemAsync() SavedTimeStamp = " + _retrievedItem.SavedTimeStamp);

					//System.Diagnostics.Debug.WriteLine("ReadItemAsync() _retrievedItem = " + _retrievedItem);
#endif
				}

                _isStatusOk = true;
            }
			return _retrievedItem;
		}

        //****** Adapted from the Query and Scan section in https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/dynamodb-integration-objectpersistencemodel.html
        /// <summary>
        /// Reads items where the contents of the field is equal to the query string. This method is async.
        /// </summary>
        /// <returns>A List of items.</returns>
        /// <param name="fieldToQuery">Field to query.</param>
        /// <param name="queryString">Query string.</param>
        public async Task<List<Item>> ReadItemEqualAsync(string fieldToQuery, string queryString)
        {
			if (string.IsNullOrEmpty(queryString))
			{
				List<Item> blankResponse = new List<Item>
				{
					new Item
					{
						SavedTimeStamp = "Nothing to retrieve",
						Name = "Nothing to retrieve",
						Email = "Nothing to retrieve",
						Password = "Nothing to retrieve"
					}
				};

				RetrievedItemDataStore.Instance().retrievedName = "Nothing to retrieve"; //Is it necessary to store the UserNameToRetrieveText?
				RetrievedItemDataStore.Instance().savedTimeStamp = "Nothing to retrieve";
				RetrievedItemDataStore.Instance().name = "Nothing to retrieve";
				RetrievedItemDataStore.Instance().email = "Nothing to retrieve";
				RetrievedItemDataStore.Instance().password = "Nothing to retrieve";

				return blankResponse; //An error of "value cannot be null" will be thrown in the viewmodel if the queryString is null.
				                      //So FromQueryAsync() isn't executed if queryString is null.
			}

            //var client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.USEast2);
            //DynamoDBContext context = new DynamoDBContext(client);
            
			//QueryFilter queryFilter = null;

            //switch (queryOperator)
            //{
            //	case "Equal":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.Equal, queryString);
            //		break;
            //	case "GreaterThan":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.GreaterThan, queryString);
            //		break;
            //	case "GreaterThanOrEqual":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.GreaterThanOrEqual, queryString);
            //		break;
            //	case "LessThan":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.LessThan, queryString);
            //		break;
            //	case "LessThanOrEqual":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.LessThanOrEqual, queryString);
            //		break;
            //	case "BeginsWith":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.BeginsWith, queryString);
            //		break;
            //	case "Between":
            //		queryFilter = new QueryFilter(fieldToQuery, QueryOperator.Between, queryString);
            //		break;
            //}

            var search = _context.FromQueryAsync<Item>(new QueryOperationConfig()
            {
                //IndexName = "Name-index", //Taken from the DynamoDB dashboard when creating the Global Secondary Index.
                //Filter = new QueryFilter("Name", QueryOperator.Equal, "AwsDynamoDbTest app started")

                IndexName = fieldToQuery + "-index", //Taken from the DynamoDB dashboard when creating the Global Secondary Index.
                Filter = new QueryFilter(fieldToQuery, QueryOperator.Equal, queryString)
            });
#if DEBUG            
            //System.Diagnostics.Debug.WriteLine("QueryAsync() items retrieved:");

            int count = 0;
            var searchResponse = await search.GetRemainingAsync();
            string itemData = null;

            searchResponse.ForEach((item) =>
            {
                count++;
                //System.Diagnostics.Debug.WriteLine(count + ". " + "Name = " + item.Name + ", Password = " + item.Password + ", Id = " + item.Id + ", SavedTimeStamp = " + item.SavedTimeStamp);
                itemData += count + ". " + "Name = " + item.Name + ", Password = " + item.Password + ", Id = " + item.Id + ", SavedTimeStamp = " + item.SavedTimeStamp + "\n";
            });

            System.Diagnostics.Debug.WriteLine("ReadItemEqualAsync() retrieved " + count + " items:\n" + itemData);
            count = 0;
#endif
            return searchResponse;
        }
        //******
    }
}
