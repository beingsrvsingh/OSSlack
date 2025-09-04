using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Application.Contracts;
using CatalogUI.Application.Services;
using CatalogUI.Domain.Core.Repository;
using CatalogUI.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace CatalogUI.Infrastructure.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;
        private readonly ILoggerService<LayoutService> _logger;

        public LayoutService(ILayoutRepository layoutRepository, ILoggerService<LayoutService> logger)
        {
            _layoutRepository = layoutRepository;
            _logger = logger;
        }

        public async Task<LayoutDto?> GetLayoutByIdAsync(int id)
        {
            try
            {
                var entity = await _layoutRepository.GetByIdAsync(id);
                return entity == null ? null : LayoutDto.ToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching layout by ID {id}");
                return null;
            }
        }

        public async Task<IEnumerable<LayoutDto>> GetAllLayoutsAsync()
        {
            try
            {
                var entities = await _layoutRepository.GetAllAsync();
                return entities.Select(LayoutDto.ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all layouts");
                return Enumerable.Empty<LayoutDto>();
            }
        }

        public async Task<IEnumerable<LayoutDto>> GetLayoutsByPageAsync(string pageName)
        {
            try
            {
                var entities = await _layoutRepository.GetByPageAsync(pageName);
                return entities.Select(LayoutDto.ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching layouts for page '{pageName}'");
                return Enumerable.Empty<LayoutDto>();
            }
        }

        public async Task<IEnumerable<LayoutDto>> GetLayoutsByTenantAndRoleAsync(string tenantId, string? userRole)
        {
            try
            {
                var entities = await _layoutRepository.GetByTenantAndRoleAsync(tenantId, userRole);
                return entities.Select(LayoutDto.ToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching layouts for tenant '{tenantId}' and role '{userRole}'");
                return Enumerable.Empty<LayoutDto>();
            }
        }

        public async Task<bool> AddLayoutAsync(CreateLayoutDto layoutDto)
        {
            try
            {
                var entity = CreateLayoutDto.ToEntity(layoutDto);
                await _layoutRepository.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding layout");
                return false;
            }
        }

        public async Task<bool> UpdateLayoutAsync(UpdateLayoutDto layoutDto)
        {
            try
            {
                var existing = await _layoutRepository.GetByIdAsync(layoutDto.Id);
                if (existing == null)
                {
                    _logger.LogWarning($"Attempt to update non-existent layout ID {layoutDto.Id}");
                    return false;
                }

                UpdateLayoutDto.UpdateEntity(existing, layoutDto);
                await _layoutRepository.UpdateAsync(existing);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating layout ID {layoutDto.Id}");
                return false;
            }
        }

        public async Task<bool> DeleteLayoutAsync(int id)
        {
            try
            {
                var exists = await _layoutRepository.GetByIdAsync(id);
                if (exists == null)
                {
                    _logger.LogWarning($"Attempt to delete non-existent layout ID {id}");
                    return false;
                }

                await _layoutRepository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting layout ID {id}");
                return false;
            }
        }
    }
}