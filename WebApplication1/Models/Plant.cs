using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Plants")]
	public class Plant
    {
		[Required]		
        public Int32 Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal PricePerUnit { get; set; }
		[Required]
		public decimal MaxTemperature { get; set; }
		[Required]
		public decimal MinTemperature { get; set; }
		[Required]
		public decimal Humidity { get; set; }

    }
}
