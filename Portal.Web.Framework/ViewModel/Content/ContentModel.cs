using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace Portal.Web.Framework.ViewModel.Content
{
    public  class ContentViewModel:EntityBaseViewModel
    {
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "این فیلد ضروری می باشد")]
        public string Title { get; set; }

        [DisplayName("لید")]
        public string Lead { get; set; }

        [Required(ErrorMessage = "این فیلد ضروری می باشد")]
        public string Body { get; set; }

        [DisplayName("قابل نظر دادن باشد")]
        [AllowHtml]
        public bool IsAllowedComment { get; set; }

        public bool Hidden { get; set; }

        [DisplayName("میزان بازدید")]
        public int Hit { get; set; }

        [DisplayName("محبوبیت")]
        public decimal Rate { get; set; }

       

        public long GroupId { get; set; }

        public List<SelectListItem> SelectListItems { get; set; }
    }
    }

