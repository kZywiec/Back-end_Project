using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Core.Entities
{
	public class Document: EntityBase
	{

        public Document(string title, string description, string author, string documentType, string path, DateTime creationDate) : base()
        {
            Title = title;
            Description = description;
            Author = author;
            DocumentType = documentType; //Can be used FileTypeHelper.GetFileType(FilePath)
            Path = path; //ToDo string GeneratePath()
            CreationDate = creationDate;
        }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "The Author field is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The DocumentType field is required.")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "The Path field is required.")]
        [StringLength(255, ErrorMessage = "The Path field must be no longer than 255 characters.")]
        
        //The provided regular expression allows URLs starting with http://, https://, or ftp://.
        //[RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "Invalid Path format.")]
        
        //Validate a local file path using a regular expression
        [RegularExpression(@"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+\.(txt|docx|pdf)$", ErrorMessage = "Invalid Path format.")]

        public string Path { get; set; }

    }
}
