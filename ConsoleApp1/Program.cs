using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://enc6t8fivik7b.x.pipedream.net";
            var method = "Post";
            var header = "content-type:application/json";
            var body = "{\"test\":\"test\"}";

            var http = new HttpClient();


            if (!string.IsNullOrEmpty(header))
            {
                var lines = header.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                lines.ToList().ForEach(h => {
                    var split = h.Split(':');
                    http.DefaultRequestHeaders.TryAddWithoutValidation(split[0].Trim(), split[1].Trim());
                });

                if (method.ToLower() == "post")
                {
                    var result = http.PostAsync(url, new StringContent(body)).Result;
                    Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                    
                }

            }
        }
    }
}
