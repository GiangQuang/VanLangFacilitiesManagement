using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;
using VLFM.Core.Response;

namespace VLFM.Services.Interfaces
{
    public interface IDetailedReceiptService
    {
        Task<bool> CreateDetailedReceipt(DetailedReceiptDetails detailedReceipt);
        Task<IEnumerable<DetailedReceiptResponse>> GetAllDetailedReceipts();
        Task<DetailedReceiptResponse> GetDetailedReceiptById(int Id);
        Task<bool> UpdateDetailedReceipt(DetailedReceiptDetails detailedReceipt);
        Task<bool> DeleteDetailedReceipt(List<DetailedReceiptResponse> derec);
    }
}
