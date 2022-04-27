using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vismaMeetings.Models;

namespace vismaMeetings.Repository
{
    public class MeetingFiltering
    {

        void Stars()
        {
            Console.WriteLine("*****************************************************************");
        }
        public void FilteringMenu()
        {
            
            JsonMarshalling jsonMarshalling = new JsonMarshalling();
            ListOfMeetingWithParticipants listOfMeetingsWrapper = new ListOfMeetingWithParticipants();
            listOfMeetingsWrapper.listMeetings = jsonMarshalling.DeSerialization(listOfMeetingsWrapper);
            bool exit = false;
            while (!exit)
            {
                Stars();
                Console.WriteLine("1. Display all meetings");
                Console.WriteLine("2. Filter by Description");
                Console.WriteLine("3. Filter by Category");
                Console.WriteLine("4. Filter by Type");
                Console.WriteLine("5. Filter by date");
                Console.WriteLine("6. Filter by number of Participants");
                int input = int.Parse(Console.ReadLine());

                if (input == 1)
                {
                    DisplayFullMeetingInfoAll(listOfMeetingsWrapper.listMeetings);
                }
                else if (input == 2)
                {
                    FilterByDescription(listOfMeetingsWrapper.listMeetings);
                }
                else if (input == 3)
                {
                    FilterByCategory(listOfMeetingsWrapper.listMeetings);
                }
                else if (input == 4)
                {
                    FilterByType(listOfMeetingsWrapper.listMeetings);
                }
                else if (input == 5)
                {
                    FilterByDate(listOfMeetingsWrapper.listMeetings);

                }
                else if (input == 6)
                {
                    FilterByNumberOfParticipants(listOfMeetingsWrapper.listMeetings);
                }
                else if(input == 7)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Bad input.");
                }

            }
        }

        private void FilterByNumberOfParticipants(List<MeetingWithParticipants> listMeetings)
        {
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            Console.WriteLine("Enter number for search in formula (your input)< Participants count");
            string? input = Console.ReadLine();
            int value;
            while (true)
            {
                if (int.TryParse(input, out value))
                {
                    break;
                }
                Console.WriteLine("Please enter Number");
                input = Console.ReadLine();
            }

            foreach (MeetingWithParticipants meeting in listMeetings)
            {
                if (meeting.ParticipantsList.Count() > value)
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void FilterByDate(List<MeetingWithParticipants> listMeetings)
        {
            Console.WriteLine("1. Filter from 'Date' , 2. Filter between dates");
            while (true)
            {
                int input = int.Parse(Console.ReadLine());

                if(input == 1)
                {
                    FilterByDateFrom(listMeetings);
                    return;
                }
                if(input == 2)
                {
                    FilterbyDateBetween(listMeetings);
                    return;
                }
                Console.WriteLine("Bad input.");
            }

        }

        private void FilterbyDateBetween(List<MeetingWithParticipants> listMeetings)
        {
            ValidationClass validationClass = new ValidationClass();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            Console.WriteLine("Input Date From :");
            DateTime inputDateTimeFrom = validationClass.DateInputValidation();
            Console.WriteLine("Input Date To :");
            DateTime inputDateTimeTo = validationClass.DateInputValidation();
            foreach (MeetingWithParticipants meeting in listMeetings)
            {
                if (meeting.StartDate > inputDateTimeFrom && meeting.StartDate < inputDateTimeTo)
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void FilterByDateFrom(List<MeetingWithParticipants> listMeetings)
        {
            ValidationClass validationClass = new ValidationClass();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();

            DateTime inputDateTime = validationClass.DateInputValidation();
            foreach(MeetingWithParticipants meeting in listMeetings)
            {
                if(meeting.StartDate > inputDateTime)
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void FilterByType(List<MeetingWithParticipants> listMeetings)
        {
            Meeting meet = new Meeting();            
            Console.WriteLine("Select type to filter");

            string? input = meet.TypeSelection();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            ValidationClass validationClass = new ValidationClass();
            foreach (MeetingWithParticipants meeting in listMeetings)
            {
                if (validationClass.StringEqualityValidation(meeting.Type, input))
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void FilterByCategory(List<MeetingWithParticipants> listMeetings)
        {
            Meeting meet = new Meeting();
            Console.WriteLine("Select Category to filter");

            string? input = meet.CategorySelection();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            ValidationClass validationClass = new ValidationClass();
            foreach (MeetingWithParticipants meeting in listMeetings)
            {
                if (validationClass.StringEqualityValidation(meeting.Category, input))
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void FilterByDescription(List<MeetingWithParticipants> listMeetings)
        {
            Console.WriteLine("Enter phrase with which to filter");
            string? input = Console.ReadLine();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            ValidationClass validationClass = new ValidationClass();
            foreach (MeetingWithParticipants meeting in listMeetings)
            {
                if(validationClass.CheckIfStringContains(meeting.Description,input))
                    filteredList.Add(meeting);
            }
            if (filteredList.Count > 0)
            {
                DisplayFullMeetingInfoAll(filteredList);
            }
            else
                Console.WriteLine("Could not find a meeting which would meet search parameters");
        }

        private void DisplayFullMeetingInfoAll(List<MeetingWithParticipants> listMeetings)
        {
            foreach(MeetingWithParticipants meeting in listMeetings)
            {
                Stars();
                Console.WriteLine($"Meeting Title : {meeting.Name}");
                Console.WriteLine($"Meeting Description : {meeting.Description}");
                Console.WriteLine($"Meeting Description : {meeting.ResponsiblePerson}");
                Console.WriteLine($"Meeting Category : {meeting.Category}");
                Console.WriteLine($"Meeting Type : {meeting.Type}");
                Console.WriteLine($"Meeting StartDate : {meeting.StartDate}");
                Console.WriteLine($"Meeting EndDate : {meeting.EndDate}");
                int i = 1;
                foreach (string? e in meeting.ParticipantsList)
                {               
                    Console.Write($"{i++}. {e}, ");
                }
                Console.WriteLine();
            }
        }
        public List<MeetingWithParticipants> FindMeetingsWherePersonIsAdded(string name)
        {
            ValidationClass validationClass = new ValidationClass();
            JsonMarshalling jsonMarshalling = new JsonMarshalling();
            List<MeetingWithParticipants> filteredList = new List<MeetingWithParticipants>();
            ListOfMeetingWithParticipants listOfMeetingsWrapper = new ListOfMeetingWithParticipants();

            listOfMeetingsWrapper.listMeetings = jsonMarshalling.DeSerialization(listOfMeetingsWrapper);
            foreach(MeetingWithParticipants meeting in listOfMeetingsWrapper.listMeetings)
            {
                foreach(string e in meeting.ParticipantsList)
                {
                    if (validationClass.CheckIfStringContains(e, name))
                        filteredList.Add(meeting);
                }
            }

            return filteredList;
        }
        
    }
}
