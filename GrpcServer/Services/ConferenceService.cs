using Grpc.Core;
using TestConference;
using static TestConference.OrganizingATAGTR;
using GrpcServer.DatabaseContext;
using GrpcServer.MappingExtensions;
using GrpcServer.Model;

namespace GrpcServer.Services
{
    public class ConferenceService(ILogger<ConferenceService> logger) : OrganizingATAGTRBase
    {
        private readonly ILogger<ConferenceService> _logger = logger;

        public override async Task<CreateConferenceResponse> CreateSpeakerDetails(ConferenceData request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: CreateSpeakerDetails");

            await using var appDbContext = new EntityModelContext();
            ConferenceDataModel conferenceData = request.MapToConferenceData();

            await appDbContext.ConferenceDataModel.AddAsync(conferenceData);
            await appDbContext.SaveChangesAsync();

            var response = new CreateConferenceResponse
            {
                Success = "Ok",
                TopicId = request.TopicId,
                Author = request.Author,
                Title = request.Title,
            };

            return await Task.FromResult(response);
        }

        public override async Task<ConferenceData> GetSpeakerDetails(GetConferenceRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: GetSpeakerDetails");

            await using var appDbContext = new EntityModelContext();

            var entity = appDbContext.ConferenceDataModel.Find(request.TopicId);
            return await Task.FromResult(entity!.MapToConferenceDataModel());
        }

        public override async Task<UpdateConferenceResponse> UpdateSpeakerDetails(ConferenceData request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: UpdateSpeakerDetails");

            UpdateConferenceResponse response;
            await using var appDbContext = new EntityModelContext();

            var entity = appDbContext.ConferenceDataModel.Find(request.TopicId);

            if (entity is not null)
            {
                entity.Title = request.Title;
                entity.Author = request.Author;
                entity.ConferenceType = request.ConferenceType;
                entity.Duration = request.Duration;
                entity.Coauthor = request.Coauthor;
                await appDbContext.SaveChangesAsync();

                response = new UpdateConferenceResponse
                {
                    TopicId = request.TopicId,
                    Success = "Updated",
                };
            }
            else
            {
                response = new UpdateConferenceResponse
                {
                    TopicId = request.TopicId,
                    Success = "Failure, No existing topic id found to update",
                };
            }

            return await Task.FromResult(response);
        }

        public override async Task<DeleteConferenceResponse> DeleteSpeakerDetails(DeleteConferenceRequest request, ServerCallContext context)
        {
            DeleteConferenceResponse response;
            await using var appDbContext = new EntityModelContext();

            var entity = appDbContext.ConferenceDataModel.Find(request.TopicId);

            if (entity is not null)
            {
                appDbContext.ConferenceDataModel.Remove(entity);
                await appDbContext.SaveChangesAsync();

                response = new DeleteConferenceResponse
                {
                    TopicId = request.TopicId,
                    Success = "Deleted",
                };
            }
            else
            {
                response = new DeleteConferenceResponse
                {
                    TopicId = request.TopicId,
                    Success = "Failure, No existing topic id found to delete",
                };
            }

            return await Task.FromResult(response);
        }
    }
}
