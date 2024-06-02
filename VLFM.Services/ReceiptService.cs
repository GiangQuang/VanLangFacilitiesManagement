using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;
using VLFM.Core.Response;
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
                await _unitOfWork.Receipts.Add(receiptDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteReceipt(List<ReceiptResponse> rec)
        {
            if (rec != null && rec.Any())
            {
                foreach (var item in rec)
                {
                    var receiptDetails = await _unitOfWork.Receipts.GetById(item.Id);
                    if (receiptDetails != null)
                    {
                        _unitOfWork.Receipts.Delete(receiptDetails);
                    }
                }
                var result = _unitOfWork.Save();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<IEnumerable<ReceiptResponse>> GetAllReceipts()
        {
            try
            {
                var receipts = await _unitOfWork.Receipts.GetAll();
                var employees = await _unitOfWork.Employees.GetAll();
                var providers = await _unitOfWork.Providers.GetAll();

                var query = from rec in receipts
                            join emp in employees on rec.EmployeeID equals emp.EmployeeID
                            join prov in providers on rec.ProviderID equals prov.ProviderID
                            select new ReceiptResponse
                            {
                                Id = rec.Id,
                                ReceiptID = rec.ReceiptID,
                                Date = rec.Date,
                                EmployeeID = rec.EmployeeID,
                                ProviderID = rec.ProviderID,
                                Receiptcode = rec.Receiptcode,
                                Note = rec.Note
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ReceiptResponse> GetReceiptById(int Id)
        {
            if (Id > 0)
            {
                var receipts = await _unitOfWork.Receipts.GetById(Id);
                var employees = await _unitOfWork.Employees.GetAll();
                var providers = await _unitOfWork.Providers.GetAll();

                if (receipts != null)
                {
                    try
                    {
                        var employee = employees.FirstOrDefault(emp => emp.EmployeeID == receipts.EmployeeID);
                        var provider = providers.FirstOrDefault(prov => prov.ProviderID == receipts.ProviderID);
                        var response = new ReceiptResponse
                        {
                            Id = receipts.Id,
                            ReceiptID = receipts.ReceiptID,
                            Date = receipts.Date,
                            EmployeeID = receipts.EmployeeID,
                            ProviderID = receipts.ProviderID,
                            Receiptcode = receipts.Receiptcode,
                            Note = receipts.Note
                        };

                        return response;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
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
                    receipt.EmployeeID = receiptDetails.EmployeeID;
                    receipt.ProviderID = receiptDetails.ProviderID;
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
    }
}
