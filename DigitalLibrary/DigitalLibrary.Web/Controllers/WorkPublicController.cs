namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Data.Logic;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Infrastructure.Populators;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Common;
    using DigitalLibrary.Web.ViewModels.Work;

    using Microsoft.AspNet.Identity;

    using ListViewModel = DigitalLibrary.Web.ViewModels.Work.WorkPublicListViewModel;

    public class WorkPublicController : KendoGridCRUDController
    {
        private const int PageSize = 5;
        private static int currentYear = DateTime.Now.Year;
        private IWorkService workServices;
        private IDropDownListPopulator populator;

        public WorkPublicController(IDigitalLibraryData data, IWorkService workServices, DropDownListPopulator populator)
            : base(data)
        {
            this.workServices = workServices;
            this.populator = populator;
        }

        public ActionResult List()
        {
            return this.View();
        }

        [Authorize]
        public ActionResult Create()
        {
            var addWorkViewModel = new WorkPublicCreateViewModel
            {
                Genres = this.populator.GetAllGenres(),
                Authors = this.populator.GetAllAuthors(),
                Years = this.populator.GetYears()
            };

            return this.View(addWorkViewModel);
        }

        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var viewModel = this.Data.Works.All().Where(x => x.Id == id)
                .Select(WorkPublicDetailsViewModel.FromWork).FirstOrDefault();

            return this.View(viewModel);
        }

        public FileResult Download(int id)
        {
            var work = this.Data.Works.GetById(id);

            if (!FileManager.CheckIfFileExists(work.ZipFileLink))
            {
                this.TempData["error"] = "File not found";
                this.Response.Redirect("~/WorkPublic/List");
                return null;
            }

            var fileBytes = FileManager.DownloadFile(work.ZipFileLink);

            return this.File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, work.Title + ".zip");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(WorkPublicCreateViewModel createModel, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (!FileManager.CheckIfFile(file))
                    {
                        ModelState.AddModelError("File missing", "No selected file");
                    }
                }

                if (ModelState.IsValid && files.Count() == 2)
                {
                    var currentUserId = User.Identity.GetUserId();
                    var currentUser = this.Data.Users.GetById(currentUserId);
                    var genre = this.Data.Genres.All().Where(g => g.Id == createModel.GenreId).FirstOrDefault();
                    var author = this.Data.Authors.All().Where(g => g.Id == createModel.AuthorId).FirstOrDefault();
                    var workUploadPath = "UploadedFiles\\" + genre.GenreName + "\\" + author.Name + "\\works\\" + createModel.Title + "\\";
                    var pictureFileExtension = Path.GetExtension(files.ElementAt(0).FileName);
                    var zipFileExtension = Path.GetExtension(files.ElementAt(1).FileName);
                    var zipFileLink = workUploadPath + createModel.Title + zipFileExtension;
                    var pictureFileLink = workUploadPath + createModel.Title + pictureFileExtension;

                    foreach (var file in files)
                    {
                        this.UploadFile(file, genre.GenreName, author.Name, createModel.Title);
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

                    this.TempData["success"] = "Uploaded successfully";
                    return this.View("Details", viewModel);
                }
            }

            this.TempData["error"] = "Invalid upload ";
            return this.View("Create", createModel);
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

            return this.Json(matchWords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkGenreData()
        {
            var genres = this.Data.Genres
                .All()
                .Select(x => new
                {
                    Genre = x.GenreName
                });

            return this.Json(genres, JsonRequestBehavior.AllowGet);
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

            return this.View(endResult);
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Works.All()
                .Where(w => w.IsApproved)
                .Select(ListViewModel.FromWork);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Works.GetById(id) as T;
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
    }
}