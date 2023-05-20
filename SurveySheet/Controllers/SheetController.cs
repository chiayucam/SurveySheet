using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveySheet.Controllers.Requests;
using SurveySheet.Controllers.Responses;
using SurveySheet.Services;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;

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

        /// <summary>
        /// 取得表單物件 (cursor based)
        /// </summary>
        /// <param name="limit">物件數量</param>
        /// <param name="nextCursor">next cursor</param>
        /// <returns></returns>
        [HttpGet]
        [Route(("Items/{limit}"))]
        public async Task<ActionResult<GetItemsResponse>> GetItems(int limit = 10, [FromQuery] int? nextCursor = null)
        {

            try
            {
                var itemDtos = await SheetService.GetItemsAsync(limit, nextCursor);
                var response = new GetItemsResponse(itemDtos);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 新增表單物件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Items")]
        public async Task<ActionResult> AddItems([FromBody] AddItemsRequest request)
        {
            var addItemDtos = request.Items.Select(item => new AddItemDto() { Title = item.Title});
            await SheetService.AddItemsAsync(addItemDtos);
            return NoContent();
        }
    }
}
