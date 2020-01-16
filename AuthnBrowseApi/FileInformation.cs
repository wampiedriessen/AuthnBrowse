namespace AuthnBrowseApi
{
    public class FileInformation
    {
        public FileType FileType { get; set; }

        public int FileSize { get; set; }

        public string FileName { get; set; }

        public string ThumbnailDataB64 { get; set; }
    }

    public enum FileType
    {
        Directory,
        File,
        MediaFile
    }
}