using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class GameDevice
    {
        public int GameID { get; set; }
        public int DeviceID { get; set; }

        [ForeignKey("GameID")]
        public Game Game { get; set; }
        [ForeignKey("DeviceID")]
        public Device Device { get; set; }
    }
}