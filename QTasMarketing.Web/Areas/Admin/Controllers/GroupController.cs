using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using Portal.core.News;
using Portal.Service;
using Portal.Service.News;
using Portal.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        #region Feild

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        private readonly IToastNotification _toastNotification;
        private readonly IPictureService _pictureService;


        #endregion

        #region Ctor

        public GroupController(IMapper mapper, INewsService newsService, IPictureService pictureService, IToastNotification toastNotification)
        {
            _mapper = mapper;
            _newsService = newsService;
            _pictureService = pictureService;
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
            return View(new GroupViewModel()
            {
                AvaiableGroup = groups

            });
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                var group = _mapper.Map<Group>(groupViewModel);
                _newsService.AddGroup(group);
                if (group.Id > 0)
                {
                    _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");
                    return RedirectToAction("List");
                }
                _toastNotification.AddErrorToastMessage("خطا در انجام عملیات");

            }

            return View(new GroupViewModel());
        }

        public IActionResult Edit(long? groupId)
        {
            if (groupId == null)
            {
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");
                RedirectToAction("List");
            }

            var group = _newsService.GetGroupById(groupId.GetValueOrDefault());

            if (group == null)
                RedirectToAction("List");

            var groupViewModel = _mapper.Map<GroupViewModel>(group);
            var groups = PrepareGroupSelectedListItem();

            groupViewModel.AvaiableGroup = groups;
            return View(groupViewModel);
        }


        [HttpPost]
        public IActionResult Edit(GroupViewModel model)
        {
            if (model == null)
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");

            var group = _newsService.GetGroupById(model.Id);

            if (group == null)
                RedirectToAction("List");

             group = _mapper.Map<Group>(model);

            _newsService.EditGroup(group);
            _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");

            return RedirectToAction("List");
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

        public JsonResult Group_Read([DataSourceRequest] DataSourceRequest request)
        {
            var groupViewModels = _newsService.GetGroups().Select(x => new GroupViewModel
            {
                Id = x.Id,
                BreadCrumbName = x.GetFormattedBreadCrumb(_newsService),
                Title = x.Title,
                IsPrivate = x.IsPrivate,
                Priority = x.Priority
            }).ToList();

            
            return Json(groupViewModels.ToDataSourceResult(request));
        }


        public IActionResult Save(IFormFile file, int? pictureId)
        {
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {

                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                //  _pictureService.InsertPicture()


                //Some browsers send file names with full path.
                // We are only interested in the file name.
                //var fileName = Path.GetFileName(fileContent.FileName.Trim('"'));
                //var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "App_Data", fileName);

                //The files are not actually saved in this demo
            }

            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    //var fileName = Path.GetFileName(fullName);
                    //var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, "App_Data", fileName);

                    // TODO: Verify user permissions

                    //if (System.IO.File.Exists(physicalPath))
                    //{
                    //    // The files are not actually removed in this demo
                    //    // System.IO.File.Delete(physicalPath);
                    //}
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}