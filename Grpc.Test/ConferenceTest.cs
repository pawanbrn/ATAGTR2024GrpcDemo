using Faker;
using TestConference;
using FluentAssertions;
using Grpc.Test.GrpcClient;

namespace Grpc.Test
{
    [TestClass]
    public class ConferenceTest
    {
        private readonly string TopicId = "8083";
        private ConferenceClient? conferenceClient;
        private static readonly Dictionary<string, string>? storage = [];

        [TestInitialize]
        public void Start()
        {
            conferenceClient = new ConferenceClient();
        }

        [TestMethod]
        public async Task Test1_CreateConferenceDetail()
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
            storage?.TryAdd(TopicId, request.TopicId);
        }

        [TestMethod]
        public async Task Test2_GetConferenceDetailByTopicId()
        {
            string? storedTopicId = GetConferenceTopicId();

            GetConferenceRequest requestGet = new()
            {
                TopicId = storedTopicId
            };

            // response to get new conference entry by using topicId
            var getResponse = await conferenceClient!.GetSpeakerDetailsAsync(requestGet);
            getResponse.Should().NotBeNull();
            getResponse.TopicId.Should().Be(requestGet.TopicId);
        }

        [TestMethod]
        public Task Test3_UpdateConferenceDetails()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public Task Test4_UpdateNonExistingConferenceDetails()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public Task Test5_DeleteConferenceDetails()
        {
            throw new NotImplementedException();
        }

        private string GetConferenceTopicId()
        {
            var storedTopicId = string.Empty;
            storage?.TryGetValue(TopicId, out storedTopicId);
            storedTopicId ??= TopicId;
            return storedTopicId;
        }
    }
}