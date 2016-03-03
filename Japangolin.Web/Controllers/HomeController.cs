namespace Wacton.Japangolin.Web.Controllers
{
    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Domain.JapanesePhrases;

    public class HomeController : Controller
    {
        [FromServices]
        public IJapanesePhraseRepository JapanesePhraseRepository { get; set; }

        public IActionResult Index()
        {
            var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
            return this.View(phrase);
        }
    }
}
