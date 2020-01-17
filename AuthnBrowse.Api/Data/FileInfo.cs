namespace AuthnBrowse.Api.Data
{
    public class FileInformation
    {
        public bool IsDirectory { get; set; }
        /// <summary>
        /// Human readable File Size
        /// </summary>
        public string FileSize { get; set; }

        public string FileName { get; set; }

        public string ThumbnailDataB64 { get; set; }
    }
}