namespace Wacton.Japangolin.Web.Controllers
{
    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Domain.JapanesePhrases;
    using Wacton.Japangolin.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly AboutViewModel aboutViewModel = new AboutViewModel();

        [FromServices]
        public IJapanesePhraseRepository JapanesePhraseRepository { get; set; }

        public IActionResult Index()
        {
            var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
            return this.View(phrase);
        }

        public IActionResult About()
        {
            return this.View(this.aboutViewModel);
        }

        public IActionResult Error()
        {
            return View();
        }

        //public IActionResult Next()
        //{
        //    var phrase = this.JapanesePhraseRepository.GetRandomPhrase();
        //    return this.View(phrase);
        //}
    }
}
