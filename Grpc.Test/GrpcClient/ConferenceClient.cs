using Grpc.Net.Client;

using Contract = TestConference;

namespace Grpc.Test.GrpcClient
{
    internal class ConferenceClient
    {
        private readonly Contract.OrganizingATAGTR.OrganizingATAGTRClient _organizingATAGTRClient;

        public ConferenceClient()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            _organizingATAGTRClient = new Contract.OrganizingATAGTR.OrganizingATAGTRClient(channel);
        }

        public async Task<Contract.CreateConferenceResponse> CreateSpeakerDetailsAsync(Contract.ConferenceData request)
        {
            return await _organizingATAGTRClient.CreateSpeakerDetailsAsync(request);
        }

        public async Task<Contract.ConferenceData> GetSpeakerDetailsAsync(Contract.GetConferenceRequest request)
        {
            return await _organizingATAGTRClient.GetSpeakerDetailsAsync(request);
        }
    }
}
