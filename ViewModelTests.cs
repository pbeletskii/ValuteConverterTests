using System.Collections.ObjectModel;
using Moq;
using ValuteConverter;

namespace ValuteConverterTests
{
    [TestClass]
    public class ViewModelTests
    {
        [DataTestMethod]
        [DataRow("10", "20", "2", "5")]
        [DataRow("20", "10", "0.5", "2")]
        public void ConvertLeftToRight_ValidInput_ReturnsExpectedResult(string leftText, string rightText, string expectedRate, string inputValue)
        {
            // Arrange
            var viewModel = new ViewModel(Mock.Of<ICourseConverter>());
            viewModel.SelectedValuteLeft = new ValuteEntry { ValuteCourse = 2 };
            viewModel.SelectedValuteRight = new ValuteEntry { ValuteCourse = 4 };
            viewModel.LeftText = leftText;

            // Act
            viewModel.ConvertLeftToRight();

            // Assert
            Assert.AreEqual(rightText, viewModel.RightText);
            Assert.AreEqual(expectedRate, viewModel.OneUnitRate);
        }

        [TestMethod]
        public void LoadValutes_CallsConverter()
        {
            // Arrange
            var converterMock = new Mock<ICourseConverter>();
            var viewModel = new ViewModel(converterMock.Object);

            // Act
            viewModel.LoadValutes();

            // Assert
            converterMock.Verify(c => c.GetExchangeRateOnDateAsync(It.IsAny<DateTime>()), Times.Once);
        }

        [TestMethod]
        public void SetFirstValutes_EmptyValuteEntries_NoExceptionThrown()
        {
            // Arrange
            var viewModel = new ViewModel(Mock.Of<ICourseConverter>());

            // Act & Assert
            Assert.AreEqual(null, viewModel.SelectedValuteLeft);
            Assert.AreEqual(null, viewModel.SelectedValuteRight);
        }

        [TestMethod]
        public void ChangeSides_ExecutesSuccessfully()
        {
            // Arrange
            var viewModel = new ViewModel(Mock.Of<ICourseConverter>());
            var initialLeft = viewModel.SelectedValuteLeft;
            var initialRight = viewModel.SelectedValuteRight;

            // Act
            viewModel.ChangeSides.Execute(null);

            // Assert
            Assert.AreEqual(initialLeft, viewModel.SelectedValuteRight);
            Assert.AreEqual(initialRight, viewModel.SelectedValuteLeft);
        }

        [TestMethod]
        public void ViewModel_PropertyChanged_IsRaised()
        {
            // Arrange
            var viewModel = new ViewModel(Mock.Of<ICourseConverter>());
            var propertyChangedRaised = false;
            viewModel.PropertyChanged += (sender, args) => { propertyChangedRaised = true; };

            // Act
            viewModel.LeftText = "10";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }
    }
}