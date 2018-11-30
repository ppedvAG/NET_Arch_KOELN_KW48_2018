using Autofac;
using ppedv.TastyMoon.DomainModel.Contracts;
using ppedv.TastyMoon.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ppedv.TastyMoon.UI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BuildContainer();
        }
        public static Autofac.IContainer Container { get; private set; }

        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<Data.EF.EfRepository>().As<IRepository>();
            //builder.RegisterType<FakeBeeper>().As<IBeeper>();

            //pfad muss absolut sein
            string path = Path.GetFullPath(@"C:\Users\ar2\Source\Repos\ppedvAG\NET_Arch_KOELN_KW48_2018\ppedv.TastyMoon\ppedv.TastyMoon.Data.EF\bin\Debug\ppedv.TastyMoon.Data.EF.dll");

            //Assemble laden
            Assembly extAss = Assembly.LoadFile(path);

            //erstbeste Klasse mit IBeeper wird verwendet
            builder.RegisterAssemblyTypes(extAss).As<IRepository>();
            builder.RegisterType<Core>().UsingConstructor(typeof(IRepository));
            Container = builder.Build();
        }
    }
}
