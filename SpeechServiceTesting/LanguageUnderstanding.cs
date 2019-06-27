using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using System.Threading.Tasks;

namespace SpeechServiceTesting
{
    static class LanguageUnderstanding
    {
        public static Task<Microsoft.Rest.HttpOperationResponse<LuisResult>> GetPrediction(string query, string endpointPredictionkey, string serviceRegion, string appId)
        {
            var credentials = new ApiKeyServiceClientCredentials(endpointPredictionkey);
            var luisClient = new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { });
            luisClient.Endpoint = String.Format("https://{0}.api.cognitive.microsoft.com", serviceRegion);

            // common settings for remaining parameters
            Double? timezoneOffset = null;
            var verbose = true;
            var staging = false;
            var spellCheck = false;
            String bingSpellCheckKey = null;
            var log = true;

            var prediction = new Prediction(luisClient);
            var result = prediction.ResolveWithHttpMessagesAsync(appId, query, timezoneOffset, verbose, staging, spellCheck, bingSpellCheckKey, log);

            // display results
            Console.WriteLine($"");
            Console.WriteLine($"2. LUIS:");
            Console.WriteLine("Top Intent: '{0}'", result.Result.Body.TopScoringIntent.Intent);
            Console.WriteLine("Confidence: '{0}'", result.Result.Body.TopScoringIntent.Score);
            var i = 1;
            foreach (var entity in result.Result.Body.Entities)
            {
                Console.WriteLine("Entity {0} --> {1}:'{2}' (Position {3} to {4})", i, entity.Type, entity.Entity, entity.StartIndex, entity.EndIndex);
            }
            
            return result;
        }
    }
}
