using System;
using System.IO;

namespace CyberChatbotSecurity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check if running in GitHub Actions or CI environment
            bool isCI = Environment.GetEnvironmentVariable("CI") == "true";

            // Create instances of classes
            ImageDisply imageDisplay = new ImageDisply();
            Greetings greet = new Greetings();
            FeelingValidator feelingValidator = new FeelingValidator();
            AudioPlayer audioPlayer = new AudioPlayer();
            ASCIITextMessage textMessage = new ASCIITextMessage();
            Cyber_Chat cyberChat = new Cyber_Chat();
            Utils utils = new Utils();

            // Fix circular dependency
            utils.ChatInstance(cyberChat);

            // Path to files
            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Cyber Voice.m4a");
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Cyber_Security-Info.txt");

            // Load topics
            cyberChat.LoadTopics(filePath);

            // Show ASCII art
            imageDisplay.Show();

            // Try playing audio
            try
            {
                audioPlayer.PlayAudio(audioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio playback failed: {ex.Message}");
            }

            // Display ASCII banner text
            textMessage.DisplayAscii();

            // Handle greeting
            if (isCI)
            {
                greet.Name = "GithubUser";
                Console.WriteLine("Using default Name for CI environment: " + greet.Name);
            }
            else
            {
                Console.WriteLine("Please enter your name:");
                greet.Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(greet.Name))
                {
                    greet.Name = "Anonymous";
                }
            }

            greet.DisplayInformation();

            // Handle feelings
            if (isCI)
            {
                greet.feeling = feelingValidator.ValidateFeeling("good") ?? "good";
                Console.WriteLine($"[CI MODE] Auto-selected feeling: {greet.feeling}");
            }
            else
            {
                greet.feeling = feelingValidator.GetUserFeeling();
            }

            greet.DisplayFeelingMessage();
            utils.SetGreetings(greet);
            cyberChat.SetUtils(utils);     //  Link the same Utils instance
            utils.ChatInstance(cyberChat); //  Set Cyber_Chat inside Utils 
            utils.CyberInterest();         // Start the interaction

            // Wait for input if not in CI
            if (!isCI)
            {
                Console.ReadKey();
            }
        }
    }
}
