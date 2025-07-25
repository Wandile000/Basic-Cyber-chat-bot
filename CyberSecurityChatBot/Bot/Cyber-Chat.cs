using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CyberChatbotSecurity
{
    internal class Cyber_Chat
    {
        // Dictionary to hold all topic-related responses loaded from file
        public static Dictionary<string, List<string>> cyberTopics = new Dictionary<string, List<string>>();

        private string lastTopicKey = null;  // Stores the last topic asked about for context
        private Utils utils;  // Reference to the Utils helper class

        // Setter method to link the external Utils helper classs
        public void SetUtils(Utils u)
        {
            utils = u;
        }

        // Reads a file line-by-line and populates the cyberTopics dictionary
        // Each line in the file should follow the format: key: value
        public void LoadTopics(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    // Split each line into key and value based on the first colon
                    string[] parts = line.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim().ToLower(); // Normalize key
                        string value = parts[1].Trim();

                        // Initialize a list for the key if not already present
                        if (!cyberTopics.ContainsKey(key))
                        {
                            cyberTopics[key] = new List<string>();
                        }

                        // Add the value (response) to the list under the given key
                        cyberTopics[key].Add(value);
                    }
                }
            }
            else
            {
                Console.WriteLine("Cybersecurity topics file not found!");
                Environment.Exit(1);  // Exit the application if file is missing
            }
        }

        // Displays a sorted list of all base topic names (ignoring _suffix entries)
        public void DisplayMenu()
        {
            Console.WriteLine("Available Topics:");
            List<string> shownTopics = new List<string>();

            foreach (var key in cyberTopics.Keys)
            {
                if (!key.Contains("_"))  // Show only non-suffixed topic names
                {
                    string topic = key.ToLower();
                    if (!shownTopics.Contains(topic))
                    {
                        shownTopics.Add(topic);
                    }
                }
            }

            shownTopics.Sort();  // Alphabetical order for user-friendliness
            foreach (var topic in shownTopics)
            {
                Console.WriteLine($"- {topic}");
            }
        }

        // Main logic to parse user input and provide the appropriate response
        public void GetTopicDescription(string input)
        {
            bool topicFound = false;
            string loweredInput = input.ToLower();
            Random unpredictable = new Random();  // Used to randomly select a response from a list

            // Check if the user asked for a simpler explanation
            bool wantsSimplified = loweredInput.Contains("easy") ||
                                   loweredInput.Contains("simpler") ||
                                   loweredInput.Contains("simple") ||
                                   loweredInput.Contains("don't understand") ||
                                   loweredInput.Contains("dont understand") ||
                                   loweredInput.Contains("don't follow") ||
                                   loweredInput.Contains("dont follow") ||
                                   loweredInput.Contains("pardon") ||
                                   loweredInput.Contains("easier") ||
                                   loweredInput.Contains("do not understand");

            // If simplification is requested, fetch _simple variant of last discussed topic
            if (wantsSimplified && lastTopicKey != null)
            {
                string simpleKey = lastTopicKey + "_simple";
                if (cyberTopics.ContainsKey(simpleKey))
                {
                    List<string> responses = cyberTopics[simpleKey];
                    string randomResponse = responses[unpredictable.Next(responses.Count)]; // Randomly choose one simplified answer
                    Console.WriteLine(randomResponse);
                    return;
                }
                else
                {
                    Console.WriteLine("Sorry, a simpler explanation is not available for the last topic.");
                    return;
                }
            }

            // Check for emotion-based input and map to specific suffixes
            string emotionType = null;
            if (loweredInput.Contains("worried") || loweredInput.Contains("anxious") || loweredInput.Contains("concerned"))
                emotionType = "_worried";
            else if (loweredInput.Contains("curious") || loweredInput.Contains("wondering"))
                emotionType = "_curious";
            else if (loweredInput.Contains("frustrated") || loweredInput.Contains("annoyed"))
                emotionType = "_frustrated";

            if (emotionType != null)
            {
                string baseTopic = null;

                // Attempt to extract base topic from the user's message
                foreach (var key in cyberTopics.Keys)
                {
                    if (!key.Contains("_") && loweredInput.Contains(key.ToLower()))
                    {
                        baseTopic = key.ToLower();
                        break;
                    }
                }

                // If not found, use the last topic as fallback
                if (baseTopic == null && lastTopicKey != null)
                {
                    baseTopic = lastTopicKey;
                }

                if (baseTopic != null)
                {
                    string emotionKey = baseTopic + emotionType;

                    if (cyberTopics.ContainsKey(emotionKey))
                    {
                        List<string> responses = cyberTopics[emotionKey];
                        string randomResponse = responses[unpredictable.Next(responses.Count)]; // Randomly select an emotional response
                        Console.WriteLine(randomResponse);

                        lastTopicKey = baseTopic;

                        // Offer follow-up help
                        Console.WriteLine($"\nWould you like more help with {baseTopic}? (yes/no)");

                        string followUp = Console.ReadLine()?.Trim().ToLower();
                        if (followUp == "yes" || followUp == "y")
                        {
                            string helpKey = baseTopic + "_simple";
                            if (cyberTopics.ContainsKey(helpKey))
                            {
                                var helpResponses = cyberTopics[helpKey];
                                Console.WriteLine(helpResponses[unpredictable.Next(helpResponses.Count)]); // Random tip/simplification
                            }
                            else if (cyberTopics.ContainsKey(baseTopic))
                            {
                                var baseResponses = cyberTopics[baseTopic];
                                Console.WriteLine(baseResponses[unpredictable.Next(baseResponses.Count)]);
                            }
                            else
                            {
                                Console.WriteLine("Sorry, I don't have more help available right now.");
                            }
                        }

                        return;
                    }
                }

                Console.WriteLine(" ");
                Console.WriteLine("Sorry, there's no special message available for that feeling about this topic.");
                return;
            }

            // Check if the input is asking for 'tips' about a topic
            if (loweredInput.Contains("tips"))
            {
                foreach (var key in cyberTopics.Keys)
                {
                    if (key.EndsWith("_tips"))
                    {
                        string baseTopic = key.Replace("_tips", "").ToLower();
                        if (loweredInput.Contains(baseTopic))
                        {
                            List<string> tipResponses = cyberTopics[key];
                            string randomTip = tipResponses[unpredictable.Next(tipResponses.Count)]; // Pick one random tip
                            Console.WriteLine(randomTip);

                            lastTopicKey = baseTopic;
                            utils.TrackInteraction(lastTopicKey);
                            return;
                        }
                    }
                }
            }

            // General topic match logic
            foreach (var topic in cyberTopics)
            {
                string loweredTopic = topic.Key.ToLower();

                if (!loweredTopic.Contains("_") && loweredInput.Contains(loweredTopic))
                {
                    string keyToUse = topic.Key;

                    // If user previously requested simplified form
                    if (wantsSimplified)
                    {
                        string simpleKey = keyToUse + "_simple";
                        if (cyberTopics.ContainsKey(simpleKey))
                        {
                            keyToUse = simpleKey;
                        }
                    }

                    List<string> responses = cyberTopics[keyToUse];
                    string randomResponse = responses[unpredictable.Next(responses.Count)]; // Random selection 
                    Console.WriteLine(randomResponse);

                    lastTopicKey = topic.Key.Replace("_simple", "").ToLower();
                    topicFound = true;

                    // Track this interaction for memory/recall 
                    utils.TrackInteraction(lastTopicKey);
                    break;
                }
            }

            if (!topicFound)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Topic not recognized. Please specify a valid cybersecurity topic or type 'Menu' for options.");
            }
        }

        // Starts the chatbot loop, with CI support and rotating prompts
        public void Start()
        {
            bool isCI = Environment.GetEnvironmentVariable("CI") == "true";
            string defaultCIInput = "exit";

            string[] prompts = {
                "Anything specific you're curious about in cybersecurity? (Type 'Menu' for options or 'exit' to quit)",
                "What would you like to learn next? (Type 'Menu' for options or 'exit' to quit)",
                "I'm all ears – pick a cybersecurity topic! (Type 'Menu' for options or 'exit' to quit)",
                "Want to explore more? Just name the topic. (Type 'Menu' for options or 'exit' to quit)",
                "What cybersecurity topic are you thinking about? (Type 'Menu' for options or 'exit' to quit)"
            };

            Console.WriteLine(" ");
            Console.WriteLine(prompts[new Random().Next(prompts.Length)]); // Random opening prompt 

            while (true)
            {
                string input = isCI ? defaultCIInput : Console.ReadLine();

                if (isCI)
                {
                    Console.WriteLine($" CI MODE Auto-selected input: {input}");
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please type a topic or type 'Menu' to see available ones.");
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Exiting. Stay safe online!");
                    break;
                }
                else if (input.Equals("Menu", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(" ");
                    DisplayMenu();
                }
                else
                {
                    Console.WriteLine(" ");
                    GetTopicDescription(input);
                }

                if (isCI) break;
            }
        }
    }
}
