using System.ComponentModel.DataAnnotations;

namespace StockWebApi.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Minimum length should be 5 characters")]
        [MaxLength(50,ErrorMessage ="Maximum length is 50 characters")]
        public string? Title { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimum length should be 5 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string? Content { get; set; }

    }
}
