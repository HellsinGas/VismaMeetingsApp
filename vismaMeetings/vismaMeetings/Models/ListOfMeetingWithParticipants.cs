using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace vismaMeetings.Models
{
    public class ListOfMeetingWithParticipants
    {
        [JsonPropertyName("listMeetings")]
        public List<MeetingWithParticipants> listMeetings { get; set; }        

        public ListOfMeetingWithParticipants()
        {
        }

       /* public ListOfMeetingWithParticipants(string? name, string? description, string? category, string? type, DateTime startDate, DateTime endDate, List<string?>? participantsList) : base(name, description, category, type, startDate, endDate, participantsList)
        {
        }*/
    }
}
