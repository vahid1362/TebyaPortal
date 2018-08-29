using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QTasMarketing.Web.Areas.Admin.Models.Media
{
  public   class ContentPictureViewModel:EntityBaseViewModel
    {

        public int ContentId { get; set; }

        public long PictureId { get; set; }


        [DisplayName("آدرس تصاویر")]
        public string PictureUrl { get; set; }

        [DisplayName("اولویت")]
        public int DisplayOrder { get; set; }

        public byte[] Image { get; set; }


    }
}
