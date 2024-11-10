using Faker;
using TestConference;
using FluentAssertions;
using Grpc.Test.GrpcClient;

namespace Grpc.Test
{
    [TestClass]
    public class ConferenceTest
    {
        private ConferenceClient? conferenceClient;

        [TestInitialize]
        public void Start()
        {
            conferenceClient = new ConferenceClient();
        }

        [TestMethod]
        public async Task CreateConferenceDetail()
        {
            var request = new ConferenceData
            {
                TopicId = RandomNumber.Next(1, 10000).ToString(),
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

            // response of created conference entry
            var createResponse = await conferenceClient!.CreateSpeakerDetailsAsync(request);
            createResponse.Should().NotBeNull();
            createResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task GetConferenceDetailByTopicId()
        { 
            GetConferenceRequest requestGet = new()
            {
                TopicId = "7229"
            };

            // response to get new conference entry by using topicId
            var getResponse = await conferenceClient!.GetSpeakerDetailsAsync(requestGet);
            getResponse.Should().NotBeNull();
            getResponse.TopicId.Should().Be(requestGet.TopicId);
        }
    }
}