namespace StoryBooks.Libraries.Email.Services
{
    public interface IEmailRenderer
    {
        /// <summary>
        /// Render email template
        /// </summary>
        /// <typeparam name="TModel">Model data type</typeparam>
        /// <param name="viewName">Full path to the template file on the file system</param>
        /// <param name="model">Model data</param>
        /// <returns>Rendered email template as strings</returns>
        Task<string> RenderAsync<TModel>(string viewName, TModel model);
    }
}
