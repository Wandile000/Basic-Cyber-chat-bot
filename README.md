# Cybersecurity Chatbot

Welcome to the **Cybersecurity Chatbot**, a C# console-based application designed to educate users on key cybersecurity concepts in a fun, interactive, and friendly way.

This chatbot acts like your personal cyber safety guide. When launched, it greets the user, plays a welcome audio (optional), and explains various cybersecurity topics in both technical and simplified formats.

---

##  Key Topics Covered

- Complex Credentials  
- Phishing Awareness  
- Multi-Factor Authentication (MFA)  
- Patch Management  
- Public Wi-Fi Risks  
- Encryption  
- Social Engineering  
- Backups  
- Digital Footprint  
- Cybersecurity Literacy  
- ...and more!

Users can type questions or topic names (e.g., “Tell me about phishing”), and the bot responds with relevant, randomized tips and explanations.

---

## ✅ Features

-  **Loads dynamic topics from a text file**
-  **Supports friendly user input** (e.g., "What is MFA?")
-  **ASCII art and startup message display**
-  **Optional welcome audio playback**
-  **CI Mode** for automated testing environments
-  **Sentiment detection** (e.g., worried, curious, frustrated)
-  **Interest-based memory** – Remembers what you're interested in and shares fun facts after continued chat
-  **Topic-specific tips** – Responds with helpful advice if you ask for "tips"
-  **Conversation tracking** – Tracks your topic interactions for a better experience

---

## 🛠 How to Run

1. **Clone the repository**
2. Ensure the following files are in the `/Files/` folder:
    - `Cyber_Security-Info.txt` — contains topic keys + responses
    - `Cyber Voice.m4a` — *(optional)* welcome audio file
3. Open the project in **Visual Studio**
4. Build and run the application

---

## 📂 File Structure


/Files
├── Cyber_Security-Info.txt ← Cybersecurity topics and responses
└── Cyber Voice.m4a ← (optional) welcome voice message

/ImageDisply.cs ← Displays ASCII image
/Greetings.cs ← Handles greetings + mood
/FeelingValidator.cs ← Validates and reacts to user feelings
/AudioPlayer.cs ← Plays/Stops audio
/ASCIITextMessage.cs ← Displays ASCII banner
/Cyber-Chat.cs ← Main chatbot interaction logic
/Utils.cs ← Tracks user interest and shows tips/facts
/Program.cs ← Entry point

