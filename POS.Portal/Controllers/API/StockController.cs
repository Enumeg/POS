using POS.Domain.Models;
using POS.Domain.Services;
using POS.Portal.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace POS.Portal.Controllers.API
{
    public class StockController : ApiController
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            var context = ContextCache.GetPosContext();
            _stockService = stockService;
            _stockService.Initialize(context);
        }
        // api/get
        [HttpGet]
        public async Task<List<StockModel>> GetStocks(int? productId = null, int? pointId = null)
        {
            return await _stockService.GetStocks(productId, pointId);
        }
    }
}
