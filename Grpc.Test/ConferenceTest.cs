using Faker;
using TestConference;
using FluentAssertions;
using Grpc.Net.Client;

namespace Grpc.Test
{
    [TestClass]
    public class ConferenceTest
    {
        [TestMethod]
        public async Task CreateConferenceDetail()
        {
            // gRPC Channel
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");

            // gRPC client
            var client = new OrganizingATAGTR.OrganizingATAGTRClient(channel);

            var request = new ConferenceData
            {
                TopicId = RandomNumber.Next(1, 10000),
                Title = $"GRPC Lab Demo {RandomNumber.Next(60, 100)}",
                Author = Name.FullName(),
                Coauthor = Name.Last(),
                ConferenceType = "Virtual",
                Duration = RandomNumber.Next(60, 100)
            };

            var expectedResponse = new CreateConferenceResponse
            {
                Success = "Ok",
                TopicId = request.TopicId,
                Author = request.Author,
                Title = request.Title,
            };

            var createResponse = await client.CreateSpeakerDetailsAsync(request);
            createResponse.Should().NotBeNull();
            createResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetConferenceDetailByTopicId()
        {
            // gRPC Channel
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");

            // gRPC client
            var client = new OrganizingATAGTR.OrganizingATAGTRClient(channel);

            GetConferenceRequest requestGet = new()
            {
                TopicId = 8776
            };

            // response to get new conference entry by using topicId
            var getResponse = await client.GetSpeakerDetailsAsync(requestGet);
            getResponse.Should().NotBeNull();
            getResponse.TopicId.Should().Be(requestGet.TopicId);
        }
    }
}