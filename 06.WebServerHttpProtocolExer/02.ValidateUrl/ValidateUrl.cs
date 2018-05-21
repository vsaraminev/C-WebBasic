namespace ValidateUrl
{
    using System;
    using System.Net;

    public class ValidateUrl
    {
        private const string InvalidUrl = "Invalid URL";

        public static void Main()
        {
            var input = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(input);

            var uri = new Uri(decodedUrl);

            var protocol = uri.Scheme;

            var host = uri.Host;
          
            var port = uri.Port;

            if ((string.IsNullOrEmpty(protocol) || string.IsNullOrEmpty(host)) || (port != 80 && port != 443))
            {
                Console.WriteLine(InvalidUrl);
                return;
            }

            var path = uri.AbsolutePath;

            var queryString = uri.Query;

            var fragment = uri.Fragment;
            
            Console.WriteLine($"Protocol: {protocol}");
            Console.WriteLine($"Host: {host}");
            Console.WriteLine($"Port: {port}");
            Console.WriteLine($"Path: {path}");
            if (!string.IsNullOrEmpty(queryString))
            {
                Console.WriteLine($"Query: {queryString}");
            }

            if (!string.IsNullOrEmpty(fragment))
            {
                Console.WriteLine($"Fragment: {fragment}");
            }
        }
    }
}
