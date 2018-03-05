using LineBotKit.Common.Model.WebHook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotResponser.Models
{
    public class LineParser:ILineParser
    {
        public LineHookResponse LineHookResponseParse(string hookResponseJson) {
            return JsonConvert.DeserializeObject<LineHookResponse>(hookResponseJson);
        }
    }
}