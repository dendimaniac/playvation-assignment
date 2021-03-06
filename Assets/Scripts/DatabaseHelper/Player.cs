﻿using Amazon.DynamoDBv2.DataModel;

namespace DatabaseHelper
{
    [DynamoDBTable("Highscore")]
    public class Player
    {
        [DynamoDBHashKey]
        public string PlayerID { get; set; }

        [DynamoDBProperty]
        public string Username { get; set; }

        [DynamoDBProperty]
        public int HighScore { get; set; }
    }
}