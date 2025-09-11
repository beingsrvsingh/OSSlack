using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Repositories;
using Mapster;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;

namespace AstrologerMicroservice.Infrastructure.Service
{

    public class AstrologerService : IAstrologerService
    {
        private readonly IAstrologerRepository _repository;
        private readonly ILoggerService<AstrologerService> _logger;

        public AstrologerService(IAstrologerRepository repository, ILoggerService<AstrologerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<AstrologerEntity>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            try
            {
                return await _repository.GetAvailableAsync(date, language, expertise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch available astrologers.");
                return Enumerable.Empty<AstrologerEntity>();
            }
        }

        public async Task<AstrologerEntity?> GetByIdAsync(int astrologerId)
        {
            _logger.LogInfo($"Getting astrologer by Id: {astrologerId}");
            try
            {
                var astrologer = await _repository.GetByIdAsync(astrologerId);
                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with Id {astrologerId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<AstrologerEntity?> GetByUserIdAsync(string userId)
        {
            _logger.LogInfo($"Getting astrologer by UserId: {userId}");
            try
            {
                var astrologer = await _repository.GetByIdAsync(userId);
                if (astrologer == null)
                    _logger.LogWarning($"Astrologer with UserId {userId} not found.");
                return astrologer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving astrologer with UserId {userId}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<AstrologerEntity>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            _logger.LogInfo($"Getting all astrologers - Page: {page}, PageSize: {pageSize}");
            try
            {
                return await _repository.GetAllAsync(page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving astrologers", ex);
                throw;
            }
        }

        public async Task<bool> CreateAsync(CreateAstrologerCommand command)
        {
            _logger.LogInfo("Creating new astrologer");

            try
            {
                // Check if astrologer exists by unique field, e.g. Email
                var exists = await _repository.AnyAsync(a => a.UserId == command.UserId);
                if (exists)
                {
                    _logger.LogWarning($"Astrologer with userId {command.UserId} already exists.");
                    return false; // or you can throw, or handle as needed
                }

                // Create new astrologer
                var astrologer = command.Adapt<AstrologerEntity>();

                astrologer.AstrologerLanguages.Clear();
                foreach (var languageEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        LanguageId = (int)languageEnum
                    });
                }

                astrologer.AstrologerExpertises.Clear();
                //foreach (var expertise in command.Expertise)
                //{
                //    astrologer.AstrologerExpertises.Add(new AstrologerExpertise
                //    {
                //        ExpertiseId = (int)expertise
                //    });
                //}

                await _repository.AddAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with userId {command.UserId} created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating astrologer", ex);
                return false;
            }
        }


        public async Task<bool> UpdateAsync(UpdateAstrologerCommand command)
        {
            _logger.LogInfo($"Updating astrologer with Id {command.Id}");

            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(command.Id);
                if (astrologer == null)
                {
                    _logger.LogWarning($"Astrologer with Id {command.Id} not found.");
                    return false;
                }

                // Update scalar properties
                astrologer.Adapt(command);

                // Clear existing languages and insert new ones
                astrologer.AstrologerLanguages.Clear();
                foreach (var langEnum in command.Languages)
                {
                    astrologer.AstrologerLanguages.Add(new AstrologerLanguage
                    {
                        LanguageId = (int)langEnum
                    });
                }

                // Clear existing expertises and insert new ones
                astrologer.AstrologerExpertises.Clear();
                //foreach (var expEnum in command.Expertise)
                //{
                //    astrologer.AstrologerExpertises.Add(new AstrologerExpertise
                //    {
                //        ExpertiseId = (int)expEnum
                //    });
                //}

                await _repository.UpdateAsync(astrologer);
                await _repository.SaveChangesAsync();

                _logger.LogInfo($"Astrologer with Id {command.Id} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating astrologer with Id {command.Id}: {ex.Message}", ex);
                return false;
            }
        }



        public async Task<bool> DeleteAsync(int astrologerId)
        {
            _logger.LogInfo($"Deleting astrologer with Id: {astrologerId}");
            try
            {
                var astrologer = await _repository.GetAstrologerWithLanguagesAndExpertisesAsync(astrologerId);
                if (astrologer is null)
                {
                    _logger.LogInfo($"astrologer not found with Id: {astrologerId}");
                    return false;   
                }
                await _repository.DeleteAsync(astrologer);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting astrologer with Id {astrologerId}", ex);
                throw;
            }
        }

        public async Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var (products, totalCount) = await _repository.SearchAsync(query, page, pageSize, cancellationToken);

                var resultDtos = products.Select(p => new SearchItemDto
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description ?? "",
                    Price = (double)(p.Price ?? 0),
                    ThumbnailUrl = p.ThumbnailUrl ?? "",
                    Score = p.Score,
                    MatchType = p.MatchType ?? "Partial",
                    CategoryId = p.CategoryId,
                    SubcategoryId = p.SubcategoryId
                }).ToList();

                var normalizedQuery = query.Trim();

                bool isCatOrSubcatExact = products.Any(p =>
                    string.Equals(p.CatSnap?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(p.SubcatSnap?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                bool isNameExact = products.Any(p =>
                    string.Equals(p.Name?.Trim(), normalizedQuery, StringComparison.OrdinalIgnoreCase));

                string matchType = isCatOrSubcatExact || isNameExact ? "Exact" : "Partial";

                bool enableFilters = isCatOrSubcatExact || products.Any(p =>
                (p.CatSnap?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (p.SubcatSnap?.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ?? false));

                return new SearchResultDto
                {
                    Results = resultDtos,
                    TotalCount = totalCount,
                    HasMoreResults = page * pageSize < totalCount,
                    Score = products.FirstOrDefault()?.Score ?? 0,
                    MatchType = matchType,
                    EnableFilters = enableFilters,
                    Source = "Astrologer"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for products. Query: '{Query}', Page: {Page}, PageSize: {PageSize}", query, page, pageSize);
                return new SearchResultDto
                {
                    Results = new List<SearchItemDto>(),
                    TotalCount = 0,
                    HasMoreResults = false,
                    Score = 0,
                    MatchType = "Partial",
                    EnableFilters = false,
                    Source = "Astrologer"
                };
            }
        }
    }

}