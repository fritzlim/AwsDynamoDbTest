using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

//****** Taken from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/setup.html
using Amazon;
//using Amazon.Util;
//******

namespace AwsDynamoDbTest.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif

            //****** Taken from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/setup.html
            var loggingConfig = AWSConfigs.LoggingConfig;
            loggingConfig.LogMetrics = true;
            loggingConfig.LogResponses = ResponseLoggingOption.Always;
            loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;

            AWSConfigs.AWSRegion = "us-east-2";

            AWSConfigs.CorrectForClockSkew = true;
            var offset = AWSConfigs.ClockOffset;
            //******

            LoadApplication(new Core.App());

            return base.FinishedLaunching(app, options);
        }
    }
}
