using GrpcServer.Model;
using TestConference;

namespace GrpcServer.MappingExtensions;

public static class ConferenceDataMappingExtensions
{
    public static ConferenceDataModel MapToConferenceData(this ConferenceData conferenceData) => new()
    {
        TopicId = conferenceData.TopicId,
        Title = conferenceData.Title,
        Author = conferenceData.Author,
        ConferenceType = conferenceData.ConferenceType,
        Duration = conferenceData.Duration,
        Coauthor = conferenceData.Coauthor,
    };
}
