using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(500)]
        public string Cover { get; set; }


        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public ICollection<GameDevice> GameDevices { get; set; } = new List<GameDevice>();

    }
}
