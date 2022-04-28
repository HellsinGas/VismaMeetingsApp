using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using vismaMeetings.Models;
using System.Text.Json.Serialization;

namespace vismaMeetings.Repository
{
    public class JsonMarshalling
    {

        public void Serialization(ListOfMeetingWithParticipants listOfMeetings)
        {
            string fileName = "VismaMeetings.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(listOfMeetings, options);
            File.WriteAllText(fileName, jsonString);

           // Console.WriteLine(File.ReadAllText(fileName));
        }

        public List<MeetingWithParticipants> DeSerialization(ListOfMeetingWithParticipants listOfMeetings)
        {
            string fileName = "VismaMeetings.json";
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                listOfMeetings = JsonSerializer.Deserialize<ListOfMeetingWithParticipants>(jsonString)!;
            }          
                               

            return listOfMeetings.listMeetings;  
        }
    }
}
