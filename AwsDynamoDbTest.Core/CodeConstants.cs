using System;
namespace AwsDynamoDbTest.Core
{
    public static class CodeConstants
    {
        public struct DateTime
        {
            /// <summary>
            /// The easily-readable local timestamp format as e.g. Mon 15 Aug 2016 12:50:14 am. Adapted from http://www.dotnetperls.com/datetime-format
            /// </summary>
            public const string LOCAL_TIMESTAMP_FORMAT = "ddd d MMM yyyy h:mm:ss tt";

            /// <summary>
            /// The timestamp with offset, as Android format (i.e. 24-hour time) ectly. Adapted from https://www.dotnetperls.com/datetime-format and https://msdn.microsoft.com/en-us/library/bb351892(v=vs.110).aspx.
            /// </summary>
            public const string TIMESTAMP_WITH_OFFSET = "yyyy/MM/dd HH:mm:ss zzz";
        }
    }
}
