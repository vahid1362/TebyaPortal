using QtasMarketing.Core;

namespace Portal.core.News
{
    public class Group : BaseEntity
    {
        public string Title { get; set; }

        public int Priority { get; set; }

        public long? ParentId { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }
        
        public bool DisplayInMain { get; set; }

        public bool PictureID { get; set; }

       


    }
}
