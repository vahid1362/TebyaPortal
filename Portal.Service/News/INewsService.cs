using System.Collections.Generic;
using Portal.core.News;

namespace Portal.Service.News
{
    public interface INewsService
    {

        #region Content

        void AddContent(Content content);


        #endregion

        #region Group

         List<Group> GetGroups();

        void AddGroup(Group group);

        Group GetGroupById(long id);

        void EditGroup(Group group);
 

        #endregion

        #region Content

        List<Content> GetContents();

       Content GetContentById(long id);

        void EditContent(Content content);

        #endregion


    }
}
