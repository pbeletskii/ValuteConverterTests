using Moq;
using ValuteConverter;
using System.Data;

namespace ValuteConverterTests
{
    [TestClass]
    public class CoinYepParserTests
    {
        [TestMethod]
        public void GetValuteEntries_ReturnsExpectedCount()
        {
            // Arrange
            DataTable table = new DataTable();
            table.Columns.Add("Vname", typeof(string));
            table.Columns.Add("Vchcode", typeof(string));
            table.Columns.Add("Vcurs", typeof(double));

            table.Rows.Add("US Dollar", "USD", 1.0);
            table.Rows.Add("Euro", "EUR", 0.85);

            // Act
            IEnumerable<ValuteEntry> entries = CoinYepParser.GetValuteEntries(table);

            // Assert
            Assert.AreEqual(3, entries.Count()); // Including the default Russian ruble entry
        }

        [TestMethod]
        public void GetValuteEntries_ReturnsExpectedEntries()
        {
            // Arrange
            DataTable table = new DataTable();
            table.Columns.Add("Vname", typeof(string));
            table.Columns.Add("Vchcode", typeof(string));
            table.Columns.Add("Vcurs", typeof(double));

            table.Rows.Add("US Dollar", "USD", 1.0);
            table.Rows.Add("Euro", "EUR", 0.85);

            // Act
            IEnumerable<ValuteEntry> entries = CoinYepParser.GetValuteEntries(table);

            // Assert
            // Assuming the Russian ruble entry is always present at the beginning
            Assert.AreEqual("Российский рубль", entries.ElementAt(0).Name);
            Assert.AreEqual("RUB", entries.ElementAt(0).Code);
            Assert.AreEqual(1.0, entries.ElementAt(0).Rate);

            Assert.AreEqual("US Dollar", entries.ElementAt(1).Name);
            Assert.AreEqual("USD", entries.ElementAt(1).Code);
            Assert.AreEqual(1.0, entries.ElementAt(1).Rate);

            Assert.AreEqual("Euro", entries.ElementAt(2).Name);
            Assert.AreEqual("EUR", entries.ElementAt(2).Code);
            Assert.AreEqual(0.85, entries.ElementAt(2).Rate);
        }
    }
}
