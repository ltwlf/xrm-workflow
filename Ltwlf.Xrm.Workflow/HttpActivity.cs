using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ltwlf.Xrm.Workflow
{
    public class HttpActivity : CodeActivity
    {
        [RequiredArgument]
        [Input("Url")]
        public InArgument<string> Url { get; set; }

        [RequiredArgument]
        [Input("Method")]
        [Default("Post")]
        public InArgument<string> Method { get; set; }

        [Input("Body")]
        [Default("")]
        public InArgument<string> Body { get; set; }

        [Input("Header")]
        [Default("")]
        public InArgument<string> Header { get; set; }

        [Output("Response Body")]
        [Default("")]
        public OutArgument<string> ResponseBody { get; set; }

        [Output("response Headers")]
        [Default("")]
        public OutArgument<string> ResponseHeader { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var url = Url.Get(context);
            var method = Method.Get(context);
            var header = Header.Get(context);
            var body = Body.Get(context);

            var http = new HttpClient();
            
            if (!string.IsNullOrEmpty(header))
            {
                var lines = header.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                lines.ToList().ForEach(h => {
                    var split = h.Split(':');
                    http.DefaultRequestHeaders.TryAddWithoutValidation(split[0].Trim(), split[1].Trim());
                });     

            }

            if (method.ToLower() == "post")
            {
                var result = http.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).Result;
                ResponseBody.Set(context, result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
