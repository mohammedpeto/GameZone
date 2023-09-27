using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Icon { get; set; }
    }
}
