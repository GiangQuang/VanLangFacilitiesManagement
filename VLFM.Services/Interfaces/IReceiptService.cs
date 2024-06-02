using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;
using VLFM.Core.Response;

namespace VLFM.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<bool> CreateReceipt(ReceiptDetails receiptDetails);
        Task<IEnumerable<ReceiptResponse>> GetAllReceipts();
        Task<ReceiptResponse> GetReceiptById(int Id);
        Task<bool> UpdateReceipt(ReceiptDetails receiptDetails);
        Task<bool> DeleteReceipt(List<ReceiptResponse> rec);
    }
}
