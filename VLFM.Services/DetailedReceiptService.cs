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
    public class DetailedReceiptService : IDetailedReceiptService
    {
        public IUnitOfWork _unitOfWork;
        public DetailedReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateDetailedReceipt(DetailedReceiptDetails detailedReceipt)
        {
            if (detailedReceipt != null)
            {
                await _unitOfWork.ReceiptsDetailed.Add(detailedReceipt);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteDetailedReceipt(List<DetailedReceiptResponse> derec)
        {
            if (derec != null && derec.Any())
            {
                foreach (var item in derec)
                {
                    var detailedReceipt = await _unitOfWork.ReceiptsDetailed.GetById(item.Id);
                    if (detailedReceipt != null)
                    {
                        _unitOfWork.ReceiptsDetailed.Delete(detailedReceipt);
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

        public async Task<IEnumerable<DetailedReceiptResponse>> GetAllDetailedReceipts()
        {
            try
            {
                var detailedreceipts = await _unitOfWork.ReceiptsDetailed.GetAll();
                var rececipts = await _unitOfWork.Receipts.GetAll();
                var properties = await _unitOfWork.Properties.GetAll();
                var statuses = await _unitOfWork.Statuses.GetAll();
                var proposes = await _unitOfWork.Proposes.GetAll();

                var query = from drec in detailedreceipts
                            join rec in rececipts on drec.ReceiptID equals rec.ReceiptID
                            join prop in properties on drec.PropertyID equals prop.PropertyID
                            join sta in statuses on drec.StatusID equals sta.StatusID
                            join props in proposes on drec.ProposeID equals props.ProposeID
                            select new DetailedReceiptResponse
                            {
                                Id = drec.Id,
                                DtReceiptID = drec.DtReceiptID,
                                ReceiptID = drec.ReceiptID,
                                PropertyID = drec.PropertyID,
                                quantity = drec.quantity,
                                Price = drec.Price,
                                Brand = drec.Brand,
                                WarrantydayAt = drec.WarrantydayAt,
                                WarrantydayEnd = drec.WarrantydayEnd,
                                StatusID = drec.StatusID,
                                ProposeID = drec.ProposeID,
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DetailedReceiptResponse> GetDetailedReceiptById(int Id)
        {
            if (Id > 0)
            {
                var detailedreceipts = await _unitOfWork.ReceiptsDetailed.GetById(Id);
                var rececipts = await _unitOfWork.Receipts.GetAll();
                var properties = await _unitOfWork.Properties.GetAll();
                var statuses = await _unitOfWork.Statuses.GetAll();
                var proposes = await _unitOfWork.Proposes.GetAll();

                if (rececipts != null)
                {
                    try
                    {
                        var rececipt = rececipts.FirstOrDefault(rec => rec.ReceiptID == detailedreceipts.ReceiptID);
                        var property = properties.FirstOrDefault(prop => prop.PropertyID == detailedreceipts.PropertyID);
                        var status = statuses.FirstOrDefault(sta => sta.StatusID == detailedreceipts.StatusID);
                        var propose= proposes.FirstOrDefault(props => props.ProposeID == detailedreceipts.ProposeID);
                        var response = new DetailedReceiptResponse
                        {
                            Id = detailedreceipts.Id,
                            DtReceiptID = detailedreceipts.DtReceiptID,
                            ReceiptID = detailedreceipts.ReceiptID,
                            PropertyID = detailedreceipts.PropertyID,
                            quantity = detailedreceipts.quantity,
                            Price = detailedreceipts.Price,
                            Brand = detailedreceipts.Brand,
                            WarrantydayAt = detailedreceipts.WarrantydayAt,
                            WarrantydayEnd = detailedreceipts.WarrantydayEnd,
                            StatusID = detailedreceipts.StatusID,
                            ProposeID = detailedreceipts.ProposeID,
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

        public async Task<bool> UpdateDetailedReceipt(DetailedReceiptDetails detailedReceipt)
        {
            if (detailedReceipt != null)
            {
                var detailedreceipt = await _unitOfWork.ReceiptsDetailed.GetById(detailedReceipt.Id);
                if (detailedreceipt != null)
                {
                    detailedreceipt.ReceiptID = detailedReceipt.ReceiptID;
                    detailedreceipt.PropertyID = detailedReceipt.PropertyID;
                    detailedreceipt.quantity = detailedReceipt.quantity;
                    detailedreceipt.Price = detailedReceipt.Price;
                    detailedreceipt.Brand = detailedReceipt.Brand;
                    detailedreceipt.WarrantydayAt = detailedReceipt.WarrantydayAt;
                    detailedreceipt.WarrantydayEnd = detailedReceipt.WarrantydayEnd;
                    detailedreceipt.StatusID = detailedReceipt.StatusID;
                    detailedreceipt.ProposeID = detailedReceipt.ProposeID;
                    _unitOfWork.ReceiptsDetailed.Update(detailedreceipt);
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
