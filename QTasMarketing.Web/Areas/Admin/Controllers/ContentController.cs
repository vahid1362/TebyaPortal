using System.Collections.Generic;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using QtasMarketing.Core.News;
using QtasMarketing.Services.News;
using QTasMarketing.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        #region Feild

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        private readonly IToastNotification _toastNotification;


        #endregion

        #region  Ctor

        public ContentController(INewsService newsService, IMapper mapper, IToastNotification toastNotification)
        {
            _newsService = newsService;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }


        #endregion
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View(new ContentViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContentViewModel contentViewModel)
        {

            if (ModelState.IsValid)
            {
                var content = _mapper.Map<ContentViewModel, Content>(contentViewModel);
                _newsService.AddContent(content);
                if (content.Id > 0)
                {
                    _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");
                    return RedirectToAction("List");
                }
                _toastNotification.AddErrorToastMessage("خطا در انجام عملیات");

            }

            return View(new ContentViewModel());
        }



        public IActionResult Edit(long? contentId)
        {
            //if (groupId == null)
            //    _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");

            //var group = _newsService.GetGroup(groupId.GetValueOrDefault());

            //var groupViewModel = _mapper.Map<Group, GroupViewModel>(group);


            return View();
        }

        //[HttpPost]
        //public IActionResult Edit(GroupViewModel model)
        //{
        //    //if (model == null)
        //    //    _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");

        //    //var group = _mapper.Map<GroupViewModel, Group>(model);

        //    //_newsService.EditGroup(group);
        //    //_toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");

        //    return RedirectToAction("List");
        //}


        public JsonResult Content_Read([DataSourceRequest] DataSourceRequest request)
        {
            var contents = _newsService.GetContents();

            var groupsModel = _mapper.Map<List<Content>, List<ContentViewModel>>(contents);

            return Json(groupsModel.ToDataSourceResult(request));
        }

        public JsonResult ComboBox_Read()
        {
            var groups = _newsService.GetGroups();

            var groupsModel = _mapper.Map<List<Group>, List<GroupViewModel>>(groups);

            return Json(groupsModel);
        }

    }
}