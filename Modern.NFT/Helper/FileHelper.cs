using System.IO;
using System.Collections.Generic;

namespace Modern.NFT.Helper
{
    public class FileHelper
    {
        public static List<string> ReadFiles(string filePath)
        {
            List<string> fileContents = new List<string>();
            var files =  Directory.GetFiles(filePath);

            foreach (var file in files)
            {
                fileContents.Add(File.ReadAllText(file));
            }

            return fileContents;
        }
    }
}
