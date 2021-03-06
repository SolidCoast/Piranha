﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebPiranha.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.WebPages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.ServiceModel.Activation;
    using System.Threading;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Piranha.Extend;
    using Piranha.Models;
    using Piranha.Models.Manager.PageModels;
    using Piranha.Models.Manager.TemplateModels;
    using Piranha.Mvc.ModelBinders;
    using Piranha.Rest;
    using Piranha.Web;
    using Piranha.WebPages.RequestHandlers;

    /// <summary>
    /// </summary>
    public static class WebPiranha
    {
        #region Static Fields

        /// <summary>
        /// The registered cultures prefixes.
        /// </summary>
        internal static Dictionary<string, string> CulturePrefixes = new Dictionary<string, string>();

        /// <summary>
        /// The registered cultures.
        /// </summary>
        internal static Dictionary<string, CultureInfo> Cultures = new Dictionary<string, CultureInfo>();

        /// <summary>
        /// The different request handlers.
        /// </summary>
        internal static Dictionary<string, RequestHandlerRegistration> Handlers = new Dictionary<string, RequestHandlerRegistration>();
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current application root.
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string root = HttpContext.Current.Request.ApplicationPath;
                if (!root.EndsWith("/"))
                {
                    root += "/";
                }

                return root;
            }
        }

        /// <summary>
        /// Gets/sets weather to use prefixless permalinks.
        /// </summary>
        public static bool PrefixlessPermalinks { get; set; }
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Handles the URL Rewriting for the application
        /// </summary>
        /// <param name="context">
        /// Http context
        /// </param>
        public static void BeginRequest(HttpContext context)
        {
            string path = context.Request.Path.Substring(context.Request.ApplicationPath.Length > 1 ? context.Request.ApplicationPath.Length : 0);

            string[] args = path.Split(new[] { '/' }).Subset(1);

            if (args.Length > 0)
            {
                int pos = 0;

                // Ensure database
                if (args[0] == string.Empty && SysParam.GetByName("SITE_VERSION") == null)
                {
                    context.Response.Redirect(url: "~/Manager");
                }

                // Check for culture prefix
                if (Cultures.ContainsKey(args[0]))
                {
                    Thread.CurrentThread.CurrentUICulture = Cultures[args[0]];
                    pos = 1;
                }
                else
                {
                    var def = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization");
                    if (def != null)
                    {
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(def.UICulture);
                    }
                }

                bool handled = false;

                // Find the correct request handler
                foreach (RequestHandlerRegistration hr in Handlers.Values)
                {
                    if (hr.UrlPrefix.ToLower() == args[pos].ToLower())
                    {
                        if (hr.Id != "PERMALINK" || !PrefixlessPermalinks)
                        {
                            // Execute the handler
                            hr.Handler.HandleRequest(context, args.Subset(pos + 1));
                            handled = false;
                            break;
                        }
                    }
                }

                // If no handler was found and we are using prefixless permalinks, 
                // route traffic to the permalink handler.
                if (!handled && PrefixlessPermalinks)
                {
                    if (Permalink.GetByName(new Guid("8FF4A4B4-9B6C-4176-AAA2-DB031D75AC03"), args[0]) != null)
                    {
                        var handler = new PermalinkHandler();
                        handler.HandleRequest(context, args);
                    }
                }
            }
        }

        /// <summary>
        /// Get's the current culture prefix.
        /// </summary>
        /// <returns>The culture prefix</returns>
        public static string GetCulturePrefix()
        {
            return CulturePrefixes.ContainsKey(CultureInfo.CurrentUICulture.Name) ? CulturePrefixes[CultureInfo.CurrentUICulture.Name] + "/" : string.Empty;
        }

        /// <summary>
        /// Gets the public site url.
        /// </summary>
        /// <returns>The url</returns>
        public static string GetSiteUrl()
        {
            HttpContext context = HttpContext.Current;
            string url = "http://" + context.Request.Url.DnsSafeHost + (!context.Request.Url.IsDefaultPort ? ":" + context.Request.Url.Port : string.Empty) + context.Request.ApplicationPath;

            if (url.EndsWith("/"))
            {
                return url.Substring(0, url.Length - 1);
            }

            return url;
        }

        /// <summary>
        /// Gets the current url prefix used for the given handler id.
        /// </summary>
        /// <param name="id">
        /// The handler id
        /// </param>
        /// <returns>
        /// The url prefix
        /// </returns>
        public static string GetUrlPrefixForHandlerId(string id)
        {
            if (Handlers.ContainsKey(id.ToUpper()))
            {
                return Handlers[id.ToUpper()].UrlPrefix;
            }

            return string.Empty;
        }

        /// <summary>
        /// Handles current UI culture.
        /// </summary>
        /// <param name="context">
        /// The http context
        /// </param>
        public static void HandleCulture(HttpContext context)
        {
            // NOTE: This code will fail completely in the manager view as accessing the request 
            // collection triggers the form data validation.
            try
            {
                if (context.Request.HttpMethod.ToUpper() == "POST")
                {
                    if (!string.IsNullOrEmpty(context.Request["lang"]))
                    {
                        context.Session["lang"] = context.Request["lang"];
                    }
                }

                if (context.Session != null && context.Session["lang"] != null)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo((string)context.Session["lang"]);
                }
            }
            catch {}
        }

        /// <summary>
        /// Initializes the webb app.
        /// </summary>
        public static void Init()
        {
            // Register virtual path provider for the manager area. This part includes a nasty hack for 
            // precompiled sites due to Microsofts implementation in the .NET framework. See
            // http://sunali.com/2008/01/09/virtualpathprovider-in-precompiled-web-sites/
            // for more information on the issue
            // PropertyInfo pc = typeof(BuildManager).GetProperty("IsPrecompiledApp", BindingFlags.NonPublic | BindingFlags.Static) ;
            // if (pc != null && (bool)pc.GetValue(null, null)) {
            // // This is a precompiled application, bend the framework a bit.
            // HostingEnvironment instance = (HostingEnvironment)typeof(HostingEnvironment).InvokeMember("_theHostingEnvironment", 
            // BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null) ;
            // if (instance == null)
            // throw new NullReferenceException("Can't get the current hosting environment") ;
            // MethodInfo m = typeof(HostingEnvironment).GetMethod("RegisterVirtualPathProviderInternal", BindingFlags.NonPublic | BindingFlags.Static) ;
            // if (m == null)
            // throw new NullReferenceException("Can't get the RegisterVirtualPathProviderInternal method") ;
            // m.Invoke(instance, new object[] { (VirtualPathProvider)new Piranha.Web.ResourcePathProvider() });
            // } else {
            HostingEnvironment.RegisterVirtualPathProvider(new ResourcePathProvider());

            // }

            // Register the basic account route
            RouteTable.Routes.MapRoute("Account", "account/{action}", new { controller = "auth", action = "index" }, new[] { "Piranha.Web" });

            // This will trigger the manager area registration
            AreaRegistration.RegisterAllAreas();

            // Register handlers
            RegisterDefaultHandlers();

            // Reset template cache
            TemplateCache.Clear();

            // Initialize the extension manager
            ExtensionManager.Init();

            // Register json deserialization for post data
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }

        /// <summary>
        /// Initializes the manager app.
        /// </summary>
        /// <param name="context">
        /// </param>
        public static void InitManager(AreaRegistrationContext context)
        {
            // Register manager routing
            context.MapRoute("Manager", "manager/{controller}/{action}/{id}", new { area = "manager", controller = "account", action = "index", id = UrlParameter.Optional });

            context.MapRoute("Content", "manager/{controller}/{action}/{id}", new { area = "manager", controller = "content", action = "index", id = UrlParameter.Optional });

            context.MapRoute("Page", "manager/{controller}/{action}/{id}", new { area = "manager", controller = "page", action = "index", id = UrlParameter.Optional });

            // Register filters & binders
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterBinders();
        }

        /// <summary>
        /// Registers all of the default rest wcf services.
        /// </summary>
        public static void InitServices()
        {
            RouteTable.Routes.Add("REST_CATEGORY", new ServiceRoute("rest/category", new WebServiceHostFactory(), typeof(CategoryService)));
            RouteTable.Routes.Add("REST_SITEMAP", new ServiceRoute("rest/sitemap", new WebServiceHostFactory(), typeof(SitemapServices)));
            RouteTable.Routes.Add("REST_PAGE", new ServiceRoute("rest/page", new WebServiceHostFactory(), typeof(PageService)));
            RouteTable.Routes.Add("REST_POST", new ServiceRoute("rest/post", new WebServiceHostFactory(), typeof(PostService)));
            RouteTable.Routes.Add("REST_CONTENT", new ServiceRoute("rest/content", new WebServiceHostFactory(), typeof(ContentService)));
            RouteTable.Routes.Add("REST_CHANGES", new ServiceRoute("rest/changes", new WebServiceHostFactory(), typeof(ChangeService)));
        }

        /// <summary>
        /// Registers the culture to the given prefix.
        /// </summary>
        /// <param name="urlprefix">
        /// The url prefix.
        /// </param>
        /// <param name="culture">
        /// The culture
        /// </param>
        public static void RegisterCulture(string urlprefix, CultureInfo culture)
        {
            Cultures.Add(urlprefix, culture);
            CulturePrefixes.Add(culture.Name, urlprefix);
        }

        /// <summary>
        /// Registers all of the default request handlers.
        /// </summary>
        public static void RegisterDefaultHandlers()
        {
            RegisterHandler(string.Empty, "STARTPAGE", new PermalinkHandler());
            RegisterHandler("home", "PERMALINK", new PermalinkHandler());
            RegisterHandler("draft", "DRAFT", new DraftHandler());
            RegisterHandler("media", "CONTENT", new ContentHandler());
            RegisterHandler("thumb", "THUMBNAIL", new ThumbnailHandler());
            RegisterHandler("upload", "UPLOAD", new UploadHandler());
            RegisterHandler("archive", "ARCHIVE", new ArchiveHandler());
            RegisterHandler("rss", "RSS", new RssHandler());
            RegisterHandler("sitemap.xml", "SITEMAP", new SitemapHandler());
        }

        /// <summary>
        /// Registers the given.
        /// </summary>
        /// <param name="urlprefix">
        /// The url prefix
        /// </param>
        /// <param name="id">
        /// The handler id
        /// </param>
        /// <param name="handler">
        /// The actual handler
        /// </param>
        public static void RegisterHandler(string urlprefix, string id, IRequestHandler handler)
        {
            Handlers.Add(id.ToUpper(), new RequestHandlerRegistration { UrlPrefix = urlprefix, Id = id, Handler = handler });
        }

        /// <summary>
        /// Removes the handler with the given id.
        /// </summary>
        /// <param name="id">
        /// The handler id
        /// </param>
        public static void RemoveHandler(string id)
        {
            if (Handlers.ContainsKey(id.ToUpper()))
            {
                Handlers.Remove(id.ToUpper());
            }
        }

        /// <summary>
        /// Clears all of the currently registered handlers.
        /// </summary>
        public static void ResetHandlers()
        {
            Handlers.Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers all custom binders.
        /// </summary>
        private static void RegisterBinders()
        {
            ModelBinders.Binders.Add(typeof(EditModel), new EditModel.Binder());
            ModelBinders.Binders.Add(typeof(Models.Manager.PostModels.EditModel), new Models.Manager.PostModels.EditModel.Binder());
            ModelBinders.Binders.Add(typeof(PageEditModel), new PageEditModel.Binder());
            ModelBinders.Binders.Add(typeof(PostEditModel), new PostEditModel.Binder());
            ModelBinders.Binders.Add(typeof(IExtension), new IExtensionBinder());
        }

        /// <summary>
        /// Registers all global filters.
        /// </summary>
        /// <param name="filters">
        /// The current filter collection
        /// </param>
        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #endregion
    }
}