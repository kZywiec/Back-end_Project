using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Entities.LogEntities;
using Core.Entities.UserEntities;

namespace Core.Entities.DocumentEntities
{
    public class Document : EntityBase
    {
        public Document(string title, string fileType, string? description, long uploaderId, DocumentAccessStatus accessStatus): base()
        {
            Title = title;
            FileType = fileType;
            Description = description;
            UploaderId = uploaderId;
            AccessStatus = accessStatus;
            FilePath = @"C:\Users\Username\Documents\" + Id + title + "." + fileType;
        }

        /// <summary>
        /// Tytuł dokumentu.
        /// </summary>
        //[Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        /// <summary>
        /// Typ pliku dokumentu.
        /// </summary>
        //[Required(ErrorMessage = "The File Type field is required.")]
        public string FileType { get; set; }

        /// <summary>
        /// Opis dokumentu.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Użytkownik, który dodał dokument do systemu.
        /// </summary>
        //[HiddenInput]
        public User Uploader { get; set; }
        
        //[HiddenInput]
        public long UploaderId { get; set; }

        /// <summary>
        /// Status udostępniania dokumentu (Public, Private, Confidential).
        /// </summary>
        //[Required]
        public DocumentAccessStatus AccessStatus { get; set; }

        /// <summary>
        /// Ścieżka do pliku dokumentu.
        /// </summary>
        //[Required]
        //[HiddenInput]
        //[StringLength(255, ErrorMessage = "The Path field must be no longer than 255 characters.")]
        // Online validation
        // The provided regular expression allows URLs starting with http://, https://, or ftp://.
        // [RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "Invalid Path format.")]

        // Local validation
        // Validate a local file path using a regular expression
        //[RegularExpression(@"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+\.(txt|docx|pdf)$", ErrorMessage = "Invalid Path format.")]
        public string FilePath { get; set; }

        public List<Log> Logs { get; set; } = new List<Log>();

        public void DownloadFile()
        {
            throw new NotImplementedException();
        }
    }
}
