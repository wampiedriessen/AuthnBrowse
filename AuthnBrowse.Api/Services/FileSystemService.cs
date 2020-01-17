using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuthnBrowse.Api.Data;
using Microsoft.Extensions.Configuration;

namespace AuthnBrowse.Api.Services
{
    public interface IFileSystemService
    {
        IEnumerable<FileInformation> GetFiles(string relativePath);
    }

    public class FileSystemService : IFileSystemService
    {
        private readonly IConfiguration _configuration;

        public FileSystemService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IEnumerable<FileInformation> GetFiles(string relativePath)
        {
            string root = _configuration["ExposingDataDir"];
            string folderPath = root;
            if (!string.IsNullOrEmpty(relativePath))
                folderPath = Path.Combine(root, relativePath);

            var files = Directory.GetFiles(folderPath);

            return files.Select(f =>
            {
                var info = new FileInfo(f);
                return new FileInformation()
                {
                    IsDirectory = IsDir(info),
                    FileName = info.Name,
                    FileSize = FileSizeH(info.Length),
                };
            });
        }

        private static string FileSizeH(long infoLength)
        {
            foreach (var unit in FileUnits())
            {
                if (infoLength < 1024)
                    return $"{infoLength:F1} {unit}";

                infoLength /= 1024;
            }

            return "Too big";
        }

        private static IEnumerable<string> FileUnits()
        {
            yield return "B";
            yield return "KB";
            yield return "MB";
        }

        private static bool IsDir(in FileInfo info)
        {
            return (info.Attributes & FileAttributes.Directory) != 0;
        }
    }
}