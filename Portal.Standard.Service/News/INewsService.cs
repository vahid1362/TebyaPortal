using System.Collections.Generic;
using Portal.core.News;
using Portal.Standard.Core.News;

namespace Portal.Service.News
{
    public interface INewsService
    {

        #region Content

      


        #endregion

        #region Group

         List<Group> GetGroups();

        void AddGroup(Group group);

        Group GetGroupById(long id);

        void EditGroup(Group group);


        #endregion

        #region Content

        void AddContent(Content content);
        List<Content> GetContents();

        Content GetContentById(long id);

        void EditContent(Content content);

        void AddContentPicture(ContentPicture contentPicture);

        List<ContentPicture> GetContentPictures(long contentId);

        #endregion



    }
}
