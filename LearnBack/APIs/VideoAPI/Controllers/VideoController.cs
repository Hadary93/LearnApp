using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using System.Text.Json.Nodes;
using DeepL;
using Microsoft.AspNetCore.Razor.Hosting;

namespace VideoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {

        [HttpGet("{videoName}")]
        public IActionResult StreamVideo(string videoName)
        {
            var filePath = Path.Combine("C:\\Hadary\\Personal\\Learn\\ContentAPI\\Videos\\", videoName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "video/mp4", enableRangeProcessing: true); // ✅ Enables pausing & resuming
        }
        [HttpGet("subtitle/{videoName}")]
        public IActionResult GetSubtitle(string videoName)
        {
            var filePath = Path.Combine($"C:\\Hadary\\Personal\\Learn\\ContentAPI\\Vtts\\{videoName}.vtt", "");
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "subtitle/vtt", enableRangeProcessing: true); // ✅ Enables pausing & resuming
        }
        [HttpGet("videos")]
        public IActionResult GetVideos()
        {
            var files = Directory.EnumerateFiles("C:\\Hadary\\Personal\\Learn\\ContentAPI\\Videos\\");
            return Ok(files.Select(x => new FileInfo(x).Name.Split(".")[0]));
        }
    }
}
