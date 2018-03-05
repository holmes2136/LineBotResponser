using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineBotResponser;
using LineBotResponser.Models;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Moq;
using LineBotResponser.Controllers;
using System.Net.Http;
using LineBotKit.Client;
using System.Threading.Tasks;
using Imgur.API.Models;
using System.Collections.Generic;
using LineBotKit.Common.Model;
using Newtonsoft.Json;
using LineBotKit.Common.Model.WebHook;
using System.Web.Http;
using System.Web.Http.Results;

namespace LineBotResponser.Test
{
    [TestClass]
    public class LineHookerResponseControllerTest
    {
        private LineHookerResponseController _controller;
        private Mock<ILineParser> _lineParser;
        private Mock<IImgurClient> _imgurClient;
        private Mock<IAlbumEndpoint> _albumEndpoint;
        private Mock<ILineClientManager> _lineClientManager;
        private Mock<IImgUrlParser> _imageUrlParser;

        [TestInitialize]
        public void TestInitialize()
        {
            _imgurClient = new Mock<IImgurClient>();
            _albumEndpoint = new Mock<IAlbumEndpoint>();
            _lineParser = new Mock<ILineParser>();
            _lineClientManager = new Mock<ILineClientManager>();
            _imageUrlParser = new Mock<IImgUrlParser>();
        }


        [TestMethod]
        public void LineHookerResponseController_OKTest()
        {

            string hookResponseJson = "{'events':[{'type':'message','replyToken':'f8b557d463b44762977e6fabb170ae9a','source':{'groupId':null,'userId':'Uf898451626deb866972c76155238d9df','type':'user','roomId':null},'timestamp':1517902017214,'message':{'type':'text','id':'','fileName':null,'fileSize':null,'text':'抽','title':null,'address':null,'latitude':0.0,'longitude':0.0,'packageId':0,'stickerId':0},'postback':null}]}";

            var _controller = new LineHookerResponseController(_imgurClient.Object, _albumEndpoint.Object, _lineParser.Object, _lineClientManager.Object, _imageUrlParser.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(hookResponseJson)
                }
            };

            var hookResponse = JsonConvert.DeserializeObject<LineHookResponse>(hookResponseJson);

            IEnumerable<IImage> images = GenerateImageList();

            _lineParser.Setup(x => x.LineHookResponseParse(It.IsAny<string>())).Returns(hookResponse);

            _albumEndpoint.Setup(x => x.GetAlbumImagesAsync(It.IsAny<string>())).Returns(Task.FromResult(images));

            _lineClientManager.Setup(x => x.ReplyImageMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new ResponseItem() { message=""}));

            _imageUrlParser.Setup(x => x.GetImageLinkByIndex(It.IsAny<List<IImage>>(), It.IsAny<int>())).Returns("https://image.jpg");

            IHttpActionResult result = _controller.Post();

            var posRes = result as OkResult;

            Assert.IsNotNull(posRes);

        }


        private  IEnumerable<IImage> GenerateImageList()
        {
            IImage[] images = new IImage[15];

            return images;
        }
    }
}
