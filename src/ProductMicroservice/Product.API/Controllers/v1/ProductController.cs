using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Commands;
using Product.Application.Features.Query;
using Product.Domain.Entities;
using Shared.Utilities.Response;

namespace Product.API.Controllers.v1
{
    public class ProductController : BaseController
    {
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var query = new GetProductQuery() { productId = id};
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:int}/price")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductPriceByIdAsync(int id)
        {
            var query = new GetProductPriceQuery { ProductId = id };

            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("by-subcategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsBySubcategoryIdAsync([FromQuery] string scid)
        {
            var query = new GetProductsBySubcategoryIdQuery() { SubCategoryId = int.Parse(scid) };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFilteredProductsAsync([FromBody] GetFilteredProductsQuery query)
        {
            var result = await Mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            // Return bad request or other appropriate status if failure
            return BadRequest(result);
        }        

        [HttpGet("trending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubcategoryTrendingAsync([FromQuery] GetTrendingProductQuery query)
        {
            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return NotFound(new { Message = "Products not found." });

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByNameAsync([FromQuery(Name = "q")] string name)
        {
            var result = await Mediator.Send(new GetProductByProductName { ProductName = name });
            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Product created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Product updated successfully." });
        }

        [HttpDelete("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await Mediator.Send(new DeleteProductCommand(productId));

            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Product deleted successfully." });
        }

        [HttpPost("byIdAndCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts(GetProductsByIdAndCategoryIdQuery productIds)
        {
            var result = await Mediator.Send(productIds);

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("{productId:int}/with-variants")]
        [ProducesResponseType(typeof(ProductMaster), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductWithVariants(int productId)
        {
            var result = await Mediator.Send(new GetProductWithVariantsQuery(productId));
            return Ok(result);
        }

        [HttpGet("{productId:int}/variants")]
        [ProducesResponseType(typeof(IEnumerable<ProductVariantMaster>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVariants(int productId)
        {
            var result = await Mediator.Send(new GetVariantsQuery(productId));

            return Ok(result);
        }

        [HttpGet("{productId:int}/region-prices")]
        [ProducesResponseType(typeof(IEnumerable<ProductRegionPriceMaster>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRegionPrices(int productId)
        {
            var result = await Mediator.Send(new GetRegionPricesQuery(productId));
            return Ok(result);
        }

        [HttpGet("{productId:int}/localized-info")]
        [ProducesResponseType(typeof(IEnumerable<LocalizedProductInfoMaster>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLocalizedInfo(int productId)
        {
            var result = await Mediator.Send(new GetLocalizedInfoQuery(productId));
            return Ok(result);
        }

        [HttpGet("{productId:int}/tags")]
        [ProducesResponseType(typeof(IEnumerable<ProductTagMaster>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTags(int productId)
        {
            var result = await Mediator.Send(new GetProductTagsQuery(productId));
            return Ok(result);
        }

        [HttpGet("{productId:int}/seo-info")]
        [ProducesResponseType(typeof(ProductSEOInfoMaster), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSEOInfo(int productId)
        {
            var result = await Mediator.Send(new GetProductSEOInfoQuery(productId));
            return Ok(result);
        }        

        /// <summary>
        /// Search products by query with pagination
        /// </summary>
        /// <param name="query">Search keyword</param>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <returns>Paginated list of product search results</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] GetSearchQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Query parameter is required.");

            var result = await Mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }

}
