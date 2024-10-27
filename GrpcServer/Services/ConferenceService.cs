using Grpc.Core;
using TestConference;
using static TestConference.OrganizingATAGTR;

namespace GrpcServer.Services
{
    public class ConferenceService(ILogger<ConferenceService> logger) : OrganizingATAGTRBase
    {
        private readonly ILogger<ConferenceService> _logger = logger;

        public override Task<GetConferenceResponse> GetSpeakerDetails(GetConferenceRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: GetConferenceDetails");
            var response = new GetConferenceResponse
            {
                TopicId = request.TopicId,
                Title = "GRPC Demo",
                Author = "Pawan",
                Coauthor = string.Empty,
                ConferenceType = "Lab Workshop",
                Duration = 60
            };

            return Task.FromResult(response);
        }
    }
}
