﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using POS.Domain.Interfaces;
using POS.Portal.Helpers;
using POS.Resources;
using Product = POS.Domain.Entities.Product;
using POS.Domain.Services;

namespace POS.Portal.Controllers.API
{
    public class ProductsController : ApiController
    {
        private readonly IProductsService _productsService;

        private readonly IStockService _stockService;
       
      
        public ProductsController(IProductsService productsService ,IStockService stockService)
        {
            _productsService = productsService;
            _stockService = stockService;        
        }

        // GET: api/Products
        public async Task<List<Product>> GetProducts()
        {
            return await _productsService.GetAllProducts();
        }
        public async Task<Product> GetProduct(string barcode)
        {
            return await _productsService.GetProduct(barcode);
        }
        [Route("api/Products/CheckStorage")]
        public async Task<decimal> GetProduct(int productId, int pointId)
        {
            return await _stockService.GetStock(productId, pointId);
        }
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _productsService.UpdateProduct(product);
                if (result == null)
                    return NotFound();
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _productsService.AddProduct(product);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok(product);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Route("api/products/Add")]
        public async Task<IHttpActionResult> PostProducts(List<Product> products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _productsService.AddProducts(products);
                if (result == false)
                    return BadRequest(Common.Duplicated);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id, bool removeRelatedEntities = false)
        {
            try
            {
                var result = await _productsService.DeleteProduct(id, removeRelatedEntities);
                if (result == false)
                    return NotFound();
                if (result == null)
                    return BadRequest();
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productsService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
