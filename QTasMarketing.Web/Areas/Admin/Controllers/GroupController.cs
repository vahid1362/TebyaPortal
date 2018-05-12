using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using QtasMarketing.Core.News;
using QtasMarketing.Services.News;
using QTasMarketing.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        #region Feild

        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        private readonly IToastNotification _toastNotification;

        #endregion

        #region Ctor

        public GroupController(IMapper mapper, INewsService newsService, IToastNotification toastNotification)
        {
            _mapper = mapper;
            _newsService = newsService;
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
            return View(new GroupViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupViewModel groupViewModel )
        {

            if (ModelState.IsValid)
            {
                var group = _mapper.Map<GroupViewModel, Group>(groupViewModel);
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
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");

            var group = _newsService.GetGroup(groupId.GetValueOrDefault());

            var groupViewModel = _mapper.Map<Group, GroupViewModel>(group);

        
            return View(groupViewModel);
        }

        [HttpPost]
        public IActionResult Edit(GroupViewModel model)
        {
            if (model == null)
                _toastNotification.AddErrorToastMessage("خطا در پار متر ورودی");
            
            var group = _mapper.Map<GroupViewModel,Group >(model);

            _newsService.EditGroup(group);
            _toastNotification.AddSuccessToastMessage("عملیات  با موفقیت صورت پذیرفت");

            return RedirectToAction("List");
        }


        public JsonResult Group_Read([DataSourceRequest] DataSourceRequest request)
        {
            var groups = _newsService.GetGroups();

            var groupsModel = _mapper.Map<List<Group>, List<GroupViewModel>>(groups);

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