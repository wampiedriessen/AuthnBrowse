using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuthnBrowse.Api.Data;
using Microsoft.Extensions.Options;

namespace AuthnBrowse.Api.Services
{
    public interface IFileSystemService
    {
        IEnumerable<FileInformation> GetRootFiles();
        IEnumerable<FileInformation> GetDirectory(Guid fileId);
        Stream GetDownload(Guid fileId);
    }

    public class FileSystemService : IFileSystemService
    {
        private readonly IOptions<FileSystemOptions> _config;
        private readonly Dictionary<Guid, (InternalFileInfo Info, HashSet<Guid> Children)> _fileSystem = 
            new Dictionary<Guid, (InternalFileInfo, HashSet<Guid>)>();
        private Guid? _rootGuid;

        public FileSystemService(IOptions<FileSystemOptions> config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            
            if(string.IsNullOrEmpty(config.Value.ExposingDataDir))
                throw new InvalidOperationException($"Could not start service without {nameof(config.Value.ExposingDataDir)} setting");
        }

        public IEnumerable<FileInformation> GetRootFiles()
        {
            if (_rootGuid.HasValue)
                return GetDirectory(_rootGuid.Value);

            _rootGuid = Guid.NewGuid();
            var rootGuid = _rootGuid.Value;

            string root = _config.Value.ExposingDataDir;
            var rootFiles = GetDirectoryInternal(root, rootGuid);

            _fileSystem[rootGuid] = (new InternalFileInfo()
            {
                FileId = rootGuid,
                FileName = "root",
                FilePath = root,
                FileSize = DirSizeH(rootFiles.Count),
                IsDirectory = true
            }, rootFiles.Select(x => x.FileId).ToHashSet());
            
            return rootFiles.Select(MapToOuter);
        }

        public IEnumerable<FileInformation> GetDirectory(Guid fileId)
        {
            if (!_rootGuid.HasValue || !_fileSystem.ContainsKey(fileId))
                throw new FileNotFoundException();

            var file = _fileSystem[fileId];

            if (!file.Info.IsDirectory)
                throw new FileNotADirectoryException();

            if (file.Children != null)
                return file.Children.Select(c => MapToOuter(_fileSystem[c].Info));
            
            return HydrateChildren(file.Info);
        }

        private IEnumerable<FileInformation> HydrateChildren(in InternalFileInfo info)
        {
            var newChildren = GetDirectoryInternal(info.FilePath, info.FileId);

            var fileEntry = _fileSystem[info.FileId];
            fileEntry.Children = newChildren.Select(c => c.FileId).ToHashSet();
            _fileSystem[info.FileId] = fileEntry;
            
            return newChildren.Select(MapToOuter);
        }

        private List<InternalFileInfo> GetDirectoryInternal(in string infoFilePath, in Guid parentId)
        {
            var files = Directory.GetFiles(infoFilePath);
            var dirs = Directory.GetDirectories(infoFilePath);

            var children = new List<InternalFileInfo>();

            foreach (var file in files)
            {
                var info = new FileInfo(file);
                var fileId = Guid.NewGuid();
                children.Add(new InternalFileInfo()
                {
                    FileId = fileId,
                    FileName = info.Name,
                    FileSize = FileSizeH(info.Length),
                    FilePath = file,
                    IsDirectory = false,
                });
            }

            foreach (var dir in dirs)
            {
                var info = new DirectoryInfo(dir);
                var fileId = Guid.NewGuid();
                children.Add(new InternalFileInfo()
                {
                    FileId = fileId,
                    FileName = info.Name,
                    FileSize = DirSizeH(info.EnumerateFiles().Count()),
                    FilePath = dir,
                    IsDirectory = true,
                });
            }

            foreach (var child in children)
            {
                _fileSystem.Add(child.FileId, (child, null));
            }

            return children;
        }

        public Stream GetDownload(Guid fileId)
        {
            if (!_rootGuid.HasValue || !_fileSystem.ContainsKey(fileId))
                throw new FileNotFoundException();

            var file = _fileSystem[fileId];

            if (file.Info.IsDirectory)
                throw new FileNotARegularFileException();

            return File.OpenRead(file.Info.FilePath);
        }

        private static FileInformation MapToOuter(InternalFileInfo fi)
        {
            return new FileInformation()
            {
                FileId = fi.FileId,
                IsDirectory = fi.IsDirectory,
                FileSize = fi.FileSize,
                FileName = fi.FileName,
            };
        }
        
        private static string DirSizeH(in int count)
        {
            return $"{count} items";
        }

        private static string FileSizeH(in long infoLength)
        {
            float size = infoLength;
            foreach (var unit in FileUnits())
            {
                if (size < 1024f)
                    return $"{size:F1} {unit}";

                size /= 1024f;
            }

            return "Too big";
        }

        private static IEnumerable<string> FileUnits()
        {
            yield return "B";
            yield return "KB";
            yield return "MB";
        }

        private class InternalFileInfo
        {
            public Guid FileId { get; set; }
            public string FilePath { get; set; }
            public string FileName { get; set; }
            public string FileSize { get; set; }
            public bool IsDirectory { get; set; }
        }
    }

    public class FileNotFoundException : Exception {}
    public class FileNotADirectoryException : Exception {}
    public class FileNotARegularFileException : Exception {}
}