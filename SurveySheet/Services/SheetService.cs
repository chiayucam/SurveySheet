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

        public async Task AddItemsAsync(IEnumerable<AddItemDto> addItemDtos)
        {
            var titles = addItemDtos.Select(dto => dto.Title);

            if (titles.Any(title => title.Length > 50))
            {
                throw new InvalidOperationException();
            }

            var addItems = addItemDtos.Select(dto => new AddItem() { Title = dto.Title });
            await SheetRepository.AddItemsAsync(addItems);
        }

        public async Task DeleteItemAsync(int id)
        {
            await SheetRepository.DeleteItemAsync(id);
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

        public async Task UpdateItemAsync(UpdateItemDto updateItemDto)
        {
            var item = new Item() { Id = updateItemDto.Id, Title = updateItemDto.Title };
            await SheetRepository.UpdateItemAysnc(item);
        }
    }
}
