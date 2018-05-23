using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Portal.core.News;
using Portal.Service.News;
using Portal.Web.Framework.ViewModel.Content;

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
            var groups = PrepareGroupSelectedListItem();
            return View(new ContentViewModel()
            {
                SelectListItems = groups
            });
        }


        private List<SelectListItem> PrepareGroupSelectedListItem()
        {
            var groups = _newsService.GetGroups().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.GetFormattedBreadCrumb(_newsService)
            }).ToList();
            return groups;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContentViewModel contentViewModel)
        {

            if (ModelState.IsValid)
            {
               
               var content = _mapper.Map<Content>(contentViewModel);
              
                _newsService.AddContent(content);
                if (content.Id > 0)
                {
                    _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");
                    return RedirectToAction("List");
                }
                _toastNotification.AddErrorToastMessage("خطا در انجام عملیات");

            }

            return RedirectToAction("List");
        }



        public IActionResult Edit(long? contentId)
        {
            if (contentId == null)
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");

            var content = _newsService.GetContents();

            var contentViewModel = _mapper.Map<ContentViewModel>(content);


            return View(contentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContentViewModel model)
        {
            if (model == null)
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");
            
            var content = _newsService.GetContentById(model.Id);
            if (content == null)
                RedirectToAction("List");
            content = _mapper.Map< Content>(model);
            
            _newsService.EditContent(content);
            _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");

            return RedirectToAction("List");
        }


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