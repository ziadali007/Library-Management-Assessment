using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AddBookDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 13 characters.")]
        public string ISBN { get; set; } = string.Empty;

        public string? Edition { get; set; }

        [Required(ErrorMessage = "Publication year is required.")]
        [Range(1000, 2026, ErrorMessage = "Please enter a valid publication year.")]
        public int PublicationYear { get; set; }

        public string? Summary { get; set; }

        [Url(ErrorMessage = "Please enter a valid URL for the cover image.")]
        public string? CoverImageUrl { get; set; }

        [Required(ErrorMessage = "Publisher ID is required.")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Language ID is required.")]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "At least one author ID must be provided.")]
        public ICollection<int> AuthorIds { get; set; } = new HashSet<int>();

        [Required(ErrorMessage = "At least one category ID must be provided.")]
        public ICollection<int> CategoryIds { get; set; } = new HashSet<int>();
    }
}
