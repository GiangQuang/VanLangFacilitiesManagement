﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IReceiptService
    {
        Task<bool> CreateReceipt(ReceiptDetails receiptDetails);
        Task<IEnumerable<ReceiptDetails>> GetAllReceipts();
        Task<ReceiptDetails> GetReceiptById(int Id);
        Task<bool> UpdateReceipt(ReceiptDetails receiptDetails);
        Task<bool> DeleteReceipt(int Id);
    }
}