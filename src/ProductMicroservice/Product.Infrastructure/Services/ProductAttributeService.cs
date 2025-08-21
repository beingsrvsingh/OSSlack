using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Application.Services;
using Product.Domain.Core.Repository;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Infrastructure.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeRepository _attributeRepository;
        private readonly ILoggerService<ProductAttributeService> _logger;

        public ProductAttributeService(
            IProductAttributeRepository attributeRepository,
            ILoggerService<ProductAttributeService> logger)
        {
            _attributeRepository = attributeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductAttributeValue>> GetAttributesByProductIdAsync(int productId)
        {
            try
            {
                var attributes = await _attributeRepository.GetByProductIdAsync(productId);
                return attributes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get attributes for productId: {productId}");
                return Enumerable.Empty<ProductAttributeValue>();
            }
        }
    }

}