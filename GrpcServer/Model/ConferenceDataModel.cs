using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GrpcServer.Model
{
    public class ConferenceDataModel()
    {
        [Key]
        public required string TopicId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ConferenceType { get; set; }
        public required int Duration { get; set; }
        public required string Coauthor { get; set; }
    }
}
