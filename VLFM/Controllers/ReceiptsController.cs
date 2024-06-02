using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Core.Response;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/receipt")]
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
            string ReceiptID = HttpContext.Request.Query.ContainsKey("ReceiptID") ? HttpContext.Request.Query["ReceiptID"].ToString() : null;
            string Date = HttpContext.Request.Query.ContainsKey("Date") ? HttpContext.Request.Query["Date"].ToString() : null;
            string EmployeeID = HttpContext.Request.Query.ContainsKey("EmployeeID") ? HttpContext.Request.Query["EmployeeID"].ToString() : null;
            string ProviderID = HttpContext.Request.Query.ContainsKey("ProviderID") ? HttpContext.Request.Query["ProviderID"].ToString() : null;
            string Receiptcode = HttpContext.Request.Query.ContainsKey("Receiptcode") ? HttpContext.Request.Query["Receiptcode"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;
            
            if (!string.IsNullOrEmpty(ReceiptID))
            {
                DetailsList = DetailsList.Where(r => r.ReceiptID.Contains(ReceiptID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Date))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(Date, out parsedDate))
                {
                    DetailsList = DetailsList.Where(r => r.Date == parsedDate).ToList();
                }
            }
            if (!string.IsNullOrEmpty(EmployeeID))
            {
                DetailsList = DetailsList.Where(r => r.EmployeeID.ToString().Contains(EmployeeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (!string.IsNullOrEmpty(ProviderID))
            {
                DetailsList = DetailsList.Where(r => r.ProviderID.Contains(ProviderID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Receiptcode))
            {
                DetailsList = DetailsList.Where(r => r.Receiptcode.Contains(Receiptcode, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Note))
            {
                DetailsList = DetailsList.Where(r => r.Note.Contains(Note, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var responseData = new
            {
                data = DetailsList,
            };

            return Ok(responseData);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetReceiptById(int Id)
        {
            var receiptDetails = await _receiptService.GetReceiptById(Id);

            if (receiptDetails != null)
            {
                var responseData = new { data = receiptDetails };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteReceipt(List<ReceiptResponse> rec)
        {
            var isReceiptDeleted = await _receiptService.DeleteReceipt(rec);

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
