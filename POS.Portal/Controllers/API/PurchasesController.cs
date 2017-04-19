using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;

namespace POS.Portal.Controllers.API
{
    public class PurchasesController : ApiController
    {
        private readonly IPurchasesService _purchasesServices;

        public PurchasesController(IPurchasesService purchasesService)
        {
            var context = ContextCache.GetPosContext();
            _purchasesServices = purchasesService;
            _purchasesServices.Initialize(context);
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
                var result = await _purchasesServices.AddPurchase(purchase);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return InternalServerError();
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