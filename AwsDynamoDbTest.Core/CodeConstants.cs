using System;
using Xamarin.Forms;

namespace AwsDynamoDbTest.Core
{
    public static class CodeConstants
    {
        public struct App
        {
            public const string NAME = "AwsDynamoDbTest";
        }

        public struct AWS
        {
            /// <summary>
            /// The identity pool ID.
            /// </summary>
            public const string IDENTITY_POOL_ID = "us-east-2:fc497215-b4e7-48f6-b4b4-ae4618026857"; // Identity pool ID for DynamoDbIdentityPool
        }

        public struct DateTime
        {
            /// <summary>
            /// The easily-readable local timestamp format as e.g. Mon 15 Aug 2016 12:50:14 am. Adapted from http://www.dotnetperls.com/datetime-format
            /// </summary>
            public const string LOCAL_TIMESTAMP_FORMAT = "ddd d MMM yyyy h:mm:ss tt";

            /// <summary>
            /// The timestamp with offset, as Android format (i.e. 24-hour time), e.g. 2018/03/15 16:57:28 +08:00. Adapted from https://www.dotnetperls.com/datetime-format and https://msdn.microsoft.com/en-us/library/bb351892(v=vs.110).aspx.
            /// </summary>
            public const string TIMESTAMP_WITH_OFFSET = "yyyy/MM/dd HH:mm:ss zzz";

            /// <summary>
            /// The timestamp with offset but without spaces nor separators, as Android format (i.e. 24-hour time), e.g. 20180315165728+08:00. Adapted from https://www.dotnetperls.com/datetime-format and https://msdn.microsoft.com/en-us/library/bb351892(v=vs.110).aspx.
            /// </summary>
            public const string TIMESTAMP_WITH_OFFSET_NO_SPACES_NO_SEPARATORS = "yyyyMMddHHmmsszzz";
        }

        public struct ImageEntry
        {
            public struct Colour
            {
                /// <summary>
                /// The text colour of the ImageEntry.
                /// </summary>
                public static Color TEXT = Color.Black;

                /// <summary>
                /// The placeholder text colour of the ImageEntry.
                /// </summary>
                public static Color PLACEHOLDER = Color.Black;

                public static Color LINE = Color.Black;
            }

            public struct Dimensions
            {
                public struct HeightRequest
                {
                    /// <summary>
                    /// The requested height of the IamgeEntry on Android.
                    /// </summary>
                    public static int ANDROID = 40;

                    /// <summary>
                    /// The requested height of the IamgeEntry on iOS.
                    /// </summary>
                    public static int IOS = 30;
                }

                /// <summary>
                /// The height of the image in the ImageEntry.
                /// </summary>
                public static int IMAGE_HEIGHT = 20;

                /// <summary>
                /// The width of the image in the ImageEntry.
                /// </summary>
                public static int IMAGE_WIDTH = 20;
            }

            public struct Font
            {
                public struct Size
                {
                    /// <summary>
                    /// The font size of the ImageEntry on the Android platform.
                    /// </summary>
                    public static double ANDROID = Device.GetNamedSize(NamedSize.Small, typeof(ImageEntry));

                    /// <summary>
                    /// The font size of the ImageEntry on the iOS platform.
                    /// </summary>
                    public static double IOS = Device.GetNamedSize(NamedSize.Small, typeof(ImageEntry));
                }
            }
        }

    }
}
