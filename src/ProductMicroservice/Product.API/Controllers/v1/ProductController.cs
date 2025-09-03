using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Commands;
using Product.Application.Features.Query;
using Product.Domain.Entities;

namespace Product.API.Controllers.v1
{
    public class ProductController : BaseController
    {

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetGetProductByProductNameAsync(string name)
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

        [HttpGet("product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetProductsWithAttributesAsync([FromQuery] GetProductQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetProductsWithAttributesAsync([FromQuery] GetProductsWithAttributesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }        

        [HttpPost("filtered-products")]
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

    }

}
