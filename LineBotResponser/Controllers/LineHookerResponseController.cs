using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using LineBotKit.Client;
using LineBotKit.Common.Model;
using LineBotKit.Common.Model.Message;
using LineBotKit.Common.Model.WebHook;
using LineBotResponser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LineBotResponser.Controllers
{
    public class LineHookerResponseController : ApiController
    {

        private readonly IImgurClient _iImgurClient;
        private readonly IAlbumEndpoint _iAlbumEndpoint;
        private readonly ILineParser _lineParser;
        private readonly ILineClientManager _lineClientManager;
        private readonly IImgUrlParser _imgUrlParser;
        private const string SHUFFLEIMAGE = "抽";

        public LineHookerResponseController(IImgurClient iimgurClient, IAlbumEndpoint iAlbumEndpoint, ILineParser lineParser, ILineClientManager lineClientManager, IImgUrlParser imgUrlParser)
        {
            _iImgurClient = iimgurClient;
            _iAlbumEndpoint = iAlbumEndpoint;
            _lineParser = lineParser;
            _lineClientManager = lineClientManager;
            _imgUrlParser = imgUrlParser;
        }


        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                var hookDataReceived = _lineParser.LineHookResponseParse(Request.Content.ReadAsStringAsync().Result);

                string requestMessage = hookDataReceived.events.FirstOrDefault().message.text;

                if (requestMessage.Equals(SHUFFLEIMAGE))
                {

                    var images = _iAlbumEndpoint.GetAlbumImagesAsync("lllm3").Result.ToList();

                    int imageRandomIndex = RNGRandomer.Next(0, images.Count());

                    string imageLink = _imgUrlParser.GetImageLinkByIndex(images, imageRandomIndex);

                    ResponseItem result = _lineClientManager.ReplyImageMessage(hookDataReceived.events.FirstOrDefault().replyToken, imageLink, imageLink).Result;

                    if (!string.IsNullOrEmpty(result.message)) {
                        return BadRequest(result.message);
                    }

                    return Ok();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
