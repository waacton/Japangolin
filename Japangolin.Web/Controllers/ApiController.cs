namespace Wacton.Japangolin.Web.Controllers
{
    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Domain.JapanesePhrases;

    [Route("api")]
    public class ApiController : Controller
    {
        [FromServices]
        public IJapanesePhraseRepository JapanesePhraseRepository { get; set; }

        // GET: api
        [HttpGet]
        public int Get()
        {
            return this.JapanesePhraseRepository.PhraseCount;
        }

        // GET: api/12345
        [HttpGet("{id}")]
        public JapanesePhrase Get(int id)
        {
            return this.JapanesePhraseRepository.GetPhrase(id);
        }

        // GET: api/random
        [ResponseCache(NoStore = true)]
        [HttpGet("random")]
        public JapanesePhrase GetRandom()
        {
            return this.JapanesePhraseRepository.GetRandomPhrase();
        }
    }
}