namespace Wacton.Japangolin.Web.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNet.Mvc;

    public class ReactController : Controller
    {
        private static readonly IList<CommentModel> _comments;

        static ReactController()
        {
            _comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Author = "Jordan Walke",
                    Text = "This is another comment"
                },
            };
        }

        // since returning a view, need a ReactDemo.html!
        public ActionResult ReactDemo()
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
        public ActionResult AddComment(CommentModel comment)
        {
            _comments.Add(comment);
            return Content("Success :)");
        }
    }
}
