using ContentProcessing.Article;
using ContentProcessing.Voice;
using ContentProcessing.VVT;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ContentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly ILogger<ContentController> _logger;
        public ContentController(ILogger<ContentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("upload-content")]
        [RequestSizeLimit(long.MaxValue)] // ⚡ No size limit
        [DisableRequestSizeLimit] // ⚡ Completely disable size limits
        public async Task<IActionResult> CreateContent(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No video file received.");
            }
            if (Path.Exists(Path.Combine("Videos", file.FileName)) || Path.Exists(Path.Combine("Vtts", file.FileName)))
            {
                return Ok(new { message = "file already exist" });
            }

            var filePath = file.Name.Contains(".mp4") ? Path.Combine("Videos", file.FileName) : file.Name.Contains(".vtt") ? Path.Combine("Vtts", file.FileName) : null;

            if (filePath == null) return BadRequest("file extension not supported");

            // Ensure the directory exists
            Directory.CreateDirectory("Videos");
            Directory.CreateDirectory("Vtts");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }
       
            return Ok(new { message = "file uploaded successfully" });
        }

        [HttpPost("process-content")]
        public async Task<IActionResult> ProcessContent([FromQuery] string[] fileNames)
        {
            // Get the video file name without extension
            var videoName = new System.IO.FileInfo(fileNames.FirstOrDefault(x=>x.EndsWith(".mp4"))??"")?.Name;
            var vvtName = new System.IO.FileInfo(fileNames.FirstOrDefault(x=>x.EndsWith(".vtt"))??"")?.Name;
            string currentDirectory = Directory.GetCurrentDirectory();

            // Define where to store the video and VVT files temporarily
            var videoPath = Path.Combine(currentDirectory, "Videos", videoName);
            var videoVVTpath = Path.Combine(currentDirectory, "Vtts", vvtName);

            // Check if the video doesn't file exists
            if (!System.IO.File.Exists(videoPath))
            {
                return BadRequest("VideoFile doesn't exist.");
            }

            // Check if the VVT file exists
            if (!System.IO.File.Exists(videoVVTpath))
            {
                return BadRequest("VideoVVTfile doesn't exist.");
            }

            // Process the video in the background
            var processVideo = async () =>
            {
                try
                {
                    var mp3Folder = "Mp3";
                    // Ensure the directory exists for MP3
                    Directory.CreateDirectory(mp3Folder);

                    // Read the VVT content
                    var vvtContent = System.IO.File.ReadAllText(videoVVTpath);

                    // Create an article using the VVT content
                    var article = ArticlesCreator.CreateArticle(videoName, vvtContent);

                    if (article != null)
                    {
                        // Create HttpClient instance and send the POST request
                        using (var client = new HttpClient())
                        {
                            // Set the API endpoint
                            string apiUrl = "https://localhost:7194/Article/";

                            // Serialize the article to JSON
                            var content = new StringContent(
                                System.Text.Json.JsonSerializer.Serialize(article),
                                Encoding.UTF8,
                                "application/json"
                            );

                            // Send the POST request
                            await SendPostRequestAsync(client, apiUrl, content);
                        }
                    }

                    // Create cues from the VVT content
                    var cues = VVTtoCue.CreateCues(vvtContent);

                    // Initialize VideoToMP3
                    var videoToMP3 = new VideoToMP3(videoPath, mp3Folder);

                    // Create the MP3 records
                    videoToMP3.CreateRecords(cues);

                    // Optionally, log success
                    Console.WriteLine("MP3 records created successfully.");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                }
            };

            // Call the async method to process the video
            await processVideo();

            return Ok("Video processed successfully.");
        }

        // Async method to send POST request
        static async Task SendPostRequestAsync(HttpClient client, string apiUrl, StringContent content)
        {
            try
            {
                // Send the POST request to the server
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request was successful!");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions during the HTTP request
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
