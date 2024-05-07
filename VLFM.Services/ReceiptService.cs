using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;
using VLFM.Services.Interfaces;

namespace VLFM.Services
{
    public class ReceiptService : IReceiptService
    {
        public IUnitOfWork _unitOfWork;
        public ReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateReceipt(ReceiptDetails receiptDetails)
        {
            if (receiptDetails != null)
            {
                receiptDetails.ReceiptID = GenerateReceiptID();
                await _unitOfWork.Receipts.Add(receiptDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteReceipt(int Id)
        {
            if (Id > 0)
            {
                var receiptDetails = await _unitOfWork.Receipts.GetById(Id);
                if (receiptDetails != null)
                {
                    _unitOfWork.Receipts.Delete(receiptDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<ReceiptDetails>> GetAllReceipts()
        {
            var DetailsList = await _unitOfWork.Receipts.GetAll();
            return DetailsList;
        }

        public async Task<ReceiptDetails> GetReceiptById(int Id)
        {
            if (Id > 0)
            {
                var receiptDetails = await _unitOfWork.Receipts.GetById(Id);
                if (receiptDetails != null)
                {
                    return receiptDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateReceipt(ReceiptDetails receiptDetails)
        {
            if (receiptDetails != null)
            {
                var receipt = await _unitOfWork.Receipts.GetById(receiptDetails.Id);
                if (receipt != null)
                {
                    receipt.Date = receiptDetails.Date;
                    receipt.Receiptcode = receiptDetails.Receiptcode;
                    receipt.Note = receiptDetails.Note;
                    _unitOfWork.Receipts.Update(receipt);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        private string GenerateReceiptID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
