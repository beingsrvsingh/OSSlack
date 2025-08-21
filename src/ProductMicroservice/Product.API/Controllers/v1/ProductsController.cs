using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Commands;
using Product.Application.Features.Query;
using Product.Domain.Entities;

namespace Product.API.Controllers.v1
{
    public class ProductController : BaseController
    {
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

        [HttpGet("{productId:int}/attributes")]
        [ProducesResponseType(typeof(IEnumerable<ProductAttributeMaster>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttributes(int productId)
        {
            var result = await Mediator.Send(new GetProductAttributesQuery(productId));
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

        [HttpGet("{scid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetProductBySubCategoryIdAsync(int scid)
        {
            var result = await Mediator.Send(new GetProductBySubCategoryId { SubCategoryId = scid });

            return Ok(result);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetGetProductByProductNameAsync(string name)
        {
            var result = await Mediator.Send(new GetProductByProductName{ProductName = name});

            return Ok(result);
        }
    }

}
