using System.Web.Mvc;

namespace Website
{
    /// <summary>
    /// This class handles the filter config
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// This method registers global filters ie error handling
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
