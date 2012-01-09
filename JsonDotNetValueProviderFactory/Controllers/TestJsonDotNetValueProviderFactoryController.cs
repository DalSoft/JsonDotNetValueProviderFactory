using System.Linq;
using System.Web.Mvc;
using JsonDotNetValueProviderFactoryTestHarness.Models;
using Newtonsoft.Json;

namespace JsonDotNetValueProviderFactoryTestHarness.Controllers
{
    public class TestJsonDotNetValueProviderFactoryController : Controller
    {
        public TestJsonDotNetValueProviderFactoryController()
        {
            /* FOR TEST PURPOSES ONLY change global.asax when using for real */
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonDotNetValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());
        }

        [HttpPost]
        public ActionResult Index(TestJsonDotNetValueProviderFactoryModel viewModel)
        {
            //Json ActionResult uses JavaScriptSerializer with the same limitation. Use Content result and JsonConvert.SerializeObject so we control the serialization.
            return Content(JsonConvert.SerializeObject(viewModel, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), "application/json");
        }

    }
}
