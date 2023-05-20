using Moq;
using SurveySheet.Repositories.Interfaces;
using SurveySheet.Services;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SurveySheet.UnitTest.Services
{
    public class SheetServiceTest
    {
        private readonly Mock<ISheetRepository> MockSheetRepository;

        public SheetServiceTest()
        {
            MockSheetRepository = new Mock<ISheetRepository>();
        }

        [Fact]
        public async Task AddItemsAsync_TitleLengthExceeds50_ThrowInvalidOperationException()
        {
            // Assign
            var title = new string('A', 55);
            var addItemDtos = new List<AddItemDto>()
            {
                new AddItemDto(){Title = title}
            };

            var sheetService = new SheetService(MockSheetRepository.Object);

            // Act
            var act = () => sheetService.AddItemsAsync(addItemDtos);

            // Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
