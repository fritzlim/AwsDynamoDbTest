using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//****** Taken from https://docs.aws.amazon.com/mobile/sdkforxamarin/developerguide/setup.html
using Amazon;
//using Amazon.Util;
//******

namespace AwsDynamoDbTest.Droid
{
    [Activity(Label = "AwsDynamoDbTest.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

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

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new Core.App());
        }
    }
}
