namespace StoryBooks.Libraries.Email.IO
{
    using System.Reflection;

    internal class AssemblyResourcesDictionary
    {
        private readonly HashSet<Assembly> assemblies;
        private readonly Dictionary<string, AssemblyResource> resources;

        public AssemblyResourcesDictionary(params Assembly[] assemblies)
        {
            this.assemblies = new HashSet<Assembly>(assemblies);
            this.resources = new Dictionary<string, AssemblyResource>();

            LoadEmbededResources();
        }

        public AssemblyResource this[string index]
        {
            get => resources[index];
        }

        public int Count => resources.Count;

        public void Add(Assembly assembly)
        {
            var resourcesNames = assembly.GetEmbededResourceNames();
            foreach (var name in resourcesNames)
            {
                if (this.Contains(name))
                {
                    throw new ResourceDuplicationException();
                }

                this.resources.Add(name, new(name, assembly));
            }
        }

        public bool Contains(string resourceName)
        {
            return this.resources.ContainsKey(resourceName);
        }

        public IEnumerable<AssemblyResource> Find(string fileName)
        {
            return this.resources.Where(r => r.Key.Contains(fileName)).Select(r => r.Value);
        }

        private void LoadEmbededResources()
        {
            foreach (var assembly in this.assemblies)
            {
                Add(assembly);
            }
        }
    }

    public record AssemblyResource
    {
        public AssemblyResource(string name, Assembly assembly)
        {
            Name = name;
            Assembly = assembly;
        }

        public string Name { get; }

        public Assembly Assembly { get; }
    }
}
