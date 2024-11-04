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

    public static ConferenceData MapToConferenceDataModel(this ConferenceDataModel conferenceDataModel) => new()
    {
        TopicId = conferenceDataModel.TopicId,
        Title = conferenceDataModel.Title,
        Author = conferenceDataModel.Author,
        ConferenceType = conferenceDataModel.ConferenceType,
        Duration = conferenceDataModel.Duration,
        Coauthor = conferenceDataModel.Coauthor,
    };
}
