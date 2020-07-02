using System;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using UnityEngine;

namespace DatabaseHelper
{
    public class DynamoDB : MonoBehaviour
    {
        public event Action<int> OnHighScoreUpdated;

        [SerializeField] private string identityPoolId;

        private const string Uid = "UID";
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

        public void PostHighscore(string username, int highScore)
        {
            _context.LoadAsync<Player>(PlayerPrefs.GetString(Uid), result =>
            {
                if (result.Exception != null || result.Result == null)
                {
                    var player = CreatePlayer(username, highScore);
                    _context.SaveAsync(player, dbResult =>
                    {
                        if (dbResult.Exception != null) return;

                        Debug.Log("Successfully added highscore record");
                    });
                    return;
                }

                var dbPlayer = result.Result;
                if (highScore <= dbPlayer.HighScore) return;

                dbPlayer.HighScore = highScore;
                _context.SaveAsync(dbPlayer, dynamoDbResult =>
                {
                    if (dynamoDbResult.Exception != null) return;

                    OnHighScoreUpdated?.Invoke(highScore);
                    Debug.Log("Successfully updated new highscore");
                });
            });
        }

        private Player CreatePlayer(string username, int highScore)
        {
            var playerId = Guid.NewGuid().ToString();
            PlayerPrefs.SetString(Uid, playerId);
            return new Player
                {PlayerID = playerId, Username = username, HighScore = highScore};
        }
    }
}