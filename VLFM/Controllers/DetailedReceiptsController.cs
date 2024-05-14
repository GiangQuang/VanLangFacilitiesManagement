using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/DetailedReceipt")]
    [ApiController]
    public class DetailedReceiptsController : ControllerBase
    {
        private readonly IDetailedReceiptService _detailedReceiptService;
        public DetailedReceiptsController(IDetailedReceiptService detailedReceiptService)
        {
            _detailedReceiptService = detailedReceiptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailedReceiptList()
        {
            var DetailsList = await _detailedReceiptService.GetAllDetailedReceipts();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDetailedReceiptById(int Id)
        {
            var detailedReceiptDetails = await _detailedReceiptService.GetDetailedReceiptById(Id);

            if (detailedReceiptDetails != null)
            {
                return Ok(detailedReceiptDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDetailedReceipt(DetailedReceiptDetails detailedReceipt)
        {
            var isDetailedReceiptCreated = await _detailedReceiptService.CreateDetailedReceipt(detailedReceipt);

            if (isDetailedReceiptCreated)
            {
                return Ok(isDetailedReceiptCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDetailedReceipt(DetailedReceiptDetails detailedReceipt)
        {
            if (detailedReceipt != null)
            {
                var isDetailedReceiptUpdated = await _detailedReceiptService.UpdateDetailedReceipt(detailedReceipt);
                if (isDetailedReceiptUpdated)
                {
                    return Ok(isDetailedReceiptUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDetailedReceipt(int Id)
        {
            var isDetailedReceiptDeleted = await _detailedReceiptService.DeleteDetailedReceipt(Id);

            if (isDetailedReceiptDeleted)
            {
                return Ok(isDetailedReceiptDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
