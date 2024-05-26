using System.Data;
using System.Net;
using System.Text;
using Moq;
using ValuteConverter;


namespace ValuteConverterTests
{
    [TestClass]
    public class SoapRequestTests
    {
        [TestMethod]
        public async Task GetExchangeRateOnDateAsync_SuccessfulRequest_ReturnsDataTable()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var expectedDataTable = new DataTable();

            var soapRequest = new SoapRequest();

            // Mocking HttpWebRequest and HttpWebResponse
            var httpWebResponseMock = new Mock<HttpWebResponse>();
            httpWebResponseMock.Setup(response => response.GetResponseStream()).Returns(() => new MemoryStream(Encoding.UTF8.GetBytes("<schema></schema><CursValute></CursValute>")));

            var httpWebRequestMock = new Mock<HttpWebRequest>();
            httpWebRequestMock.Setup(request => request.GetResponseAsync()).ReturnsAsync(httpWebResponseMock.Object);

            var webRequestCreateMock = new Mock<IWebRequestCreate>();
            webRequestCreateMock.Setup(create => create.Create(It.IsAny<Uri>())).Returns(httpWebRequestMock.Object);

            WebRequest.RegisterPrefix("http://", webRequestCreateMock.Object);

            // Act
            var result = await soapRequest.GetExchangeRateOnDateAsync(dateTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDataTable.TableName, result.TableName);
        }

        [TestMethod]
        public void GetExchangeRate_SuccessfulRequest_ReturnsDataTable()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var expectedDataTable = new DataTable();

            var soapRequest = new SoapRequest();

            // Mocking HttpWebRequest and HttpWebResponse
            var httpWebResponseMock = new Mock<HttpWebResponse>();
            httpWebResponseMock.Setup(response => response.GetResponseStream()).Returns(() => new MemoryStream(Encoding.UTF8.GetBytes("<schema></schema><CursValute></CursValute>")));

            var httpWebRequestMock = new Mock<HttpWebRequest>();
            httpWebRequestMock.Setup(request => request.GetResponse()).Returns(httpWebResponseMock.Object);

            var webRequestCreateMock = new Mock<IWebRequestCreate>();
            webRequestCreateMock.Setup(create => create.Create(It.IsAny<Uri>())).Returns(httpWebRequestMock.Object);

            WebRequest.RegisterPrefix("http://", webRequestCreateMock.Object);

            // Act
            var result = soapRequest.GetExchangeRate(dateTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDataTable.TableName, result.TableName);
        }
    }
}
}
