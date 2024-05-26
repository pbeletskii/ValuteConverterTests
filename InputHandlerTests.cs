using Moq;
using ValuteConverter;
using System.Collections.ObjectModel;

namespace ValuteConverterTests
{
    [TestClass]
    public class InputHandlerTests
    {
        [DataTestMethod]
        [DataRow("", "", DisplayName = "Empty input returns empty string")]
        [DataRow(null, "", DisplayName = "Null input returns empty string")]
        [DataRow("10.5", "10,5", DisplayName = "Input with decimal point converted to comma")]
        [DataRow("1,0,0", "1,00", DisplayName = "Input with multiple commas truncated")]
        [DataRow("abc123", "123", DisplayName = "Non-numeric characters removed")]
        public void Unify_ValidInput_ReturnsExpectedResult(string input, string expectedResult)
        {
            // Arrange

            // Act
            var result = InputHandler.Unify(input);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Unify_InputStartsWithDecimalPoint_ReturnsEmptyString()
        {
            // Arrange

            // Act
            var result = InputHandler.Unify(",10");

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Unify_InputWithSpecialCharacters_RemovesSpecialCharacters()
        {
            // Arrange

            // Act
            var result = InputHandler.Unify("1,2!3@");

            // Assert
            Assert.AreEqual("123", result);
        }

        [TestMethod]
        public void Unify_InputWithTrailingComma_RemovesTrailingComma()
        {
            // Arrange

            // Act
            var result = InputHandler.Unify("1,2,");

            // Assert
            Assert.AreEqual("1,2", result);
        }

        [TestMethod]
        public void Unify_InputWithOnlyCommas_ReturnsEmptyString()
        {
            // Arrange

            // Act
            var result = InputHandler.Unify(",,,,");

            // Assert
            Assert.AreEqual("", result);
        }
    }
}
