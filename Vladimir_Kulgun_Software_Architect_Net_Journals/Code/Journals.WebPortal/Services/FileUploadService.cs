using System;
using System.IO;
using System.Net.Mime;
using System.Web;
using Journals.WebPortal.Exceptions;

namespace Journals.WebPortal.Services
{
    public class FileUploadService : IFileUploadService
    {
        public byte[] CreateContent(HttpPostedFileBase journalFile)
        {
            if (journalFile == null)
                throw new ArgumentNullException(nameof(journalFile));

            if (journalFile.ContentType != MediaTypeNames.Application.Pdf)
                throw new FileUploadException("Invalid file format.");

            var stream = journalFile.InputStream;
            var binaryReader = new BinaryReader(journalFile.InputStream);
            var content = binaryReader.ReadBytes((int) stream.Length);

            return  content;
        }
    }
}