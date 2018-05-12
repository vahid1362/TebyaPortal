using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QTasMarketing.Web.Framework.ViewModel.Content
{


   public class GroupViewModel:EntityBaseViewModel
    {  
        [Required(ErrorMessage = "وارد کردن عنوان ضروری است")]
        [DisplayName("عنوان")]
        public string Title { get; set; }


        public long? ParentId { get; set; }

        [Required(ErrorMessage = "وارد کردن الویت ضروری است")]
        [DisplayName("اولویت")]
        public int Priority { get; set; }

        [DisplayName("شرح")]
        public string Description { get; set; }

        [DisplayName("آیا خصوصصی باشد")]
        public bool IsPrivate { get; set; }
    }
}
