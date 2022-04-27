using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json.Serialization;
using vismaMeetings.Repository;

namespace vismaMeetings.Models
{
    public class MeetingWithParticipants : Meeting
    {
        
        public List<string?>? ParticipantsList { get; set; }

        /* public Participants(List<string?>? participantsList)
         {
             ParticipantsList = participantsList;
        }*/       

       
        public MeetingWithParticipants()
        {
        }

        public MeetingWithParticipants(string? name, string? description, string? responsiblePerson, string? category, string? type, DateTime startDate, DateTime endDate, List<string?>? participantsList) : base(name, description, responsiblePerson, category, type, startDate, endDate)
        {
            ParticipantsList = participantsList;
        }


        public MeetingWithParticipants OnMeetingCreation(Meeting meet)
        {
            MeetingWithParticipants meetingWithParticipants = new MeetingWithParticipants();
            meetingWithParticipants = Populate(meet);

            meetingWithParticipants.ParticipantsList.Add($"{meetingWithParticipants.ResponsiblePerson} Added on : {DateTime.Now} ");

            return meetingWithParticipants;
        }
        public List<MeetingWithParticipants> AddorRemoveParticipant(List<MeetingWithParticipants> meetingWithParticipants, bool removeOrAdd)
        {
            ValidationClass validationClass = new ValidationClass();            
            Console.WriteLine("Into which meeting do you wish to add a person");            
            bool validation = false;
            while (!validation)
            {
                ShowMeetingTitles(meetingWithParticipants);
                string input = Console.ReadLine();
                int count = 0;
                foreach (MeetingWithParticipants m in meetingWithParticipants)
                {
                    if (validationClass.StringEqualityValidation(input, m.Name))
                    {
                        if (removeOrAdd)
                        {
                            meetingWithParticipants[count] = AddParticipant(m);
                            return meetingWithParticipants;
                        }
                        if (!removeOrAdd)
                        {
                            meetingWithParticipants[count] = RemoveParticipant(m);
                            return meetingWithParticipants;
                        }
                    }                    
                   
                    count++;
                }
                if (!validation)
                    Console.WriteLine(" Error: Bad input, specified meeting not found ");

            }           

            return meetingWithParticipants;


        }

        private MeetingWithParticipants RemoveParticipant(MeetingWithParticipants m)
        {
            ValidationClass validationClass = new ValidationClass();
            ShowMeetingParticipants(m.ParticipantsList);
            Console.WriteLine("Enter the name of person you wish to add :");            
            string input = Console.ReadLine();
            int count = 0;
            foreach (string participant in m.ParticipantsList)
            {
                if (validationClass.CheckIfStringContains(participant, input))
                {
                    if (validationClass.CheckIfStringContains(participant, m.ResponsiblePerson))
                    {
                        Console.WriteLine("This person is Responsible for this meeting and cannot be removed, Exiting.");
                        return m;
                    }
                    else
                    {
                        Console.WriteLine($"Participant {input} has been removed, exiting.");
                        m.ParticipantsList.RemoveAt(count);
                        return m;
                    }

                }
                count++;
            }         
            
            return m;
        }

        private MeetingWithParticipants AddParticipant(MeetingWithParticipants m)
        {
            ValidationClass validationClass = new ValidationClass();
            Console.WriteLine("Enter the name of person you wish to add :");
            string input = Console.ReadLine();
            foreach(string participant in m.ParticipantsList)
            {
                if (validationClass.CheckIfStringContains(participant,input))
                {
                    Console.WriteLine("This person is allready in this meeting, Exiting.");
                    return m;                   
                    
                }
            }
            CheckIntersectionWithAnotherMeeting(input, m);
            m.ParticipantsList.Add($"{input} Added on : {DateTime.Now}");
            return m;
        }

        private void CheckIntersectionWithAnotherMeeting(string name,MeetingWithParticipants meeting1 )
        {
            MeetingFiltering meetingFiltering = new MeetingFiltering();
            List<MeetingWithParticipants> meetingWithParticipants = new List<MeetingWithParticipants>();

            meetingWithParticipants= meetingFiltering.FindMeetingsWherePersonIsAdded(name);

            foreach(MeetingWithParticipants meetingWithParticipant in meetingWithParticipants)
            {
                if (meeting1.StartDate.Ticks > meetingWithParticipant.StartDate.Ticks && meeting1.StartDate.Ticks < meetingWithParticipant.EndDate.Ticks)
                {
                    Console.WriteLine($"Person whos beeing added intersects with meeting:{meetingWithParticipant.Name}");
                }
            }
            
        }

        private MeetingWithParticipants Populate(Meeting meetingObj)
        {
            MeetingWithParticipants participants1 = new MeetingWithParticipants(meetingObj.Name, meetingObj.Description, meetingObj.ResponsiblePerson, meetingObj.Category, meetingObj.Type, meetingObj.StartDate, meetingObj.EndDate, new List<string?>());          

            return participants1;

        }

        public ListOfMeetingWithParticipants DeleteMeeting(string username, ListOfMeetingWithParticipants listOfMeetings)
        {
            ValidationClass validationClass = new ValidationClass();

            Console.WriteLine("Which Meeting do you wish to delete?");
            bool validation = false;
            while (!validation)
            {
                ShowMeetingTitles(listOfMeetings.listMeetings);
                string input = Console.ReadLine();
                foreach (MeetingWithParticipants m in listOfMeetings.listMeetings)
                {                  
                    if (validationClass.StringEqualityValidation(input, m.Name))
                    {
                        if (validationClass.StringEqualityValidation(username, m.ResponsiblePerson))
                        {
                            listOfMeetings.listMeetings.Remove(m);
                            validation=true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Only person responsible for the meeting can delete it. Responsible person :{m.ResponsiblePerson}");
                            Console.WriteLine("Enter anything to return to menu");
                            Console.ReadLine();
                            return listOfMeetings;
                        }
                            

                    }
                        
                }
                if (!validation)
                    Console.WriteLine(" Error: Bad input, specified meeting not found ");

            }
            return listOfMeetings;

        }

        private void ShowMeetingTitles(List<MeetingWithParticipants> listMeetings)
        {
            Console.WriteLine("List of meetings :");
            foreach(MeetingWithParticipants m in listMeetings)
            {
                Console.WriteLine(m.Name);
            }
        }

        private void ShowMeetingParticipants(List<string> participantsList)
        {
            Console.WriteLine("List of participants in the meeting :");
            foreach (string m in participantsList)
            {
                Console.WriteLine(m);
            }
        }
    }
}
