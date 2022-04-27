// See https://aka.ms/new-console-template for more information
using vismaMeetings.Models;
using vismaMeetings.Repository;
//using vismaMeetings.Repository;
string? userName;


List<Meeting> meetingList = new List<Meeting>();
Meeting meeting = new Meeting();



List<MeetingWithParticipants> participantsList = new List<MeetingWithParticipants>();
MeetingWithParticipants participants = new MeetingWithParticipants();


ListOfMeetingWithParticipants listOfMeetingWithParticipants = new ListOfMeetingWithParticipants();



JsonMarshalling marshalling = new JsonMarshalling();

ValidationClass validation = new ValidationClass();

listOfMeetingWithParticipants.listMeetings = marshalling.DeSerialization(listOfMeetingWithParticipants);


bool exit = false;




Console.WriteLine("Hello, To start using the program, you must first log in...");
Console.WriteLine("Enter your name...(Case insensitive, A-Z,no simbols or figures");
userName = Console.ReadLine();
Console.WriteLine(userName);
while (!exit)
{
    Stars();
    Console.WriteLine("1. To create a new meeting");
    Console.WriteLine("2. To delete meeting");
    Console.WriteLine("3. Add person to an existing meeting");
    Console.WriteLine("4. Remove person from a meeting");
    Console.WriteLine("5. For meetings list and filtering options");
    Console.WriteLine("6. Exit");
    int input = int.Parse(Console.ReadLine());

    if(input == 1)
    {
        meeting = meeting.CreateMeeting(userName);
        listOfMeetingWithParticipants.listMeetings.Add(participants.OnMeetingCreation(meeting));

        marshalling.Serialization(listOfMeetingWithParticipants);
    }
    else if(input == 2)
    {
        listOfMeetingWithParticipants=participants.DeleteMeeting(userName, listOfMeetingWithParticipants);       
        marshalling.Serialization(listOfMeetingWithParticipants);
    }
    else if(input == 3)
    {
        listOfMeetingWithParticipants.listMeetings = participants.AddorRemoveParticipant(listOfMeetingWithParticipants.listMeetings, true);
        marshalling.Serialization(listOfMeetingWithParticipants);
    }
    else if(input == 4)
    {
        listOfMeetingWithParticipants.listMeetings = participants.AddorRemoveParticipant(listOfMeetingWithParticipants.listMeetings, false);
        marshalling.Serialization(listOfMeetingWithParticipants);
    }
    else if(input == 5)
    {
        MeetingFiltering meetingFiltering = new MeetingFiltering();
        meetingFiltering.FilteringMenu();

    }
    else if (input == 6)
    {
        exit = true;
    }
    else
    {
        Console.WriteLine("Bad input.");
    }
   
}

void Stars()
{
    Console.WriteLine("*****************************************************************");
}
/*foreach (MeetingWithParticipants m in participantsList)
{
    Console.WriteLine($"Meeting Title : {participants.Name}");
    Console.WriteLine($"Meeting Description : {participants.Description}");
    Console.WriteLine($"Meeting Category : {participants.Category}");
    Console.WriteLine($"Meeting Type : {participants.Type}");
    Console.WriteLine($"Meeting StartDate : {participants.StartDate}");
    Console.WriteLine($"Meeting EndDate : {participants.EndDate}");
    foreach (string? e in m.ParticipantsList)
    {
        Console.Write($"1. {e}, ");
    }
}*/
//foreach (MeetingWithParticipants m in participantsList)
//{
//meetingWithParticipantsList = participantsList;

//}





