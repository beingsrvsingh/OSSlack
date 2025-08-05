// using AstrologerMicroservice.Application.Service;
// using AstrologerMicroservice.Domain.Entities;
// using AstrologerMicroservice.Domain.UOW;
// using Shared.Application.Interfaces.Logging;

// namespace AstrologerMicroservice.Infrastructure.Service
// {
//     public class ConsultationService : IConsultationService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILoggerService<ConsultationService> _logger;

//         public ConsultationService(ILoggerService<ConsultationService> logger, IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }

//         public async Task<Consultation?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _unitOfWork.Consultations.GetByIdAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error fetching consultation by id {Id}", id);
//                 return null;
//             }
//         }

//         public async Task<bool> CreateAsync(Consultation consultation)
//         {
//             try
//             {
//                 await _unitOfWork.Consultations.AddAsync(consultation);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating consultation");
//                 return false;
//             }
//         }

//         public async Task<bool> UpdateAsync(Consultation consultation)
//         {
//             try
//             {
//                 await _unitOfWork.Consultations.UpdateAsync(consultation);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error updating consultation with id {Id}", consultation.Id);
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var consultation = await _unitOfWork.Consultations.GetByIdAsync(id);
//                 if (consultation == null)
//                 {
//                     _logger.LogWarning("Attempt to delete non-existing consultation with id {Id}", id);
//                     return false;
//                 }

//                 await _unitOfWork.Consultations.DeleteAsync(consultation);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting consultation with id {Id}", id);
//                 return false;
//             }
//         }
//     }
// }