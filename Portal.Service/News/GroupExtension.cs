using System;
using System.Collections.Generic;
using System.Text;
using Portal.core.News;

namespace Portal.Service.News
{
    public static class GroupExtension
    {

        // <summary>
        /// Get formatted category breadcrumb 
        /// Note: ACL and store mapping is ignored
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="newsService">Category service</param>
        /// <param name="separator">Separator</param>
        /// <returns>Formatted breadcrumb</returns>
        public static string GetFormattedBreadCrumb(this Group category,
            INewsService newsService,
            string separator = ">>")
        {
            var result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, newsService);
            for (var i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Title;
                result = string.IsNullOrEmpty(result)
                    ? categoryName
                    : $"{result} {separator} {categoryName}";
            }

            return result;
        }

        /// <summary>
        /// Get category breadcrumb 
        /// </summary>

        /// <returns>Category breadcrumb </returns>
        public static IList<Group> GetCategoryBreadCrumb(this Group group,
            INewsService newsService
        )
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            var result = new List<Group>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<long>();

            while (group != null && !alreadyProcessedCategoryIds.Contains(group.Id))

            {
                result.Add(group);

                alreadyProcessedCategoryIds.Add(group.Id);

                group = newsService.GetGroupById(group.ParentId.GetValueOrDefault());
            }
            result.Reverse();
            return result;
        }


    }


}
