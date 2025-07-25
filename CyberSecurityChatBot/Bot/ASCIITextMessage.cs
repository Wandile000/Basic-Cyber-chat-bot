using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatbotSecurity
{
    internal class ASCIITextMessage
    {
        public void DisplayAscii()
        {
            // ANSI escape code for pink text
            string PinkText = "\u001b[1;35m"; // https://stackoverflow.com/questions/5762491/how-to-print-color-in-console-using-system-out-println
            // Reset code
            string resetText = "\u001b[0m";

            string asciiArt = @"
 __  __        __       __            __                                                    __  __ 
/  |/  |      /  |  _  /  |          /  |                                                  /  |/  |
$$ |$$ |      $$ | / \ $$ |  ______  $$ |  _______   ______   _____  ____    ______        $$ |$$ |
$$ |$$ |      $$ |/$  \$$ | /      \ $$ | /       | /      \ /     \/    \  /      \       $$ |$$ |
$$ |$$ |      $$ /$$$  $$ |/$$$$$$  |$$ |/$$$$$$$/ /$$$$$$  |$$$$$$ $$$$  |/$$$$$$  |      $$ |$$ |
$$/ $$/       $$ $$/$$ $$ |$$    $$ |$$ |$$ |      $$ |  $$ |$$ | $$ | $$ |$$    $$ |      $$/ $$/ 
 __  __       $$$$/  $$$$ |$$$$$$$$/ $$ |$$ \_____ $$ \__$$ |$$ | $$ | $$ |$$$$$$$$/        __  __ 
/  |/  |      $$$/    $$$ |$$       |$$ |$$       |$$    $$/ $$ | $$ | $$ |$$       |      /  |/  |
$$/ $$/       $$/      $$/  $$$$$$$/ $$/  $$$$$$$/  $$$$$$/  $$/  $$/  $$/  $$$$$$$/       $$/ $$/ 
";
            Console.WriteLine(PinkText + asciiArt + resetText);
            
           

        }
        
    }
}
