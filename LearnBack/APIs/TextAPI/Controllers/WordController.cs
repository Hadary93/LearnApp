using LanguageLib;
using LanguageServices.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Learn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController: ControllerBase
    {
        private readonly LanguageContext _repository;
        public WordController(LanguageContext repository)
        {
            _repository = repository;   
        }
        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> Get()
        {
            var result = await _repository.Words.ToListAsync();
            return Ok(result);
        }
        [HttpGet("by-text/{word}")]
        public async Task<ActionResult<Word>> GetWord(string word)
        {
            var wordObject = await _repository.Words.FirstOrDefaultAsync(x => x.WordText.Equals(word));
            if (wordObject!=null)
            {
                return wordObject;
            }
            return BadRequest();  
        }
        [HttpGet("get-favourites/{start}/{count}")]
        public async Task<IActionResult> GetFavourizedWords(int start, int count)
        {
            return Ok(await _repository.Words.Where(x => x.Favourite)
            .GroupBy(x => x.WordText)
            .Select(g => g.First())
            .Skip(start)
            .Take(count).ToListAsync());
        }
        [HttpGet("by-id/{id}")]
        public Word GetNextWord(int id)
        {
            return _repository.Words.FirstOrDefault(x => x.Id == id) ?? _repository.Words?.FirstOrDefault() ?? new Word();
        }
        [HttpGet("by-group-by-id/{group}/{id}")]
        public Word GetNextWord(string group, int id)
        {
            return _repository.Words.FirstOrDefault(x => x.Id >= id && x.Group.Equals(group)) ?? _repository.Words.FirstOrDefault() ?? new Word();
        }
        [HttpGet("GetGroups")]
        public List<string> GetGroups()
        {
            return _repository.Words.GroupBy(x => x.Group).Select(x => x.Key).ToList() ?? new List<string>();
        }
        
        #endregion
        #region Post
        [HttpPost]
        public bool Add([FromBody] Word word)
        {
            var avalaibleWord = _repository.Words.FirstOrDefault(x => x.WordText.Equals(word.WordText));
            if (avalaibleWord!=null)
            {
                avalaibleWord.Translation = word.Translation;
            }
            else
            {
                _repository.Words.Add(word);
            }
            _repository.SaveChanges();
            return true;
        }
        [HttpPost("favourite/{word}")]
        public bool FavouriteWord(string word)
        {
            var avalaibleWord = _repository.Words.Where(x => x.WordText.Equals(word)).ForEachAsync(x => x.Favourite = true);
            _repository.SaveChanges();
            return true;
        }
        [HttpPost("translate/{word}/{translation}")]
        public bool TranslateWord(string word,string translation)
        {
            var avalaibleWord = _repository.Words.Where(x => x.WordText.Equals(word)).ForEachAsync(x => x.Translation = translation);
            _repository.SaveChanges();
            return true;
        }
        [HttpPost("AddWords")]
        public bool AddWords([FromBody] List<Word> words)
        {
            return true;
        }
        #endregion
        #region Put
        [HttpPut]
        public bool Update([FromBody] Word updatedWord)
        {
            var word = _repository.Words.FirstOrDefault(x => x.Id == updatedWord. Id);

            if (word == null) return false;

            word.Difficulty = updatedWord.Difficulty;
            word.Favourite = updatedWord.Favourite;
            word.WordText = updatedWord.WordText;
            word.Translation = updatedWord.Translation;

            var result = _repository.SaveChanges();

           return result>0?true:false;
        }
        #endregion
    }
}
