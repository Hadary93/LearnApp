using LanguageLib;
using LanguageServices.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentenceController : ControllerBase
    {
        private readonly LanguageContext _repository;
        public SentenceController(LanguageContext repository)
        {
            _repository = repository;
        }
        #region Get
        [HttpGet]
        public IEnumerable<Sentence> Get()
        {
            return _repository.Sentences.Include(a => a.Words);
        }

        [HttpGet("random-sentence")]
        public async Task<IActionResult> GetRandomSentence()
        {
            int count = await _repository.Sentences.CountAsync();
            if (count == 0) return NotFound("No sentences available.");

            int randomIndex = new Random().Next(0, count); // Get a random index between 0 and count-1

            var sentence = await _repository.Sentences
                .Include(s => s.Words)
                .Skip(randomIndex)
                .FirstOrDefaultAsync();

            return Ok(sentence);
        }
        [HttpGet("{word}")]
        public IEnumerable<Sentence> Get(string word)
        {
            return _repository.Sentences.Include(a => a.Words).Where(x => x.Words.Where(y => y.WordText.Equals(word)).Count() > 0);
        }
        #endregion
    }
}
