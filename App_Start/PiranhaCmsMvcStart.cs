using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Piranha.App_Start.PiranhaCmsMvcStart), "Start")]

namespace Piranha.App_Start {
    using System.Reflection;

    using Piranha.Mvc;

    public static class PiranhaCmsMvcStart {
        public static void Start() {
            var engine = new PirahnaCmsViewEngine(typeof(PiranhaCmsMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
