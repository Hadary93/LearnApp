using Microsoft.AspNetCore.Mvc;

namespace VoiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<VoiceController> _logger;

        public VoiceController(ILogger<VoiceController> logger)
        {
            _logger = logger;
        }
        [HttpGet("{voiceId}")]
        public IActionResult StreamVoice(string voiceId)
        {
            var filePath = Path.Combine("C:\\Hadary\\Personal\\Learn\\VoiceAPI\\Voices\\", $"{voiceId}.mp3");

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "audio/mpeg", enableRangeProcessing: true); // ✅ Correct MIME type
        }

    }
}
