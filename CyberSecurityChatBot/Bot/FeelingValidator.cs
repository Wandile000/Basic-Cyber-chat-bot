using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatbotSecurity
{
    internal class FeelingValidator
    {
        string[] validFeelings = { "good", "alright", "great", "ok", "bad", "sad", "poor" };
        public string GetUserFeeling()
        {
            
            string feeling = "";

            while (true)
            {
                Console.WriteLine("How are you feeling today? (Good, Alright, Great, Ok, Bad, Sad, Poor)");
                feeling = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(feeling)) // Check if the input feeling is not null, empty, or just whitespace
                {
                    feeling = feeling.Trim().ToLower();    // Trim any extra spaces and convert the feeling to lowercase

                    if (validFeelings.Contains(feeling)) // Check if the feeling is valid by comparing it to the predefined list
                    {
                        return feeling;
                    }
                }

                Console.WriteLine("Invalid input. Please enter a valid feeling.");
            }
        }

        
        public string ValidateFeeling(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string trimmed = input.Trim().ToLower();
                if (validFeelings.Contains(trimmed))
                {
                    return trimmed;
                }
            }
            return null;
        }

    }
}