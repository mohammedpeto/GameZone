using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }


        public ICollection<Game> GameDevices { get; set; } = new List<Game>();

    }
}