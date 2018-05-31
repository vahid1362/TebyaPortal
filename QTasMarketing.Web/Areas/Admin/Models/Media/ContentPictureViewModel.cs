using System.ComponentModel.DataAnnotations;

namespace QTasMarketing.Web.Areas.Admin.Models.Media
{
  public   class ContentPictureViewModel:EntityBaseViewModel
    {

        public int ContetnId { get; set; }

        [UIHint("Picture")]
       
        public int PictureId { get; set; }

      
        public string PictureUrl { get; set; }

       
        public int DisplayOrder { get; set; }

      
    }
}
