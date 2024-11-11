using TestConference;
using Grpc.Net.Client;
using Faker;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// gRPC Channel
using var channel = GrpcChannel.ForAddress("http://localhost:5000");

// gRPC client
var client = new OrganizingATAGTR.OrganizingATAGTRClient(channel);

#region create conference entry
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
Console.WriteLine();

#endregion

#region get conference entry by topic id
// request to get new conference entry by using topicId
GetConferenceRequest requestGet = new()
{
    TopicId = requestCreate.TopicId
};

// response to get new conference entry by using topicId
var getResponse = await client.GetSpeakerDetailsAsync(requestGet);
Console.WriteLine(getResponse.ToString());
Console.WriteLine();

#endregion

#region update conference entry
// request to update conference entry
var requestUpdate = new ConferenceData
{
    TopicId = requestCreate.TopicId,
    Title = $"GRPC_Demo_update {RandomNumber.Next(60, 100)}",
    Author = Name.FullName(),
    Coauthor = "Updated Name",
    ConferenceType = "Lab_Workshop_Virtual_Update",
    Duration = RandomNumber.Next(60, 100)
};

// response of updated conference entry
var updateResponse = await client.UpdateSpeakerDetailsAsync(requestUpdate);
Console.WriteLine(updateResponse.ToString());
Console.WriteLine();

#endregion

#region delete conference entry

// request to delete conference entry
DeleteConferenceRequest requestDelete = new()
{
    TopicId = requestCreate.TopicId
};

// response of deleted conference entry
var deleteResponse = await client.DeleteSpeakerDetailsAsync(requestDelete);
Console.WriteLine(deleteResponse.ToString());
Console.WriteLine();

#endregion