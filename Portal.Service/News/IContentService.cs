using System.Collections.Generic;
using QtasMarketing.Core.News;

namespace QtasMarketing.Services.News
{
    public interface INewsService
    {

        #region Content

        void AddContent(Content content);


        #endregion

        #region Group

         List<Group> GetGroups();

        void AddGroup(Group group);

        Group GetGroup(long id);

        void EditGroup(Group group);

        #endregion

        #region Content

        List<Content> GetContents();

        #endregion
    }
}
