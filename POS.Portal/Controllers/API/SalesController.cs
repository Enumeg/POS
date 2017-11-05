using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Entities;
using POS.Domain.Enums;
using POS.Domain.Services;
using POS.Portal.Helpers;
using POS.Resources;

namespace POS.Portal.Controllers.API
{


    public class SalesController : ApiController
    {
        private readonly ISalesService _salesServices;
        private readonly IStockService _stockService;

        public SalesController(ISalesService salesService, IStockService stockService)
        {
            var context = ContextCache.GetPosContext();
            _salesServices = salesService;
            _stockService = stockService;
            _salesServices.Initialize(context);
            _stockService.Initialize(context);
        }
        [ResponseType(typeof(Sale))]
        public async Task<IHttpActionResult> PostSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                sale.ShiftId = CookieHelper.ShiftId;
                foreach (var item in sale.Details)
                {
                    await _stockService.UpdateStock(new Domain.Entities.Stock { Amount = item.Amount, ProductId = item.ProductId, PointId = sale.PointId }, Operation.Take);
                }
                var result = await _salesServices.AddSale(sale);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
