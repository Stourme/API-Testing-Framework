namespace Testing.Common
{
    //The Retry class contains methods to support a wait and retry for API responses
    public static class Retry
        {
            private const int DefaultMaxRetries = 10;
            private const int DefaultTimeBetweenRetries = 250;

            /// <summary>
            /// This method will try the excution of a passed method for the amount of times passed the retries parameter
            /// </summary>
            /// <param name="method">Method to retry</param>
            /// <param name="timeBetweenRetries">Time delay bfore next attempt</param>
            /// <param name="retries">Maximum number of retries</param>
            /// <returns>bool</returns>
            public static bool UntilTrueOrTimeOut(Func<bool> method, int timeBetweenRetries = DefaultTimeBetweenRetries, int retries = DefaultMaxRetries)
            {
                for (int attempt = 0; attempt < retries; attempt++)
                {
                    if (method())
                    {
                        return true;
                    }
                    Thread.Sleep(timeBetweenRetries);
                }
                return false;
            }
        }
}
