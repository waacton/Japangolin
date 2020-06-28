namespace Wacton.Japangolin.Romaji.Web.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Mvc;

    using Wacton.Japangolin.Romaji.Web.Models;

    public class TutorialController : Controller
    {
        private static readonly IList<TutorialCommentModel> _comments;

        static TutorialController()
        {
            _comments = new List<TutorialCommentModel>
                        {
                            new TutorialCommentModel
                            {
                                Author = "Daniel Lo Nigro",
                                Text = "Hello ReactJS.NET World!"
                            },
                            new TutorialCommentModel
                            {
                                Author = "Pete Hunt",
                                Text = "This is one comment"
                            },
                            new TutorialCommentModel
                            {
                                Author = "Jordan Walke",
                                Text = "This is another comment"
                            },
                        };
        }

        // since returning a view, need a TutorialMain.html!
        public ActionResult TutorialMain()
        {
            return View();
        }

        // returning a non-view object, browser can just try to render the json version of this
        [ResponseCache(NoStore = true)]
        public ActionResult Comments()
        {
            return Json(_comments);
        }

        [HttpPost]
        public ActionResult AddComment(TutorialCommentModel tutorialComment)
        {
            _comments.Add(tutorialComment);
            return Content("Success :)");
        }
    }
}