using Moq;
using ValuteConverter;

namespace ValuteConverterTests
{
    [TestClass]
    public class ValuteEntryTests
    {
        [DataTestMethod]
        [DataRow("USD", "840", 1.0)]
        [DataRow("EUR", "978", 0.85)]
        [DataRow("GBP", "826", 0.72)]
        public void ValuteEntry_Constructor_InitializesPropertiesCorrectly(string name, string code, double course)
        {
            // Arrange

            // Act
            var valuteEntry = new ValuteEntry(name, code, course);

            // Assert
            Assert.AreEqual(name, valuteEntry.ValuteName);
            Assert.AreEqual(code, valuteEntry.ValuteCode);
            Assert.AreEqual(course, valuteEntry.ValuteCourse);
        }

        [TestMethod]
        public void ValuteEntry_CanBeMocked()
        {
            // Arrange
            var mockValuteEntry = new Mock<ValuteEntry>("USD", "840", 1.0);

            // Act
            var name = mockValuteEntry.Object.ValuteName;
            var code = mockValuteEntry.Object.ValuteCode;
            var course = mockValuteEntry.Object.ValuteCourse;

            // Assert
            Assert.AreEqual("USD", name);
            Assert.AreEqual("840", code);
            Assert.AreEqual(1.0, course);
        }

        [TestMethod]
        [DynamicData(nameof(GetValuteEntries), DynamicDataSourceType.Method)]
        public void ValuteEntry_CanBeCreatedWithDataRows(string name, string code, double course)
        {
            // Arrange

            // Act
            var valuteEntry = new ValuteEntry(name, code, course);

            // Assert
            Assert.AreEqual(name, valuteEntry.ValuteName);
            Assert.AreEqual(code, valuteEntry.ValuteCode);
            Assert.AreEqual(course, valuteEntry.ValuteCourse);
        }

        public static IEnumerable<object[]> GetValuteEntries()
        {
            yield return new object[] { "USD", "840", 1.0 };
            yield return new object[] { "EUR", "978", 0.85 };
            yield return new object[] { "GBP", "826", 0.72 };
        }
    }
}
