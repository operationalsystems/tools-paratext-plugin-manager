using System;

namespace PpmMain.UriSigning
{
    /// <summary>
    /// Utilities relevant to signing URIs
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// This method sanitizes a base64 string for use in a URL
        /// </summary>
        /// <param name="bytes">A base64 string to be sanitized.</param>
        /// <returns>A sanitized base64 string</returns>
        public static string ToUrlSafeBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('=', '_')
                .Replace('/', '~');
        }

        /// <summary>
        /// This method gets the <c>TimeSpan</c> for a given unit of time and number of units.
        /// </summary>
        /// <param name="units">The unit of time. (eg "minutes", "seconds")</param>
        /// <param name="numUnits">The number of units.</param>
        /// <returns>The <c>TimeSpan</c> for a given unit of time and number of units.</returns>
        public static TimeSpan GetDuration(string units, string numUnits)
        {
            TimeSpan timeSpanInterval = new TimeSpan();
            switch (units)
            {
                case "seconds":
                    timeSpanInterval = new TimeSpan(0, 0, 0, int.Parse(numUnits));
                    break;
                case "minutes":
                    timeSpanInterval = new TimeSpan(0, 0, int.Parse(numUnits), 0);
                    break;
                case "hours":
                    timeSpanInterval = new TimeSpan(0, int.Parse(numUnits), 0, 0);
                    break;
                case "days":
                    timeSpanInterval = new TimeSpan(int.Parse(numUnits), 0, 0, 0);
                    break;
                default:
                    Console.WriteLine("Invalid time units;" +
                       "use seconds, minutes, hours, or days");
                    break;
            }
            return timeSpanInterval;
        }


    }
}
