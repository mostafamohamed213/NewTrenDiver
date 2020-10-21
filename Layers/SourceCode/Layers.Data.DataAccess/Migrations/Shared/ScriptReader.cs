using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Data.DataAccess.Migrations.Shared
{
    internal static class ScriptReader
    {
        internal static string Read(MigrationContext context, string fileName)
        {
            // Find current assemplyLocation
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;

            // Map to URI
            UriBuilder uri = new UriBuilder(codeBase);

            // Find physical path
            string path = Uri.UnescapeDataString(uri.Path);

            // Extract script file path
            var filePath = Path.GetDirectoryName(path).Replace("bin\\Debug","") + $"\\Migrations\\{context.ToString()}\\Scripts\\{fileName}.sql";

            // Read Content
            string content = File.ReadAllText(filePath);

            // Return Content
            return content;
        }
    }

    internal enum MigrationContext
    {
        Write,
        Read
    }
}
