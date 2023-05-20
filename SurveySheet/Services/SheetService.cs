using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;

namespace SurveySheet.Services
{
    public class SheetService : ISheetService
    {
        private ISheetRepository SheetRepository;

        public SheetService(ISheetRepository sheetRepository)
        {
            SheetRepository = sheetRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync(int limit, int? nextCursor)
        {
            IEnumerable<Item> items;

            if (nextCursor.HasValue)
            {
                items = await SheetRepository.GetNextItemsAsync(limit, nextCursor.Value);
            }
            else
            {
                items = await SheetRepository.GetInitialItemsAsync(limit);
            };

            var itemDtos = items.Select(item => new ItemDto(item));
            return itemDtos;
        }
    }
}
