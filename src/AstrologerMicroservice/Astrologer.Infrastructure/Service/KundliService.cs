// using AstrologerMicroservice.Application.Service;
// using AstrologerMicroservice.Domain.Entities;
// using AstrologerMicroservice.Domain.UOW;
// using Shared.Application.Interfaces.Logging;

// namespace AstrologerMicroservice.Infrastructure.Service
// {
//     public class KundliService : IKundliService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILoggerService<KundliService> _logger;

//         public KundliService(ILoggerService<KundliService> logger, IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }

//         public async Task<Kundli?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _unitOfWork.Kundlis.GetByIdAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error fetching kundli by id {Id}", id);
//                 return null;
//             }
//         }

//         public async Task<bool> CreateAsync(Kundli kundli)
//         {
//             try
//             {
//                 await _unitOfWork.Kundlis.AddAsync(kundli);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating kundli");
//                 return false;
//             }
//         }

//         public async Task<bool> UpdateAsync(Kundli kundli)
//         {
//             try
//             {
//                 await _unitOfWork.Kundlis.UpdateAsync(kundli);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error updating kundli with id {Id}", kundli.Id);
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var kundli = await _unitOfWork.Kundlis.GetByIdAsync(id);
//                 if (kundli == null)
//                 {
//                     _logger.LogWarning("Attempt to delete non-existing kundli with id {Id}", id);
//                     return false;
//                 }

//                 await _unitOfWork.Kundlis.DeleteAsync(kundli);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting kundli with id {Id}", id);
//                 return false;
//             }
//         }
//     }
// }