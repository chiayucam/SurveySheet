﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySheet.Controllers.Requests;
using SurveySheet.Controllers.Responses;
using SurveySheet.Extensions;
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
                var userId = HttpContext.GetUserId();

                var itemDtos = await SheetService.GetItemsAsync(limit, nextCursor, userId);
                var response = new GetItemsResponse(itemDtos);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 新增表單物件
        /// </summary>
        /// <param name="request">新增物件內容</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Items")]
        public async Task<ActionResult> AddItems([FromBody] AddItemsRequest request)
        {
            try
            {
                var addItemDtos = request.Items.Select(item => new AddItemDto() { Title = item.Title });
                await SheetService.AddItemsAsync(addItemDtos);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 更新表單物件
        /// </summary>
        /// <param name="id">物件 id</param>
        /// <param name="request">更新物件內容</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Item/{id}")]
        public async Task<ActionResult> UpdateItem(int id, [FromBody] UpdateItemRequest request)
        {
            try
            {
                var updateItemDto = new UpdateItemDto() { Id = id, Title = request.Title };
                await SheetService.UpdateItemAsync(updateItemDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// 刪除表單物件
        /// </summary>
        /// <param name="id">物件 id</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Item/{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                await SheetService.DeleteItemAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// 勾選表單物件
        /// </summary>
        /// <param name="id">物件 id</param>
        /// <returns></returns>
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        [Route("Item/{id}/Check")]
        public async Task<ActionResult> CheckItem(int id)
        {
            try
            {
                var userId = HttpContext.GetUserId();
                await SheetService.CheckItemAsync(userId!.Value, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
