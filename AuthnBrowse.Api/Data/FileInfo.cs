using System;

namespace AuthnBrowse.Api.Data
{
    public class FileInformation
    {
        /// <summary>
        /// Used for the Identification of a file
        /// </summary>
        public Guid FileId { get; set; }
        public bool IsDirectory { get; set; }
        /// <summary>
        /// Human readable File Size
        /// </summary>
        public string FileSize { get; set; }

        public string FileName { get; set; }
    }
}