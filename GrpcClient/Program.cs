using TestConference;
using Grpc.Net.Client;
using Faker;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// gRPC Channel
using var channel = GrpcChannel.ForAddress("http://localhost:5000");

// gRPC client
var client = new OrganizingATAGTR.OrganizingATAGTRClient(channel);

// request to create new conference entry
var requestCreate = new ConferenceData
{
    TopicId = RandomNumber.Next(1, 10000).ToString(),
    Title = $"GRPC_Demo {RandomNumber.Next(60, 100)}",
    Author = Name.FullName(),
    Coauthor = string.Empty,
    ConferenceType = "Lab_Workshop",
    Duration = RandomNumber.Next(60, 100)
};

// response of created conference entry
var createResponse = await client.CreateSpeakerDetailsAsync(requestCreate);
Console.WriteLine(createResponse.ToString());

// request to get new conference entry by using topicId
GetConferenceRequest requestGet = new()
{
    TopicId = requestCreate.TopicId
};

// response to get new conference entry by using topicId
var getResponse = await client.GetSpeakerDetailsAsync(requestGet);
Console.WriteLine(getResponse.ToString());