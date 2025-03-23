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

        [HttpGet("{word}")]
        public IEnumerable<Sentence> Get(string word)
        {
            return _repository.Sentences.Include(a => a.Words).Where(x => x.Words.Where(y => y.WordText.Equals(word)).Count() > 0);
        }
        #endregion
    }
}
