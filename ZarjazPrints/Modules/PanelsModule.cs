using Nancy;

namespace ZarjazPrints.Modules
{
    public class PanelsModule : NancyModule
    {
        public PanelsModule()
        {
            Get["/panels"] = parameters =>
            {
                return View["panels"];
            };
            Get["/panel/{id}"] = parameters =>
            {
                return View["panel"];
            };
        }
    }
}