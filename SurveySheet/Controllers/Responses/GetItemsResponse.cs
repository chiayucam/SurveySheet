using SurveySheet.Services.Models;

namespace SurveySheet.Controllers.Responses
{
    public class GetItemsResponse
    {
        public IEnumerable<ItemDto> Items { get; set; }

        public GetItemsResponse(IEnumerable<ItemDto> itemDtos)
        {
            Items = itemDtos;
        }
    }
}
