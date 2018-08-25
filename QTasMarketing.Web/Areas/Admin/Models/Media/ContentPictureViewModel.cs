using System.ComponentModel.DataAnnotations;

namespace QTasMarketing.Web.Areas.Admin.Models.Media
{
  public   class ContentPictureViewModel:EntityBaseViewModel
    {

        public int ContetnId { get; set; }
        
       
        public int PictureId { get; set; }
        
       
        public int DisplayOrder { get; set; }

      
    }
}
