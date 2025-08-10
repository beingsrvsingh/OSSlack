// using Temple.Application.Service;
// using Temple.Domain.Entities;
// using Temple.Domain.UOW;
// using Shared.Application.Interfaces.Logging;

// namespace Temple.Infrastructure.Service
// {
//     public class PoojaProductService : IPoojaProductService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILoggerService<PoojaProductService> _logger;

//         public PoojaProductService(ILoggerService<PoojaProductService> logger, IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }

//         public async Task<PoojaProduct?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _unitOfWork.PoojaProducts.GetByIdAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error fetching pooja product by id {Id}", id);
//                 return null;
//             }
//         }

//         public async Task<bool> CreateAsync(PoojaProduct product)
//         {
//             try
//             {
//                 await _unitOfWork.PoojaProducts.AddAsync(product);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating pooja product");
//                 return false;
//             }
//         }

//         public async Task<bool> UpdateAsync(PoojaProduct product)
//         {
//             try
//             {
//                 await _unitOfWork.PoojaProducts.UpdateAsync(product);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error updating pooja product with id {Id}", product.Id);
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var product = await _unitOfWork.PoojaProducts.GetByIdAsync(id);
//                 if (product == null)
//                 {
//                     _logger.LogWarning("Attempt to delete non-existing pooja product with id {Id}", id);
//                     return false;
//                 }

//                 await _unitOfWork.PoojaProducts.DeleteAsync(product);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting pooja product with id {Id}", id);
//                 return false;
//             }
//         }
//     }
// }