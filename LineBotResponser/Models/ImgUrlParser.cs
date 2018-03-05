using Imgur.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotResponser.Models
{
    public class ImgUrlParser: IImgUrlParser
    {
        public string GetImageLinkByIndex(List<IImage> images, int index) {
            return images[index].Link;
        }
    }
}