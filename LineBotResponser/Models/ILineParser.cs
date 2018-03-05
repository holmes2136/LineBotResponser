using LineBotKit.Common.Model.WebHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotResponser.Models
{
    public interface ILineParser
    {
         LineHookResponse LineHookResponseParse(string hookResponseJson);
    }
}