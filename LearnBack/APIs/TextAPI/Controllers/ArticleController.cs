using LanguageLib;
using LanguageServices.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly LanguageContext _repository;
        public ArticleController(LanguageContext repository)
        {
            _repository = repository;
        }
        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> Get()
        {
            return Ok(await _repository.Articles.ToListAsync());
        }
        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<string>>> GetNames()
        {
            return Ok(await _repository.Articles.Select(x=>x.Name).ToListAsync());
        }
        [HttpGet("by-name/{name}")]
        public async Task<Article?> GetByName(string name)
        {
            return await _repository.Articles.Include(a=>a.Paragraphs).ThenInclude(p=>p.Sentences).ThenInclude(s=>s.Words).FirstOrDefaultAsync(x=>x.Name.Equals(name));
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] Article article)
        {
            var avalaible = _repository.Articles.FirstOrDefault(x => x.Name.Equals(article.Name));
            if (avalaible!=null) return Ok();


            for (int i = 0; i < article.Paragraphs.Count; i++)
            {
                for (int j = 0; j < article.Paragraphs.ElementAt(i).Sentences.Count; j++)
                {
                    // Get the available sentence from the database based on the hash
                    var availableSentence = _repository.Sentences
                        .FirstOrDefault(s => s.Hash == article.Paragraphs.ElementAt(i).Sentences.ElementAt(j).Hash);

                    // If the available sentence is found
                    if (availableSentence != null)
                    {
                        // Remove the old sentence from the collection
                        article.Paragraphs.ElementAt(i).Sentences.Remove(article.Paragraphs.ElementAt(i).Sentences.ElementAt(j));

                        // Add the available sentence to the collection
                        article.Paragraphs.ElementAt(i).Sentences.Add(availableSentence);
                        // Decrease the index to re-check the current index after the removal and addition
                        j--;  // Adjust the index to avoid skipping the next item
                    }
                    for (int z = 0; z < article.Paragraphs.ElementAt(i).Sentences.ElementAt(j).Words.Count; z++)
                    {
                        // Get the word from the sentence
                        var word = article.Paragraphs.ElementAt(i).Sentences.ElementAt(j).Words.ElementAt(z);

                        // Retrieve the available word from the database based on the word text
                        var availableWord = _repository.Words
                            .FirstOrDefault(w => w.WordText.Equals(word.WordText));

                        // If the available word is found in the database
                        if (availableWord != null)
                        {
                            // Remove the old word from the collection (if needed)
                            article.Paragraphs.ElementAt(i).Sentences.ElementAt(j).Words.Remove(word);

                            // Add the available word to the collection
                            article.Paragraphs.ElementAt(i).Sentences.ElementAt(j).Words.Add(availableWord);
                            // Adjust the index to avoid skipping words
                            z--; // Decrease the index to re-check the current word after modification
                        }
                    }
                }
            }

            _repository.Articles.Add(article);

            await _repository.SaveChangesAsync();

            return Ok();
        }
        #endregion
        #region Delete
        [HttpDelete("delete-all")]
        public async Task<ActionResult> DeleteAll()
        {
            await _repository.Words.ExecuteDeleteAsync();
            await _repository.Sentences.ExecuteDeleteAsync();
            await _repository.Paragraphs.ExecuteDeleteAsync();
            await _repository.Articles.ExecuteDeleteAsync();
            return Ok();
        }
        #endregion
    }
}
