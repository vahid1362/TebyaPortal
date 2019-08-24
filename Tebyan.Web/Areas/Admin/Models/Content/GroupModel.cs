using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QTasMarketing.Web.Areas.Admin.Models.Content
{


   public class GroupViewModel:EntityBaseViewModel
    {
        public GroupViewModel()
        {
            AvaiableGroup=new List<SelectListItem>();
        }
        [Required(ErrorMessage = "وارد کردن عنوان ضروری است")]
        [DisplayName("عنوان")]
        public string Title { get; set; }


        public string ParentId { get; set; }

        [Required(ErrorMessage = "وارد کردن الویت ضروری است")]
        [DisplayName("اولویت")]
        public int Priority { get; set; }

        [DisplayName("شرح")]
        public string Description { get; set; }

        [DisplayName("آیا خصوص باشد")]
        public bool IsPrivate { get; set; }

        
        public List<SelectListItem> AvaiableGroup { get; set; }

        public int? PictureId { get; set; }

        [DisplayName("عنوان")]
        public string  BreadCrumbName { get; set; }
  

    }
}
