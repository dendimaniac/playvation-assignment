using System;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using UnityEngine;

namespace Flappy_Bird_Style.Scripts.DatabaseHelper
{
    public class DynamoDB : MonoBehaviour
    {
        [SerializeField] private string identityPoolId;

        private CognitoAWSCredentials _credentials;
        private AmazonDynamoDBClient _client;
        private DynamoDBContext _context;

        private void Start()
        {
            UnityInitializer.AttachToGameObject(gameObject);

            AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
            _credentials = new CognitoAWSCredentials(identityPoolId, RegionEndpoint.EUWest2);
            _client = new AmazonDynamoDBClient(_credentials, RegionEndpoint.EUNorth1);
            _context = new DynamoDBContext(_client);
        }

        private void PostHighscore(int highScore)
        {
            var player = new Player
                {PlayerID = Guid.NewGuid().ToString(), Username = "Quan Dao", HighScore = highScore};
            _context.SaveAsync(player, result =>
            {
                if (result.Exception != null) return;

                Debug.Log("Successfully added highscore record");
            });
        }
    }
}