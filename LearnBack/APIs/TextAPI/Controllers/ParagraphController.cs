using LanguageLib;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParagraphController : ControllerBase
    {
        public ParagraphController()
        {
        }
        #region Get
        [HttpGet]
        public IEnumerable<Paragraph> Get()
        {
            return new List<Paragraph> {};
        }
        #endregion
    }
}
