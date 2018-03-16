//Code in this file adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html,
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
using Amazon.DynamoDBv2.Model;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync; //For AmazonCognitoSyncConfig
//using Amazon.CognitoSync.Model; //For ListRecordsRequest and ResourceNotFoundException
//using Amazon.CognitoSync.SyncManager; //For CognitoSyncManager. Found this out by looking at https://github.com/awslabs/aws-sdk-net-samples/blob/master/XamarinSamples/CognitoSync/TODOListPortableLibrary/CognitoSyncUtils.cs.
using Xamarin.Forms;
//******

namespace AwsDynamoDbTest.Core
{
    public partial class AwsDynamoDbTestPage : ContentPage
    {
        //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
        private static AmazonDynamoDBClient _client;
        DynamoDBContext _context;
        //private static string _tableName = "ExampleTable";
        bool _isStatusOk = true;
        //******

        public AwsDynamoDbTestPage()
        {
            InitializeComponent();

            //****** Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            // Get AWS credentials. Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                //"us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0", // Identity pool ID
                "us-east-2:fc497215-b4e7-48f6-b4b4-ae4618026857", // Identity pool ID for DynamoDbIdentityPool
                RegionEndpoint.USEast2 // Region
            );

            //****** Adapted from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/getting-started-store-retrieve-data.html.
            _client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);
            _context = new DynamoDBContext(_client);
            //******

            // Initialize the Cognito Sync client. Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            //CognitoSyncManager syncManager = new CognitoSyncManager(
            Amazon.CognitoSync.AmazonCognitoSyncClient syncClient = new AmazonCognitoSyncClient( //Found out this line through trial and error.
                credentials,
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
        private async static Task CreateTableAsync(string tableName)
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

        private static void ListTables(int maximumNumberOfTablesToReturn)
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

        private static void GetTableInformation(string tableName)
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

        private async static Task UpdateTableAsync(string tableName)
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

        private static void DeleteTable(string tableName)
        {
            System.Diagnostics.Debug.WriteLine("\n*** Deleting table ***");
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            var response = _client.DeleteTableAsync(request);

            System.Diagnostics.Debug.WriteLine("Table is being deleted...");
        }

        private async static Task WaitUntilTableReadyAsync(string tableName)
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

        private async Task<bool> SaveItemAsync()
        {
            System.Diagnostics.Debug.WriteLine("\n*** Saving item into DynamoDBTest table ***");

            var currentTime = DateTime.Now;

            Item testItem = new Item()
            {
                //Id = "1",
                //SavedTimeStamp = DateTime.Now.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
                Id = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET_NO_SPACES_NO_SEPARATORS) + "#" + Guid.NewGuid().ToString(),
                SavedTimeStamp = currentTime.ToString(CodeConstants.DateTime.TIMESTAMP_WITH_OFFSET),
                Name = "Testing_SaveItemAsync()"
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

        private async Task<bool> ReadItemAsync(object hash)
        {
            try
            {
                Item retrievedItem = await _context.LoadAsync<Item>(hash); 
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
                    System.Diagnostics.Debug.WriteLine("ReadItemAsync() status = OK");

                _isStatusOk = true;
            }
            return _isStatusOk;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await SaveItemAsync();
                await ReadItemAsync("20180316112048+08:00#cf3f8156-78c6-4b38-9c5d-ef23ea35fdc3");
                //CreateTableAsync("ExampleTable");
                //WaitUntilTableReadyAsync("ExmapleTable");
                //ListTables(100);
                //GetTableInformation("ExmapleTable");
                //GetTableInformation("DynamoDBTest");
            }
            catch (AmazonDynamoDBException e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine("Overall AmazonDynamoDBException = " + e.Message);
            }
            catch (AmazonServiceException e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine("Overall AmazonServiceException = " + e.Message);
            }
            catch (Exception e)
            {
                _isStatusOk = false;
                System.Diagnostics.Debug.WriteLine("Overall Exception = " + e.Message);
            }
            //******
            finally
            {
                if (_isStatusOk)
                    System.Diagnostics.Debug.WriteLine("Overall status = OK");

                _isStatusOk = true;
            }
        }
    }
}