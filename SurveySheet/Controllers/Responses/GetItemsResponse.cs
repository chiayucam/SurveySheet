using SurveySheet.Services.Models;

namespace SurveySheet.Controllers.Responses
{
    public class GetItemsResponse
    {
        public IEnumerable<ItemDto> Items { get; set; }

        public int NextCursor { get; set; }

        public GetItemsResponse(IEnumerable<ItemDto> itemDtos)
        {
            Items = itemDtos;
            NextCursor = itemDtos.Last().Id;
        }
    }
}
