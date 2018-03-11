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
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync; //For AmazonCognitoSyncConfig
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
        string _status;
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

            // Initialize the Cognito Sync client. Taken from https://us-east-2.console.aws.amazon.com/cognito/code/?region=us-east-2&pool=us-east-2:f4f90926-c251-456c-b82d-ac691a5a70e0.
            //CognitoSyncManager syncManager = new CognitoSyncManager(
            //    credentials,
            //    new AmazonCognitoSyncConfig
            //    {
            //        RegionEndpoint = RegionEndpoint.USEast2 // Region
            //    }
            //);

            // Create a record in a dataset and synchronize with the server.
            //Dataset dataset = syncManager.OpenOrCreateDataset("myDataset");
            //dataset.OnSyncSuccess += SyncSuccessCallback;
            //dataset.Put("myKey", "myValue");
            //dataset.SynchronizeAsync();
            //******

            //****** Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetTableOperationsExample.html.
            try
            {
                SaveItemAsync();
                CreateTableAsync("ExampleTable");
                WaitUntilTableReadyAsync("ExmapleTable");
                ListTables(100);
                GetTableInformation("ExmapleTable");
                System.Diagnostics.Debug.WriteLine("Status = " + _status);
            }
            catch (AmazonDynamoDBException e) { System.Diagnostics.Debug.WriteLine(e.Message); }
            catch (AmazonServiceException e) { System.Diagnostics.Debug.WriteLine(e.Message); }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); }
            //******

            //****** Adapted from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/getting-started-store-retrieve-data.html.
            _client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);
            _context = new DynamoDBContext(_client);
            //******

            //******Adapted from https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/WorkingWithItemsDocumentClasses.html.
            //Table table = Table.LoadTable(_client, "DynamoDBTest");
            //******

            //****** Adapted from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/getting-started-store-retrieve-data.html.
            Item testItem = new Item()
            {
                Id = 1,
                SaveTimeStamp = DateTime.Now.ToString(),
                Name = "Testing1"
            };
            _context.SaveAsync(testItem);
            //******
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
            System.Diagnostics.Debug.WriteLine("CreateTableAsync() response = " + response);

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
                catch (ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
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

        private async Task SaveItemAsync()
        {
            System.Diagnostics.Debug.WriteLine("\n*** Saving item into DynamoDBTest table ***");

            Item testItem = new Item()
            {
                Id = 1,
                SaveTimeStamp = DateTime.UtcNow.ToString(),
                Name = "Testing"
            };

            //_status = _context.SaveAsync(testItem).Status.ToString();

            //var status = await Task.FromResult(_context.SaveAsync(testItem));
            System.Diagnostics.Debug.WriteLine ("Status = " +  _context.SaveAsync(testItem).Status);
        }
    }
}