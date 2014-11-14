namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;

    using Microsoft.AspNet.Identity;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using DigitalLibrary.Data;
using DigitalLibrary.Web.ViewModels.Work;
    using DigitalLibrary.Web.ViewModels.Common;
    using DigitalLibrary.Data.Logic;
    using System.ComponentModel.DataAnnotations;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels;

    public class WorkPublicController : BaseController
    {
        private const int PageSize = 5;
        private const int StartWorkYear = 1800;
        private static int currentYear = DateTime.Now.Year;


        public WorkPublicController(IDigitalLibraryData data)
            : base(data)
        {

        }

        private IQueryable<WorkPublicListViewModel> GetAllWorks()
        {
            var allWorks = this.Data.Works
                .All()
                .Select(WorkPublicListViewModel.FromWork);

            return allWorks;
        }


        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            LoadDataToViewBag();
            return View();
        }

        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var viewModel = this.Data.Works.All().Where(x => x.Id == id)
                .Select(WorkPublicDetailsViewModel.FromWork).FirstOrDefault();

            return View(viewModel);
        }

        public FileResult Download(int id)
        {
            var work = this.Data.Works.GetById(id);

            if (!FileManager.CheckIfFileExists(work.ZipFileLink))
            {
                TempData["error"] = "File not found";
                Response.Redirect("~/WorkPublic/List");
                return null;
            }

            var fileBytes = FileManager.DownloadFile(work.ZipFileLink);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, work.Title + ".zip");
        }

        [Authorize]
        public ActionResult Upload(WorkPublicCreateViewModel createModel, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.Identity.GetUserId();
                var currentUser = this.Data.Users.GetById(currentUserId);
                var genre = this.Data.Genres.All().Where(g => g.GenreName == createModel.Genre).FirstOrDefault();
                var author = this.Data.Authors.All().Where(g => g.Name == createModel.Author).FirstOrDefault();
              

                var workUploadPath = "UploadedFiles\\" + genre.GenreName + "\\" + author.Name + "\\works\\" + createModel.Title + "\\";
                var zipFileLink = workUploadPath + createModel.Title + ".zip";
                var pictureFileLink = workUploadPath + createModel.Title + ".png";

                foreach (var file in files)
	             {
                     UploadFile(file, genre.GenreName, author.Name, createModel.Title); 
	            }
                
                var newWork = new Work
                {
                    AuthorId = author.Id,
                    Description = createModel.Description,
                    GenreId = genre.Id,
                    UploadedById = currentUserId,
                    Year = createModel.Year,
                    Title = createModel.Title,
                    ZipFileLink = zipFileLink,
                    PictureLink = pictureFileLink
                };

                this.Data.Works.Add(newWork);
                this.Data.SaveChanges();

                var viewModel = this.Data.Works.All()
                    .Where(w => w.Id == newWork.Id)
                    .Select(WorkPublicDetailsViewModel.FromWork)
                    .FirstOrDefault();

                TempData["success"] = "Uploaded successfully";
                return View("Details", viewModel);
            }

            return this.View("Create", createModel);
        }

        public JsonResult GetWorks([DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.GetAllWorks().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkData(string text)
        {
            var result = this.Data.Works
                .All()
                .Where(w => w.Author.Name.ToLower().Contains(text.ToLower()) || w.Title.ToLower().Contains(text.ToLower()))
                .Select(s => new
                {
                    AuthorName = s.Author.Name,
                    Title = s.Title
                });

            var matchWords = new HashSet<object>();

            foreach (var item in result)
            {
                matchWords.Add(new
                {
                    MatchResult = item.AuthorName
                });

                matchWords.Add(new
                {
                    MatchResult = item.Title
                });
            }

            return Json(matchWords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkGenreData()
        {
            var genres = this.Data.Genres
                .All()
                .Select(x => new
                {
                    Genre = x.GenreName
                });

            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(SubmitSearchModel submitModel)
        {
            var result = this.Data.Works.All();

            if (!string.IsNullOrEmpty(submitModel.MatchSearch))
            {
                var searchWord = submitModel.MatchSearch.ToLower();
                result = result.Where(x => x.Author.Name.ToLower().Contains(searchWord)
                    || x.Title.ToLower().Contains(searchWord)
                    || x.Description.ToLower().Contains(searchWord));
            }

            if (submitModel.GenreSearch != "All")
            {
                result = result.Where(x => x.Genre.GenreName == submitModel.GenreSearch);
            }

            if (submitModel.YearSearch != 0)
            {
                result = result.Where(x => x.Year == submitModel.YearSearch);
            }

            var endResult = result.Select(WorkPublicListViewModel.FromWork);

            var test = endResult.ToList();

            return this.View(endResult);
        }

        private void UploadFile(HttpPostedFileBase file, string genre, string author, string title)
        {
            var workUploadPath = "UploadedFiles/" + genre + "/" + author + "/works/" + title + "/";
 
            FileManager.CreateFolderIfDoesntExists("UploadedFiles/" + genre);
            FileManager.CreateFolderIfDoesntExists("UploadedFiles/" + genre + "/" + author);
            FileManager.CreateFolderIfDoesntExists("UploadedFiles/" + genre + "/" + author + "/works/");
            FileManager.CreateFolderIfDoesntExists("UploadedFiles/" + genre + "/" + author + "/works/" + title + "/");

            if (Request.Files.Count > 0 && Request.Files.Count < 3)
            {
                if (FileManager.CheckIfFileIsPicture(file))
                {
                    FileManager.UploadFile(file, title.ToLower(), workUploadPath);
                }
                else if (FileManager.CheckIfFileIsZipped(file))
                {
                    FileManager.UploadFile(file, title.ToLower(), workUploadPath);
                }
                else
                {
                    throw new ValidationException("Files are not in correct format");
                }
            }
        }

        private void LoadDataToViewBag()
        {
            IEnumerable<SelectListItem> authors = this.Data.Authors.All().Select(
               a => new SelectListItem { Value = a.Name, Text = a.Name });

            IEnumerable<SelectListItem> genres = this.Data.Genres.All().Select(
              a => new SelectListItem { Value = a.GenreName, Text = a.GenreName });

            this.ViewBag.Year = new SelectList(Enumerable.Range(StartWorkYear, currentYear - StartWorkYear), Enumerable.Range(StartWorkYear, currentYear - StartWorkYear));

            this.ViewBag.Author = authors;

            this.ViewData["Genre"] = genres;
        }
    }
}