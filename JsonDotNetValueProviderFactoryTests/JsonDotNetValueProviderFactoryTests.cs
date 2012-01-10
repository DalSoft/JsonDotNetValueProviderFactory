using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JsonDotNetValueProviderFactoryTestHarness;
using Moq;
using NUnit.Framework;

namespace JsonDotNetValueProviderFactoryTests
{
    [TestFixture]
    public class JsonDotNetValueProviderFactoryTests
    {
        private Mock<HttpContextBase> _httpContextBaseFake;
        private Mock<HttpRequestBase> _requestFake;
        private ControllerContext _controllerContext;

        [SetUp]
        public void SetUp()
        {
            _httpContextBaseFake = new Mock<HttpContextBase>();
            _requestFake = new Mock<HttpRequestBase>();
            _controllerContext = new ControllerContext(new RequestContext(_httpContextBaseFake.Object, new RouteData()), new Mock<Controller>().Object);
        }

        [Test]
        public void GetValueProvider_NullControllerContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDotNetValueProviderFactory().GetValueProvider(null));
        }

        [Test]
        public void GetValueProvider_ContentTypeDoesNotStartsWithApplicationSlashJson_ReturnsNull()
        {
            _requestFake.Setup(x => x.ContentType).Returns("text/html");
            _httpContextBaseFake.Setup(x => x.Request).Returns(_requestFake.Object);

            var jsonDotNetValueProvider = new JsonDotNetValueProviderFactory().GetValueProvider(_controllerContext);
            Assert.IsNull(jsonDotNetValueProvider);
        }
        
        [Test]
        public void GetValueProvider_InputStreamIsEmpty_ReturnsNull()
        {
            _requestFake.Setup(x => x.ContentType).Returns("application/json");
            _requestFake.Setup(x => x.InputStream).Returns(Stream.Null);
            _httpContextBaseFake.Setup(x => x.Request).Returns(_requestFake.Object);

            var jsonDotNetValueProvider = new JsonDotNetValueProviderFactory().GetValueProvider(_controllerContext);
            Assert.IsNull(jsonDotNetValueProvider);
        }

        [Test]
        public void GetValueProvider_InputStreamContainsVaildJson_ReturnsCorrectlyMappedDictionaryValueProviderFromDeserializedJson()
        {
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes("{value1:'MyString',value2:100}")))
            {
                _requestFake.Setup(x => x.ContentType).Returns("application/json");
                _requestFake.Setup(x => x.InputStream).Returns(inputStream);
                _httpContextBaseFake.Setup(x => x.Request).Returns(_requestFake.Object);

                var jsonDotNetValueProvider = new JsonDotNetValueProviderFactory().GetValueProvider(_controllerContext);
                Assert.IsInstanceOf<DictionaryValueProvider<object>>(jsonDotNetValueProvider);
                Assert.AreEqual(jsonDotNetValueProvider.GetValue("value1").RawValue, "MyString");
                Assert.AreEqual(jsonDotNetValueProvider.GetValue("value2").RawValue, 100);
            }
        }

        [Test]
        public void GetValueProvider_InputStreamContainsVaildJson_ReturnsCorrectlyMappedDictionaryValueProviderFromDeserializedJsonWithNestedValues()
        {
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes("{ value1:'MyString', value2:100, nestedValue: { nestedValue1:'MyString2', nestedValue2:200 } }")))
            {
                _requestFake.Setup(x => x.ContentType).Returns("application/json");
                _requestFake.Setup(x => x.InputStream).Returns(inputStream);
                _httpContextBaseFake.Setup(x => x.Request).Returns(_requestFake.Object);
                
                var jsonDotNetValueProvider = new JsonDotNetValueProviderFactory().GetValueProvider(_controllerContext);
                Assert.IsInstanceOf<DictionaryValueProvider<object>>(jsonDotNetValueProvider);
                dynamic nestedValue = jsonDotNetValueProvider.GetValue("nestedValue").RawValue;
                Assert.AreEqual(nestedValue.nestedValue1, "MyString2");
                Assert.AreEqual(nestedValue.nestedValue2, 200);
            }
        }
    }
}
