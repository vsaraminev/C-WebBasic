namespace RequestParser
{
    using System;
    using System.Collections.Generic;

    public class RequestParser
    {
        public static void Main()
        {
            var input = Console.ReadLine();

            var pathMethods = new Dictionary<string, HashSet<string>>();

            while (input != "END")
            {
                var parts = input.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                var path = $"/{parts[0]}";
                var method = parts[1];

                if (!pathMethods.ContainsKey(path))
                {
                    pathMethods[path] = new HashSet<string>();
                }

                pathMethods[path].Add(method);

                input = Console.ReadLine();
            }

            var request = Console.ReadLine();

            var requestParts = request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var reqMethod = requestParts[0];

            var reqUrl = requestParts[1];

            var protocol = requestParts[2];

            var statusCode = 400;

            var statusCodeMessage = "Not Found";

            if (pathMethods.ContainsKey(reqUrl) && pathMethods[reqUrl].Contains(reqMethod.ToLower()))
            {
                statusCode = 200;
                statusCodeMessage = "OK";
            }

            Console.WriteLine($"{protocol} {statusCode} {statusCodeMessage}");
            Console.WriteLine($"Content-Length: {statusCodeMessage.Length}");
            Console.WriteLine($"Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine(statusCodeMessage);
        }
    }
}
