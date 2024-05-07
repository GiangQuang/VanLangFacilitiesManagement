using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Receipt")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        public ReceiptsController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReceiptList()
        {
            var DetailsList = await _receiptService.GetAllReceipts();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReceiptById(int Id)
        {
            var receiptDetails = await _receiptService.GetReceiptById(Id);

            if (receiptDetails != null)
            {
                return Ok(receiptDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceipt(ReceiptDetails receiptDetails)
        {
            var isReceiptCreated = await _receiptService.CreateReceipt(receiptDetails);

            if (isReceiptCreated)
            {
                return Ok(isReceiptCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReceipt(ReceiptDetails receiptDetails)
        {
            if (receiptDetails != null)
            {
                var isReceiptUpdated = await _receiptService.UpdateReceipt(receiptDetails);
                if (isReceiptUpdated)
                {
                    return Ok(isReceiptUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteReceipt(int Id)
        {
            var isReceiptDeleted = await _receiptService.DeleteReceipt(Id);

            if (isReceiptDeleted)
            {
                return Ok(isReceiptDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
