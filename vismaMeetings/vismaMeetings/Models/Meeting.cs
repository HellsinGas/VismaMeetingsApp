namespace vismaMeetings.Models
{
    using System.Text.Json.Serialization;
    using System.Globalization;
    using vismaMeetings.Repository;
    public class Meeting
    {
     

        
        public string? Name { get;  set; }
        public string? Description { get;  set; }
        public string? ResponsiblePerson { get; set; }
        public string? Category { get;  set; }
        public string? Type { get; set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }

        public Meeting(string? name, string? description, string? responsiblePerson, string? category, string? type, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            ResponsiblePerson = responsiblePerson;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }
      

        public Meeting()
        {
        }

        public Meeting CreateMeeting(string username)
        {
            ValidationClass validationClass = new ValidationClass();
            Meeting meeting = new Meeting();

            Console.WriteLine("Enter title  of the meeting");            
            meeting.Name = Console.ReadLine();

            Console.WriteLine("Enter Description of the meeting");
            meeting.Description = Console.ReadLine();

            Console.WriteLine("Enter person responsible for the meeting, A-z Only");
            meeting.ResponsiblePerson = validationClass.AzValidation();

            Console.WriteLine("Select Category");
            meeting.Category = CategorySelection();
            Console.WriteLine("Select Type");
            meeting.Type = TypeSelection();

            meeting.StartDate = StartDateInput();
            meeting.EndDate = validationClass.EndDateInput(meeting.StartDate);
            
          
            return meeting;

        }

        private DateTime StartDateInput()
        {
            ValidationClass validationClass = new ValidationClass();
            bool validDate = false;
            DateTime dateTime;
            while (!validDate)
            {
                dateTime = validationClass.DateInputValidation();
                int result = DateTime.Compare(dateTime, DateTime.Now);
                if (result < 0)
                    Console.WriteLine("You entered date thats earlier than today, please retry");                
                else
                    return dateTime;                                   

            }
            return DateTime.MaxValue;
        }

        public string? TypeSelection()
        {
            int checker = 0;
            while (checker != 1 && checker != 2)
            {

                Console.WriteLine("1 - Live");
                Console.WriteLine("2 - InPerson");                
                string? line = Console.ReadLine();
                if (int.TryParse(line, out int result))
                {
                    checker = int.Parse(line);
                }
            }
            if (checker == 1)
            {
                return "Live";
            }            
            else return "InPerson";
        }

        public string? CategorySelection()
        {
            int checker=0;
            while (checker != 1 && checker != 2 && checker != 3 && checker != 4)
            {
               
                Console.WriteLine("1 - CodeMonkey");
                Console.WriteLine("2 - Hub");
                Console.WriteLine("3 - Short");
                Console.WriteLine("4 - TeamBuilding");
                string? line = Console.ReadLine();
                if( int.TryParse(line, out int result))
                {
                    checker = int.Parse(line);                    
                }
            }
            if (checker== 1)
            {
                return "CodeMonkey";
            }
            if (checker == 2)
            {
                return "Hub";
            }
            if (checker == 3)
            {
                return "Short";
            }
            else return "TeamBuilding";
            
        }
    }
}
