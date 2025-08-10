// using AstrologerMicroservice.Application.Service;
// using AstrologerMicroservice.Domain.Entities;
// using AstrologerMicroservice.Domain.UOW;
// using Shared.Application.Interfaces.Logging;

// namespace AstrologerMicroservice.Infrastructure.Service
// {
//     public class PoojaService : IPoojaService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILoggerService<PoojaService> _logger;

//         public PoojaService(ILoggerService<PoojaService> logger, IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }

//         public async Task<Pooja?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _unitOfWork.Poojas.GetByIdAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error fetching pooja by id {Id}", id);
//                 return null;
//             }
//         }

//         public async Task<bool> CreateAsync(Pooja pooja)
//         {
//             try
//             {
//                 await _unitOfWork.Poojas.AddAsync(pooja);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating pooja");
//                 return false;
//             }
//         }

//         public async Task<bool> UpdateAsync(Pooja pooja)
//         {
//             try
//             {
//                 await _unitOfWork.Poojas.UpdateAsync(pooja);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error updating pooja with id {Id}", pooja.Id);
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var pooja = await _unitOfWork.Poojas.GetByIdAsync(id);
//                 if (pooja == null)
//                 {
//                     _logger.LogWarning("Attempt to delete non-existing pooja with id {Id}", id);
//                     return false;
//                 }

//                 await _unitOfWork.Poojas.DeleteAsync(pooja);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting pooja with id {Id}", id);
//                 return false;
//             }
//         }
//     }
// }