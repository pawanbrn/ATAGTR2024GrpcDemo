using Faker;
using TestConference;
using FluentAssertions;
using Grpc.Test.GrpcClient;

namespace Grpc.Test
{
    [TestClass]
    public class ConferenceTest
    {
        private readonly string TopicId = "7229";
        private ConferenceClient? conferenceClient;
        private static readonly Dictionary<string, string>? storage = [];

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
            storage?.TryAdd(TopicId, request.TopicId);
        }

        [TestMethod]
        public async Task GetConferenceDetailByTopicId()
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
        public async Task UpdateConferenceDetails()
        {
            string? storedTopicId = TopicId;

            var requestUpdate = new ConferenceData
            {
                TopicId = storedTopicId,
                Title = $"GRPC_Demo_update {RandomNumber.Next(60, 100)}",
                Author = Name.FullName(),
                Coauthor = $"Updated Name {Name.First}",
                ConferenceType = $"Lab_Workshop_Virtual_Update{RandomNumber.Next(1, 100)}",
                Duration = RandomNumber.Next(60, 100)
            };

            var expectedResponse = new UpdateConferenceResponse
            {
                Success = "Updated",
                TopicId = storedTopicId,
            };

            // response to update conference entry
            var updateResponse = await conferenceClient!.UpdateSpeakerDetailsAsync(requestUpdate);
            updateResponse.Should().NotBeNull();
            updateResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task UpdateNonExistingConferenceDetails()
        {
            const string nonExistingTopicId = "123456";
            var requestUpdate = new ConferenceData
            {
                TopicId = nonExistingTopicId,
                Title = $"GRPC_Demo_update {RandomNumber.Next(60, 100)}"
            };

            var expectedResponse = new UpdateConferenceResponse
            {
                Success = "Failure, No existing topic id found to update",
                TopicId = nonExistingTopicId,
            };

            // response to update non existing conference entry
            var updateResponse = await conferenceClient!.UpdateSpeakerDetailsAsync(requestUpdate);
            updateResponse.Should().NotBeNull();
            updateResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [TestMethod]
        public async Task DeleteConferenceDetails()
        {
            string? storedTopicId = GetConferenceTopicId();
            DeleteConferenceRequest requestDelete = new()
            {
                TopicId = storedTopicId
            };

            var expectedResponse = new DeleteConferenceResponse
            {
                Success = "Deleted",
                TopicId = storedTopicId,
            };

            // response to delete conference entry
            var updateResponse = await conferenceClient!.DeleteSpeakerDetailsAsync(requestDelete);
            updateResponse.Should().NotBeNull();
            updateResponse.Should().BeEquivalentTo(expectedResponse);
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