using System.ComponentModel.DataAnnotations;

namespace Social_Media.Models
{
    public class PostCategory
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }

	}
}
