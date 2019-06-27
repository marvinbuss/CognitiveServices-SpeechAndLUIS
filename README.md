# CognitiveServices-Speech and LUIS testing tool

This small and lightweight tool can be used to quickly test the capabilities of the Speech and LUIS Cognitive Services. The application provides feedback about the Speech to Text conversion and the top intent that was identified by LUIS. Is also returns a list of all entities that were extracted from the utterance.

Also, this tool can be used to quickly add sample utterances to your LUIS model via voice. This is generally much quicker than typing sample utterances in LUIS. The console application is written in .NET Core 2.2 and is adaptible by changing the settings and subscription keys in the `appsettings.json` file, even after compiling the solution.

# Compiled version
If you do not want to compile the solution yourself, you can just download a compiled version for Windows x64 Systems. Just hit the download button. If you download this version, you will still be able to change the settings of your application. Download the solution by using the follwing link: [Download Link](compiled/win-x64.zip)

# Setup of Speech and LUIS
Before we start, make sure that you have access to the [Speech Service](https://azure.microsoft.com/en-us/services/cognitive-services/speech-services) as well as [LUIS](https://eu.luis.ai), which are part of the Microsoft Cognitive Services. I you are not sure, you can create a new resource for each of them in the Azure Portal:
- [Speech](https://portal.azure.com/#create/Microsoft.CognitiveServicesSpeechServices)
- [LUIS](https://ms.portal.azure.com/#create/Microsoft.CognitiveServicesLUIS)
Select `West Europe`, if you are not sure which region to choose.

**NOTE:** When deploying the services on Azure, please take note of the selected region. This is very important for later use of the app. Once the deployment was successful, please also take note of the subscription keys of the Cognitive Services.

# Changing Settings of the Application
In this section, I will describe how to use the application and adjust the settings. If you downloaded the compiled version (`win-x64.zip`), you should first unzip the archive. Afterwards, you should see the following files in the unpacked folder:
<img src="pictures/zipFiles.png" alt="Files in the zip-Archive" width="250"/>

Next, open the `appsettings.json` with Notepad or any other preferred tool. In this file, you can adjust the settings, so that the right speech and LUIS model is used. There are three important sections in this file: `Logging`, `SpeechToTextSettings` and `LanguageUnderstandingSettings`.

## Logging
The application writes log-files in the background. This enables you to analyze the results of the Speech and LUIS service later on. The parameters in this section will have the following effect:

- `Logging`: When `true` logging is enabled and if set to `false` logging is disabled.
- `LoggingPath`: With this parameter you can adjust and change the name and path of the log-file that saves the results of the Speech and LUIS service. There is no need to change this value.
- `LoggingPathSpeech`: With this parameter you can adjust and change the name and path of the log-file that saves additional data of the Speech service. There is no need to change this value.

## SpeechToTextSettings
This section contains all the settings for the Speech to Text functionality. The parameters in this section will have the following effect:

- `ServiceRegion`: Set this parameter to the region to which you deplyoed your Cognitive Service.
- `SubscriptionKey`: Set this parameter to your Speech Subscription Key. You can find the key in the Azure Portal.
- `RecognitionLanguage`: Specifies the name of spoken language to be recognized in BCP-47 format. Set this value to the language that you would like to use for STT. A more comprehensive list of possible values can be found here: https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/language-support
- `DetailedOutput`: When `true` the console application also returns information about alternative transcriptions with lower confidence. This feature only works for a few languages such as `en-US`.

## LanguageUnderstandingSettings
This section contains all the settings for the Natural Language Understanding functionality. The parameters in this section will have the following effect:

- `ServiceRegion`: Set this parameter to the region to which you deplyoed your Cognitive Service.
- `EndpointPredictionkey`: Set this parameter to the Endpoint Prediction Key of your LUIS app. This key can be found in the app if you navigate to Manage > Keys and Endpoints > Key 1.
- `AppId`: Set this parameter to the App ID of your LUIS app. This ID can be found in the app if you navigate to Manage > Application Information > Application ID.

Once you have set all values, you can save and close the `appsettings.json` file. Now you can open the console application by double clicking on the `SpeechServiceTesting.exe`. This will open the console application. Here you have two options:
- Click `Esc` to end the application
- Click any other button on the keyboard to start the speech recognition.
<img src="pictures/ConsoleApp.png" alt="Screenshot of the Console Application" width="250"/>
