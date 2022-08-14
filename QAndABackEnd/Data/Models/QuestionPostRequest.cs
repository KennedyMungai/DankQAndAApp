using System.ComponentModel.DataAnnotations;

namespace QAndABackEnd.Data.Models;

public class QuestionPostRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public DateTime Created { get; set; }
}