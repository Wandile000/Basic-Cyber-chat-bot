using System;
using System.Collections.Generic;

namespace CyberChatbotSecurity
{
    internal class Utils
    {
        // Stores greeting instance and user interest
        private Greetings greetings;
        private string InterestTopic = null;

        // Tracks how many times each topic was interacted with
        private Dictionary<string, int> topicInteractions = new Dictionary<string, int>();
        private int totalInteractions = 0;         // Total user inputs
        private int nonInterestInteractions = 0;   // How many unrelated topics since interest was set

        private Cyber_Chat chats;

        // Set a reference to the main chatbot so we can call its methods
        public void ChatInstance(Cyber_Chat chat)
        {
            chats = chat;
        }

        // Store the greetings instance so we can access the user's name
        public void SetGreetings(Greetings greet)
        {
            greetings = greet;
        }

        // Ask user if they have a preferred cybersecurity topic
        public void CyberInterest()
        {
            string interest, confirm;
            bool isCI = Environment.GetEnvironmentVariable("CI") == "true";
            string defaultCIConfirm = "no";

            Console.WriteLine(" ");
            Console.WriteLine($"So {greetings.Name}, do you have an interest in any Cyber Security topics?");
            confirm = isCI ? defaultCIConfirm : Console.ReadLine()?.Trim().ToLower();

            // Handle invalid or empty input
            if (confirm == null)
            {
                Console.WriteLine(" ");
                Console.WriteLine("No input detected. Exiting.");
                Environment.Exit(0);
            }

            // Handle exit request
            if (confirm == "exit")
            {
                Console.WriteLine(" ");
                Console.WriteLine("Goodbye, stay safe!");
                Environment.Exit(0);
            }

            // If the user says yes, ask for the specific topic of interest
            if (confirm.Contains("yes"))
            {
                Console.WriteLine(" ");
                Console.WriteLine("Really!? What is the topic?");
                interest = isCI ? "encryption" : Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(interest))
                {
                    interest = interest.ToLower();
                    InterestTopic = null;

                    // Get list of valid topic names (without suffixes like _tips)
                    List<string> knownTopics = new List<string>();
                    foreach (var key in Cyber_Chat.cyberTopics.Keys)
                    {
                        string baseTopic = key.Split('_')[0];
                        if (!knownTopics.Contains(baseTopic))
                        {
                            knownTopics.Add(baseTopic);
                        }
                    }

                    // Try to match the entered interest to a known topic
                    foreach (var topic in knownTopics)
                    {
                        if (interest.Contains(topic))
                        {
                            InterestTopic = topic;
                            break;
                        }
                    }

                    // If no match found, still store the input
                    if (InterestTopic == null)
                    {
                        InterestTopic = interest;
                    }

                    Console.WriteLine(" ");
                    Console.WriteLine($"Nice, I too find {InterestTopic} quite fascinating, even though my knowledge on it is limited.");
                }

                // Launch chatbot with interest stored
                chats.SetUtils(this);
                chats.Start();
            }
            else if (confirm.Contains("no"))
            {
                Console.WriteLine(" ");
                Console.WriteLine("That's alright, after all I'm here to share info on Cyber Security.");
                chats.SetUtils(this);
                chats.Start();
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("Please answer with 'yes', 'no', or 'exit'.");
                if (!isCI)
                    CyberInterest(); // Repeat prompt if invalid answer
            }
        }

        // Called whenever user interacts with a topic
        public void TrackInteraction(string topic = null)
        {
            totalInteractions++;

            if (!string.IsNullOrEmpty(topic))
            {
                if (!topicInteractions.ContainsKey(topic))
                    topicInteractions[topic] = 0;

                topicInteractions[topic]++;

                // If user has an interest and just mentioned something else...
                if (!string.IsNullOrEmpty(InterestTopic) && topic != InterestTopic)
                {
                    nonInterestInteractions++;

                    // After 3 unrelated interactions, show a fact about their interest
                    if (nonInterestInteractions == 3)
                    {
                        ShowInterestingFact();
                        nonInterestInteractions = 0;
                    }
                }
            }
        }

        // Display a random tip for a given topic (only checks _tips keys)
        public void GiveTipsOnTopic(string topic)
        {
            topic = topic.ToLower().Trim();
            string tipKey = topic + "_tips";

            Console.WriteLine();

            if (Cyber_Chat.cyberTopics.ContainsKey(tipKey))
            {
                var tips = Cyber_Chat.cyberTopics[tipKey];
                Console.WriteLine($"Here are some helpful tips on {topic}:");
                Console.WriteLine(tips[new Random().Next(tips.Count)]);
            }
            else
            {
                Console.WriteLine($"Hmm, I don’t have tips on '{topic}' yet. Try asking about another cybersecurity topic.");
            }

            Console.WriteLine();
        }

        // Show a fun fact about the user's declared interest topic
        public void ShowInterestingFact()
        {
            if (string.IsNullOrEmpty(InterestTopic))
                return;

            string InterestKey = $"{InterestTopic.ToLower()}_interest";

            Console.WriteLine("\nOh hey! I remember you said you're interested in " + InterestTopic + ".");

            if (Cyber_Chat.cyberTopics.ContainsKey(InterestKey))
            {
                var facts = Cyber_Chat.cyberTopics[InterestKey];
                Console.WriteLine($"Here's something interesting about {InterestTopic}:\n{facts[new Random().Next(facts.Count)]}");
            }
            else
            {
                Console.WriteLine($"I couldn't find a specific fun fact about {InterestTopic}, but feel free to ask more about it!");
            }

            Console.WriteLine();
        }
    }
}
