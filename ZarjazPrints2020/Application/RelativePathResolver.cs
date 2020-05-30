using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ZarjazPrints2020.Application
{
    /// <summary>
    /// Resolve path relative to application root 
    /// remove /bin/{debug|release} if present to make it easier to reference project-relative resources
    /// </summary>
    public class RelativePathResolver
    {

        public string Resolve(string pathToResolve) 
        {
            if (Path.IsPathFullyQualified(pathToResolve)) return pathToResolve;

            var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //string comparison here is a bit hacky is there a better approach?
            if (assemblyPath.EndsWith("bin\\debug\\netcoreapp3.1", StringComparison.CurrentCultureIgnoreCase)
                || assemblyPath.EndsWith("bin\\release\\netcoreapp3.1", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                assemblyPath = Path.Combine(assemblyPath, "..", "..", "..");
            }
            var resolved = Path.GetFullPath(Path.Combine(assemblyPath, pathToResolve));
            return resolved;
        }
    }
}
