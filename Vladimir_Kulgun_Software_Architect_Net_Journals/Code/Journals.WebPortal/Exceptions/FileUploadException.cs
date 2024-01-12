using System;

namespace Journals.WebPortal.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message)
        {
        }
    }
}