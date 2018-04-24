using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KateRecipesV2.DAL;
using KateRecipesV2.Models;

namespace KateRecipesV2.Controllers
{
    public class RecipesController : Controller
    {
        private RecipesDBEntities db = new RecipesDBEntities();
        private RecipeRepository repository = new RecipeRepository();

        // GET: Recipes
        public ActionResult Index()
        {
            return View(repository.Recipes);     
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe context = repository.Recipes.FirstOrDefault(p => p.Id == id);
            if (context == null)
            {
                return HttpNotFound();
            }
            return View(context);
        }
        public ViewResult Edit(int id)
        {
            Recipe product = repository.Recipes.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        
        public ViewResult Create()
        {
            return View("Edit", new Recipe());
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    recipe.ImageMimeType = image.ContentType;
                    recipe.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(recipe.ImageData, 0, image.ContentLength);
                }
                repository.SaveRecipe(recipe);
                TempData["message"] = string.Format("Zapisano {0} ", recipe.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // błąd w wartościach danych
                return View(recipe);
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Recipe deletedProduct = repository.DeleteRecipe(Id);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int Id)
        {
            Recipe recipe = repository.Recipes.FirstOrDefault(p => p.Id == Id);
            if (recipe != null)
            {
                return File(recipe.ImageData, recipe.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
