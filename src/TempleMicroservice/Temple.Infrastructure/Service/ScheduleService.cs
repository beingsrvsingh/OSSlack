// using Temple.Application.Service;
// using Temple.Domain.Entities;
// using Temple.Domain.UOW;
// using Shared.Application.Interfaces.Logging;

// namespace Temple.Infrastructure.Service
// {
//     public class ScheduleService : IScheduleService
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly ILoggerService<ScheduleService> _logger;

//         public ScheduleService(ILoggerService<ScheduleService> logger, IUnitOfWork unitOfWork)
//         {
//             _unitOfWork = unitOfWork;
//             _logger = logger;
//         }

//         public async Task<Schedule?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _unitOfWork.Schedules.GetByIdAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error fetching schedule by id {Id}", id);
//                 return null;
//             }
//         }

//         public async Task<bool> CreateAsync(Schedule schedule)
//         {
//             try
//             {
//                 await _unitOfWork.Schedules.AddAsync(schedule);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating schedule");
//                 return false;
//             }
//         }

//         public async Task<bool> UpdateAsync(Schedule schedule)
//         {
//             try
//             {
//                 await _unitOfWork.Schedules.UpdateAsync(schedule);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error updating schedule with id {Id}", schedule.Id);
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var schedule = await _unitOfWork.Schedules.GetByIdAsync(id);
//                 if (schedule == null)
//                 {
//                     _logger.LogWarning("Attempt to delete non-existing schedule with id {Id}", id);
//                     return false;
//                 }

//                 await _unitOfWork.Schedules.DeleteAsync(schedule);
//                 await _unitOfWork.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting schedule with id {Id}", id);
//                 return false;
//             }
//         }
//     }
// }