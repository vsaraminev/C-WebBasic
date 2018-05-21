namespace UrlDecode
{
    using System;
    using System.Net;

    public class UrlDecode
    {
        public static void Main()
        {
            var input = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(input);

            Console.WriteLine(decodedUrl);
        }
    }
}
