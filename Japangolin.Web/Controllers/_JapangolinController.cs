namespace Wacton.Japangolin.Web.Controllers
{
    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Domain.JapanesePhrases;

    /* here for future reference */

    [Route("api/[controller]")]
    public class _JapangolinController : Controller
    {
        [FromServices]
        public IJapanesePhraseRepository JapanesePhraseRepository { get; set; }

        // GET: api/japangolin
        [HttpGet]
        public int Get()
        {
            return this.JapanesePhraseRepository.PhraseCount;
        }

        // GET: api/japangolin/12345
        [HttpGet("{id}")]
        public JapanesePhrase Get(int id)
        {
            return this.JapanesePhraseRepository.GetPhrase(id);
        }

        // GET: api/japangolin/random
        [HttpGet("random")]
        public JapanesePhrase GetRandom()
        {
            return this.JapanesePhraseRepository.GetRandomPhrase();
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
        //    return phrase.Kanji;
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
