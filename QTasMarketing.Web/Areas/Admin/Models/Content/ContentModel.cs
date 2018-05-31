using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QTasMarketing.Web.Areas.Admin.Models.Content
{
    public  class ContentViewModel:EntityBaseViewModel
    {
        public ContentViewModel()
        {
            SelectListItems=new List<SelectListItem>();
            ContentPictureModels=new List<ContentPictureModel>();
            AddContentPictureModel=new ContentPictureModel();
        }
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "این فیلد ضروری می باشد")]
        public string Title { get; set; }

        [DisplayName("لید")]
        public string Lead { get; set; }

        [Required(ErrorMessage = "این فیلد ضروری می باشد")]
        
        public string Body { get; set; }

        [DisplayName("قابل نظر دادن باشد")]
        
        public bool IsAllowedComment { get; set; }

        public bool Hidden { get; set; }

        [DisplayName("میزان بازدید")]
        public int Hit { get; set; }

        [DisplayName("محبوبیت")]
        public decimal Rate { get; set; }

       
        public long GroupId { get; set; }

        public List<SelectListItem> SelectListItems { get; set; }

        public ContentPictureModel AddContentPictureModel { get; set; }

        public List<ContentPictureModel> ContentPictureModels { get; set; }


    }


    public  class ContentPictureModel : EntityBaseViewModel
    {
        public int ContentId { get; set; }

    
        public int PictureId { get; set; }

  
        public string PictureUrl { get; set; }

  
        public int DisplayOrder { get; set; }

  
    }
}

