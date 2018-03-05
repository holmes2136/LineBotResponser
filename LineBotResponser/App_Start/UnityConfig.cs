using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using LineBotKit.Client;
using LineBotResponser.Controllers;
using LineBotResponser.Models;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace LineBotResponser
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<LineHookerResponseController>();

            container.RegisterType<IImgurClient, ImgurClient>(
                    new InjectionConstructor("imgUrl clientId, imgUrl clientSecret"));

            container.RegisterType<ILineClientManager, LineClientManager>(
                  new InjectionConstructor("Please put your line token here"));

            container.RegisterType<IAlbumEndpoint, AlbumEndpoint>(new ContainerControlledLifetimeManager(),
                                                                 new InjectionConstructor(new ResolvedParameter<IImgurClient>()
                                                                 ));
            container.RegisterType<ILineParser, LineParser>();

            container.RegisterType<IImgUrlParser, ImgUrlParser>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}