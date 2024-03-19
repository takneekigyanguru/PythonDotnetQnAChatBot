using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
namespace PythonDotnetQnAChatBot.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatBotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ChatBot([FromBody] string userInput)
        {            
            try
            {
                string answer = string.Empty;
                
                // Path to the Python executable
                string pythonExePath = "C:\\Python312\\python";
                // Path to the Python script you want to run
                string scriptPath = "chatbot.py";
                // Arguments to pass to the Python script (if any)
                string scriptArguments = $"\"{userInput}\"";
                string wDirectory = "E:\\Takneekigyanguru\\Code\\dotnet\\asp.net\\botrename\\PythonDotnetQnAChatBot\\PythonDotnetQnAChatBot.Server\\ChatbotFiles";

                // Create process start info
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = pythonExePath,
                    Arguments = $"{scriptPath} {scriptArguments}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = wDirectory
                };

                // Create and start the process
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;

                    // Handle output data received event
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Console.WriteLine($"Output: {e.Data}");
                            answer= e.Data;
                        }
                    };

                    // Handle error data received event
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Console.WriteLine($"Error: {e.Data}");
                            answer= e.Data;
                        }
                    };

                    // Start process
                    process.Start();

                    // Begin asynchronous read operations on the output and error streams
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Wait for the process to exit
                    process.WaitForExit();
                }                
                return Ok(new { Answer = answer });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "An error occurred while processing the request.- " + ex.Message });
            }
        }

    }

  }
