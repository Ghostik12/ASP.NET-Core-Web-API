using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Rooms
{
    public class EditRoomRequest
    {
        public string Name {  get; set; }
        public bool NewGasConnected { get; set; }
        public int NewVoltage { get; set; }
    }
}
