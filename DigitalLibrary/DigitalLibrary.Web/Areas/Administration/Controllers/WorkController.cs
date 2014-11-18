namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using DigitalLibrary.Data;
    using DigitalLibrary.Data.Logic;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Kendo.Mvc.UI;

    using EditModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Works.WorkEditModel;
    using Model = DigitalLibrary.Models.Work;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Works.WorkViewModel;
    
    public class WorkController : KendoGridCRUDController
    {
        public WorkController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var genre = this.Data.Genres
                .All()
                .Where(g => g.GenreName == model.Genre)
                .FirstOrDefault();

            var author = this.Data.Authors
                .All()
                .Where(a => a.Name == model.Author)
                .FirstOrDefault();

            var updateModel = new EditModel();
            Mapper.Map<ViewModel, EditModel>(model, updateModel);

            updateModel.AuthorId = author.Id;
            updateModel.GenreId = genre.Id;

            base.Update<Model, EditModel>(updateModel, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var folders = model.ZipFileLink.Split('\\').ToList();

            folders.RemoveAt(folders.Count - 1);

            base.Destroy<Model>(model.Id);

            var filePath = string.Join("/", folders);
            if (folders.Count > 1)
            {
                FileManager.DeleteFile(filePath);
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Works.All().Select(ViewModel.FromWork);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Works.GetById(id) as T;
        }
    }
}