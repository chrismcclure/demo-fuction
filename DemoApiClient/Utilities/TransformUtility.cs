using System;

namespace DemoApiClient.Utilities
{
    /// <summary>
    /// Super crazy simple logic.
    /// More complex business logic would be moved to it's own project of engines
    /// and put behind an Interface to isolate it from the client project, make it re-useable by other projects, and make it testable with fakes.
    /// </summary>
    public static class TransformUtility
    {

        /// <summary>
        /// Reverses a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }
    }
}
