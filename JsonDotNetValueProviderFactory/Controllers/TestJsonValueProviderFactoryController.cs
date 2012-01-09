using System.Linq;
using System.Web.Mvc;
using JsonDotNetValueProviderFactoryTestHarness.Models;

namespace JsonDotNetValueProviderFactoryTestHarness.Controllers
{
    public class TestJsonValueProviderFactoryController : Controller
    {
        public TestJsonValueProviderFactoryController()
        {
            /* FOR TEST PURPOSES ONLY change global.asax when using for real */
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonDotNetValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new TestJsonDotNetValueProviderFactoryModel().LoadTestData());
        }

        [HttpPost]
        public ActionResult Index(TestJsonDotNetValueProviderFactoryModel viewModel)
        {
            return Json(viewModel);
        }
    }
}
