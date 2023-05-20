using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;

namespace SurveySheet.Services
{
    public class SheetService : ISheetService
    {
        private IItemRepository ItemRepository;

        private ICheckedItemRepository CheckedItemRepository;

        public SheetService(IItemRepository itemRepository, ICheckedItemRepository checkedItemRepository)
        {
            ItemRepository = itemRepository;
            CheckedItemRepository = checkedItemRepository;
        }

        public async Task AddItemsAsync(IEnumerable<AddItemDto> addItemDtos)
        {
            var titles = addItemDtos.Select(dto => dto.Title);

            if (titles.Any(title => title.Length > 50))
            {
                throw new InvalidOperationException("Title length exceeded limit");
            }

            var addItems = addItemDtos.Select(dto => new AddItem() { Title = dto.Title });
            await ItemRepository.AddItemsAsync(addItems);
        }

        public async Task CheckItemAsync(int userId, int itemId)
        {
            await CheckedItemRepository.CheckItemAsync(userId, itemId);
        }

        public async Task DeleteItemAsync(int id)
        {
            await ItemRepository.DeleteItemAsync(id);
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync(int limit, int? nextCursor)
        {
            IEnumerable<Item> items;

            if (nextCursor.HasValue)
            {
                items = await ItemRepository.GetNextItemsAsync(limit, nextCursor.Value);
            }
            else
            {
                items = await ItemRepository.GetInitialItemsAsync(limit);
            };

            var itemDtos = items.Select(item => new ItemDto(item));
            return itemDtos;
        }

        public async Task UpdateItemAsync(UpdateItemDto updateItemDto)
        {
            var item = new Item() { Id = updateItemDto.Id, Title = updateItemDto.Title };
            await ItemRepository.UpdateItemAysnc(item);
        }
    }
}
