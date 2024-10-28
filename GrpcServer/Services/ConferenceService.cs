using Grpc.Core;
using TestConference;
using static TestConference.OrganizingATAGTR;
using System.Data.SqlClient;

namespace GrpcServer.Services
{
    public class ConferenceService(ILogger<ConferenceService> logger) : OrganizingATAGTRBase
    {
        private readonly ILogger<ConferenceService> _logger = logger;

        public override async Task<ConferenceData> GetSpeakerDetails(GetConferenceRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: GetSpeakerDetails");
            const string connectionString = @"Data Source=(localdb)\localdb;Initial Catalog=Speakers;Integrated Security=True;";
            string query = $"select * from dbo.SpeakerList where topicId = '{request.TopicId}'";
            await using SqlConnection conn = new(connectionString);
            conn.Open();
            await using SqlCommand cmd = new(query, conn);
            await using SqlDataReader reader = cmd.ExecuteReader();

            ConferenceData response = new();

            while (reader.Read())
            {
                response = new ConferenceData
                {
                    TopicId = reader["topicId"].ToString(),
                    Title = reader["title"].ToString(),
                    Author = reader["author"].ToString(),
                    Coauthor = reader["coauthor"].ToString(),
                    ConferenceType = reader["conferenceType"].ToString(),
                    Duration = Convert.ToInt32(reader["duration"])
                };
            }

            return await Task.FromResult(response);
        }

        public override async Task<CreateConferenceResponse> CreateSpeakerDetails(ConferenceData request, ServerCallContext context)
        {
            _logger.LogInformation("Received request to: CreateSpeakerDetails");

            const string connectionString = @"Data Source=(localdb)\localdb;Initial Catalog=Speakers;Integrated Security=True;";
            string query = $"insert into dbo.SpeakerList (topicId, title, author, conferenceType, duration, coauthor) values ('{request.TopicId}', '{request.Title}', '{request.Author}', '{request.ConferenceType}', {request.Duration}, null)";
            await using SqlConnection conn = new(connectionString);
            conn.Open();
            await using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();

            var response = new CreateConferenceResponse
            {
                Success = "Ok",
                Author = request.Author,
                Title = request.Title,
            };

            return await Task.FromResult(response);
        }
    }
}
