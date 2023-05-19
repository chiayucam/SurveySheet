using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveySheet.Controllers.Responses;
using SurveySheet.Services;

namespace SurveySheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetController : ControllerBase
    {
        private ISheetService SheetService;

        public SheetController(ISheetService sheetService)
        {
            SheetService = sheetService;
        }

        [HttpGet]
        [Route(("Items/{limit}"))]
        public async Task<ActionResult<GetItemsResponse>> GetItems(int limit = 10, [FromQuery] int? nextCursor = null)
        {
            var itemDtos = await SheetService.GetItemsAsync(limit, nextCursor);
            var response = new GetItemsResponse(itemDtos);
            return Ok(response);
        }




        
    }
}
