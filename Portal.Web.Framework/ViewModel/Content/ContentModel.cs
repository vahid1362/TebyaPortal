using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portal.Web.Framework.ViewModel.Content
{
    public  class ContentViewModel:EntityBaseViewModel
    {
        [Required(ErrorMessage = "این  فیلد ضروری است")]
        [DisplayName("عنوان")]
        public string Title { get; set; }

        [DisplayName("لید")]
        public string Lead { get; set; }

        [Required(ErrorMessage = "این  فیلد ضروری است")]
        [DisplayName("متن")]
        public string Body { get; set; }

       
        [Required(ErrorMessage = "این  فیلد ضروری است")]
        [DisplayName("قابل نظر دادن باشد")]
        public bool IsAllowedComment { get; set; }

        [DisplayName("تعداد بازدید")]
        public int Hit { get; set; }

        [DisplayName("محبوبیت")]
        public decimal Rate { get; set; }

        public long GroupId { get; set; }

        public List<SelectListItem> SelectListItems { get; set; }
    }
    }

