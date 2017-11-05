using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using POS.Domain.Services;

namespace POS.Portal.Controllers.API
{
    public class PurchasesController : ApiController
    {
        private readonly IPurchasesService _purchasesServices;

        private readonly IStockService _stockService;

        public PurchasesController(IPurchasesService purchasesService, IStockService stockService)
        {
            var context = ContextCache.GetPosContext();
            _purchasesServices = purchasesService;
            _stockService = stockService;
            _purchasesServices.Initialize(context);
            _stockService.Initialize(context);
        }

        // GET: api/Purchases
        //public async Task<List<Purchase>> GetPurchases()
        //{
        //    return await _purchasesServices.GetAllPurchases();
        //}


        //// PUT: api/Purchases/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutPurchase(Purchase Purchase)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var result = await _purchasesServices.UpdatePurchase(Purchase);
        //        if (result == null)
        //            return NotFound();
        //        if (result == false)
        //            return BadRequest(Common.Duplicated);
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}

        // POST: api/Purchases
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> PostPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                purchase.ShiftId = CookieHelper.ShiftId;
                foreach (var item in purchase.Details)
                {
                    await _stockService.UpdateStock(new Domain.Entities.Stock { Amount = item.Amount, ProductId = item.ProductId, PointId = purchase.PointId }, Operation.Put);
                }
                var result = await _purchasesServices.AddPurchase(purchase);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Purchases/5
        //[ResponseType(typeof(Purchase))]
        //public async Task<IHttpActionResult> DeletePurchase(int id, bool removeRelatedEntities = false)
        //{
        //    try
        //    {
        //        var result = await _purchasesServices.DeletePurchase(id, removeRelatedEntities);
        //        if (result == false)
        //            return NotFound();
        //        if (result == null)
        //            return BadRequest();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _purchasesServices.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}