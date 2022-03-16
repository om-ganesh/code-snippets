using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace spaudyal.core
{
    public static class FileHelper
    {
        public static string FindFilesInParentDirectory(string fileName, DirectoryInfo dirInfo = null)
        {
            //if directory info is not provided, take the assembly path
            if (dirInfo == null)
            {
                dirInfo = new DirectoryInfo(GetExecutingAssemblyPath());
            }

            var filePath = Path.Combine(dirInfo.FullName, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }

            // Go one level up to search for
            DirectoryInfo parentDirInfo = dirInfo.Parent;
            if (parentDirInfo != null)
            {
                return FindFilesInParentDirectory(fileName, parentDirInfo);
            }

            // if we reach root directory, admit it is a failure
            return null;
        }

        private static string GetExecutingAssemblyPath()
        {
            UriBuilder repoBaseUri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(repoBaseUri.Path));
        }
    }
}
