using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WealthMind.Controllers
{
    public class RecommendationController : Controller
    {
        // GET: RecommendationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecommendationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecommendationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecommendationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecommendationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecommendationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecommendationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecommendationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
