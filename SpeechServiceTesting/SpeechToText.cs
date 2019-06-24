using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SpeechServiceTesting
{
    static class SpeechToText
    {
        public static async Task<SpeechRecognitionResult> RecognizeSpeechAsync(string subscriptionKey, string serviceRegion, string recognitionLanguage, bool logging, string loggingPath, bool detailedOutput)
        {
            var config = SpeechConfig.FromSubscription(subscriptionKey, serviceRegion);
            config.OutputFormat = OutputFormat.Detailed;
            config.SpeechRecognitionLanguage = recognitionLanguage;
            if (logging)
            {
                config.SetProperty(PropertyId.Speech_LogFilename, loggingPath);
                config.EnableAudioLogging();
            }

            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");
                var result = await recognizer.RecognizeOnceAsync();

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    if (detailedOutput)
                    {
                        var i = 1;
                        foreach (var item in result.Best())
                        {
                            Console.WriteLine($"Result: {i}");
                            Console.WriteLine($"Recognized: {item.MaskedNormalizedForm}");
                            Console.WriteLine($"Confidence: {item.Confidence}");
                            i++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Recognized: {result.Text}");
                    }
                    return result;
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
                return null;
            }
        }
    }
}
