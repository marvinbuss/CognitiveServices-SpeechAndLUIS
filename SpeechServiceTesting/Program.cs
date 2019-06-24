using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SpeechServiceTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            // add App Settings
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            while (true)
            {
                // Instructions
                Console.WriteLine("");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Select a key to continue.");
                Console.WriteLine("- Any key to do STT and NLU");
                Console.WriteLine("- Esc to end");

                // Read Key Input
                var key = Console.ReadKey().Key.ToString();

                // Exit Program if you hit ESC
                if (key.Equals("Escape"))
                {
                    Environment.Exit(0);
                }

                // Logging parameters
                var logging = configuration.GetSection("Logging").GetSection("Logging").Value == "True";
                var loggingPath = configuration.GetSection("Logging").GetSection("LoggingPath").Value;
                var loggingPathSpeech = configuration.GetSection("Logging").GetSection("LoggingPathSpeech").Value;

                // Speech to Text
                var sttServiceRegion = configuration.GetSection("SpeechToTextSettings").GetSection("ServiceRegion").Value;
                var subscriptionKey = configuration.GetSection("SpeechToTextSettings").GetSection("SubscriptionKey").Value;
                var recognitionLanguage = configuration.GetSection("SpeechToTextSettings").GetSection("RecognitionLanguage").Value;
                var detailedOutput = configuration.GetSection("SpeechToTextSettings").GetSection("DetailedOutput").Value == "True";
                var sttResult = SpeechToText.RecognizeSpeechAsync(subscriptionKey, sttServiceRegion, recognitionLanguage, logging, loggingPathSpeech, detailedOutput);
                
                // Language Understanding Service
                var luisServiceRegion = configuration.GetSection("LanguageUnderstandingSettings").GetSection("ServiceRegion").Value;
                var endpointPredictionkey = configuration.GetSection("LanguageUnderstandingSettings").GetSection("EndpointPredictionkey").Value;
                var appId = configuration.GetSection("LanguageUnderstandingSettings").GetSection("AppId").Value;
                var luisResult = LanguageUnderstanding.GetPrediction(sttResult.Result.Text, endpointPredictionkey, luisServiceRegion, appId);

                // Log results
                if (logging)
                {
                    var sttJson = sttResult.Result.ToString();
                    var luisJson = Regex.Replace(luisResult.Result.Response.Content.ReadAsStringAsync().Result.ToString(), @"\t|\n|\r", "");

                    File.AppendAllText(loggingPath, DateTime.Now.ToString() + ": " + sttJson + Environment.NewLine);
                    File.AppendAllText(loggingPath, DateTime.Now.ToString() + ": " + luisJson + Environment.NewLine);
                }
                
            }
            
        }
    }
}
