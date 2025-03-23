using DeepL;
using LanguageLib;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TranslateAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private static string translationAPIkey = string.Empty;
        private readonly ILogger<TranslateController> _logger;

        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }

        [HttpGet("text/{text}")]
        public async Task<IActionResult> Translate(string text)
        {
            HttpClient httpClient = new HttpClient();

            // Replace `text` with the actual variable you want to pass
            var response = await httpClient.GetAsync($"https://localhost:7194/Word/by-text/{text}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var wordObject = JsonSerializer.Deserialize<Word>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (wordObject?.Translation != null)
                {
                    return Ok(wordObject?.Translation);
                }
                Console.WriteLine($"Text: {wordObject?.Translation}, Translation: {wordObject?.Translation}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
            var translator = new Translator(translationAPIkey);
            try
            {
                var translatedText = await translator.TranslateTextAsync(
                      text,
                      LanguageCode.German,
                       "en-GB");

                await httpClient.PostAsync($"https://localhost:7194/Word/translate/{text}/{translatedText.Text}", null);

                return Ok(translatedText.Text);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }
        [HttpPost("api-key/{key}")]
        public async Task<IActionResult> RegisterAPIkey(string key)
        {
            await Task.Run(() =>
            {
                translationAPIkey = key;
            });
            return Ok();
        }
    }
}
