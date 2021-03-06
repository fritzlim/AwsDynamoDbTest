﻿//Code in this file is adapted from https://forums.xamarin.com/discussion/81722/aws-dynamodb
using Amazon.DynamoDBv2.DataModel;
using System;

namespace AwsDynamoDbTest.Core
{
    [DynamoDBTable("DynamoDBTest")]
    public class Item
    {
        //[DynamoDBHashKey]
        //public string User { get; set; }

        //[DynamoDBRangeKey]
        //public string Password { get; set; }

        //[DynamoDBProperty]
        //public string Email { get; set; }

        //[DynamoDBProperty]
        //public string Name { get; set; }

        //[DynamoDBProperty]
        //public string LastName { get; set; }

        //[DynamoDBProperty]
        //public string BirthDay { get; set; }

        [DynamoDBHashKey]
        public string Id { get; set; }

        //[DynamoDBRangeKey]
		[DynamoDBProperty]
        public string SavedTimeStamp { get; set; }

        //[DynamoDBProperty]
		[DynamoDBGlobalSecondaryIndexHashKey]
        public string Name { get; set; }

        [DynamoDBProperty]
		//[DynamoDBGlobalSecondaryIndexHashKey]
        public string Email { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        //[DynamoDBProperty]
        //public string LastName { get; set; }

        //[DynamoDBProperty]
        //public string BirthDay { get; set; }

    }
}
