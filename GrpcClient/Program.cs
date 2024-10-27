using TestConference;
using Grpc.Net.Client;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new OrganizingATAGTR.OrganizingATAGTRClient(channel);
GetConferenceRequest request = new()
{
    TopicId = "1"
};

var response = await client.GetSpeakerDetailsAsync(request);
Console.WriteLine(response.ToString());


