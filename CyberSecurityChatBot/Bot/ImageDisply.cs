using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CyberChatbotSecurity
{
    internal class ImageDisply
    {
        public void Show()
        {
            string imagePath = @"Files\PROGcHATIMAGE.png"; // Use @ for the file path
            string fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), imagePath); // Combines the current directory with the image path to get the full path
            if (File.Exists(fullImagePath)) // Check if file exists
            {
                string asciiArt = ConvertImageToAscii(fullImagePath); //Use full path
                Console.WriteLine(asciiArt);
            }
            else
            {
                Console.WriteLine($"Error: Image file not found at {fullImagePath}"); //  error message
            }
        }

        // This method converts an image to ASCII art and returns it as a string
        static string ConvertImageToAscii(string imagePath)
        {
            // Create a StringBuilder to hold the ASCII result
            StringBuilder sb = new StringBuilder();
            using (Bitmap bitmap = new Bitmap(imagePath)) // Automatically disposes the image after use
            {
                string asciiChars = "@%#*+=-:. "; // Characters from dark to light

                for (int y = 0; y < bitmap.Height; y += 40) // Loop through the image pixels vertically, skipping some rows to reduce size
                {
                    for (int x = 0; x < bitmap.Width; x += 20) // Loop through the image pixels horizontally, skipping some columns to reduce size
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        // Convert the pixel color to grayscale using weighted average formula
                        int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                        // Map grayscale value to ASCII character
                        char asciiChar = asciiChars[grayValue * (asciiChars.Length - 1) / 255];
                        sb.Append(asciiChar); // Add the character to the result
                    }
                    sb.AppendLine();// Move to the next line after finishing one row
                }
            }

            return sb.ToString();
        }
    }
}