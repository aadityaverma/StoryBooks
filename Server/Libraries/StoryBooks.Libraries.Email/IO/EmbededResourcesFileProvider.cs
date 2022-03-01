namespace StoryBooks.Libraries.Email.IO
{
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Primitives;

    public class EmbededResourcesFileProvider : IFileProvider
    {
        private readonly IFileProvider physicalFileProvider;
        private readonly AssemblyResourcesDictionary resourceDictionary;

        public EmbededResourcesFileProvider(string rootPath)
        {
            this.physicalFileProvider = new PhysicalFileProvider(rootPath);
            this.resourceDictionary = new (AppDomain.CurrentDomain.GetAssemblies());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return this.physicalFileProvider.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var resource = this.resourceDictionary.Find(subpath).FirstOrDefault();
            if (resource == null)
            {
                throw new FileNotFoundException();
            }

            var fileInfo = new EmbededResourceFileInfo(subpath, resource);
            if (fileInfo.Exists)
            {
                return fileInfo;
            }

            return this.physicalFileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return this.physicalFileProvider.Watch(filter);
        }
    }
}
