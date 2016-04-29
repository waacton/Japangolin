namespace Wacton.Japangolin.Web.Controllers
{
    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Japangolin.Web.ViewModels;

    public class InertController : Controller
    {
        private readonly AboutViewModel aboutViewModel = new AboutViewModel();

        [FromServices]
        public IJapanesePhraseRepository JapanesePhraseRepository { get; set; }

        public IActionResult InertJapangolin()
        {
            // note: pretty sure this is obsolete after using JavaScript calls from client-side
            var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
            return this.View(phrase);
        }

        public IActionResult InertAbout()
        {
            return this.View(this.aboutViewModel);
        }

        //public IActionResult Next()
        //{
        //    var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
        //    return this.View(phrase);
        //}
    }
}
