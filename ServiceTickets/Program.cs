using System;
using System.IO;


namespace ServiceTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            String file = "ServiceTickets.csv";
            String choice;
            
            do{
            Console.WriteLine("Enter 1 to view tickets");
            Console.WriteLine("Enter 2 to add ticket");
            Console.WriteLine("Enter any other key to exit");
            choice = Console.ReadLine();
           
           if(choice == "1"){
           StreamReader sr = new StreamReader(file);

            if(File.Exists(file)){
           while(!sr.EndOfStream){
               String line = sr.ReadLine();
               Console.WriteLine(line);

           } sr.Close();}}

           if (choice == "2"){
            // create new file
            StreamWriter sw = new StreamWriter(file);

            // get Ticket ID
            Console.WriteLine("Enter your Ticket ID");
            String ticketID = Console.ReadLine();

            //  Get Summary
             Console.WriteLine("Enter Summary of Request");
            String summary = Console.ReadLine();

            //get Status
            Console.WriteLine("Enter Status of the ticket");
            String status = Console.ReadLine();
            
            // get Priority
            Console.WriteLine("Enter Priority Level");
            String priority = Console.ReadLine();

            // get Submitter
            Console.WriteLine("Enter your name");
            String submitter = Console.ReadLine();

            // get Assigned
            Console.WriteLine("Enter name of Assigned Employee");
            String assigned = Console.ReadLine();
            
            // get Watching
            Console.WriteLine("Enter name of employee watching");
            String watching = Console.ReadLine();

            sw.WriteLine("{0},{1},{2},{3},{4},{5}|{6}",ticketID,summary,status,priority,submitter,assigned,watching);
            sw.Close();
           }
            }while(choice == "1"|| choice == "2");
        }
    }
}
