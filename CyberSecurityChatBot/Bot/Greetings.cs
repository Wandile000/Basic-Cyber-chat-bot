using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatbotSecurity
{
    internal class Greetings
    {
        public string Name;
        public string feeling;

        // Default constructor
        public Greetings()
        {
            Name = "User";  // Default value if the user doesn't set the name
        }

        // Method to display a customized message 
        public void DisplayFeelingMessage()
        {
            // Convert the feeling to lowercase to make the comparison case-insensitive
            string feelingLower = feeling.ToLower();

            if (feelingLower == "good" || feelingLower == "alright" || feelingLower == "great" || feelingLower == "ok")
            {
                Console.WriteLine($"WOW!! It's great that you are doing well, {Name}! \nHopefully, I can make it even better by sharing some Cyber Security knowledge.");
            }
            else if (feelingLower == "bad" || feelingLower == "sad" || feelingLower == "poor")
            {
                Console.WriteLine($"Sorry to hear that, {Name}. Hopefully, I can make it better by sharing some Cyber Security knowledge.");
            }
            else
            {
                Console.WriteLine("I see! Let's chat about Cyber Security.");
            }
        }

        // Method to display a standard greeting message
        public void DisplayInformation()
        {
            Console.WriteLine($"Well Hello: {Name}! It's quite a pleasure to meet you.");
            Console.WriteLine("I'm a Cyber Security chatbot, who can answer questions about Cyber security.");
        }
    }
}
