using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        
    }
}
