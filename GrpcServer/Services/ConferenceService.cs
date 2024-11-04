using Grpc.Core;
using TestConference;
using static TestConference.OrganizingATAGTR;
using System.Data.SqlClient;
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
    }
}
