using System.ComponentModel.DataAnnotations;

namespace LuckyMeProject.Models
{
	public class Player
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
		public int Credits { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }

	}
}