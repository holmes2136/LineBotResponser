using Imgur.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineBotResponser.Models
{
    public interface IImgUrlParser
    {
        string GetImageLinkByIndex(List<IImage> images, int index);
    }
}