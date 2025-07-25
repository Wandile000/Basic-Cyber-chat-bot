using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;


namespace CyberChatbotSecurity
{
    internal class AudioPlayer
    {
        private IWavePlayer? waveOut; // Handles audio playback
        private AudioFileReader? audioFile; // Reads the audio file for playback

        public void PlayAudio(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))// checks if audio file exists
                {
                    Console.WriteLine($"Error: Audio file not found at {filePath}");
                    return;
                }

                waveOut = new WaveOutEvent();// Create a new audio playback device
                audioFile = new AudioFileReader(filePath);// Load the audio file
                waveOut.Init(audioFile);// Initialize the player with the audio file
                waveOut.Play();

                Console.WriteLine("Playing audio...");

                // Wait until the audio finishes
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(500);
                }

                waveOut.Dispose(); // Release resources used by the audio player
                audioFile.Dispose();// Release resources used by the audio file
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while playing audio: " + ex.Message);
            }
        }
    }
}