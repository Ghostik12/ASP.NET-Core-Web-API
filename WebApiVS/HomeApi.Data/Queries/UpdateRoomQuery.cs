using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        public bool NewGasConnected { get; set; }
        public int NewVoltage { get; set; }

        public UpdateRoomQuery(bool newGasConnected = true, int newVoltage = 0)
        {
            NewGasConnected = newGasConnected;
            NewVoltage = newVoltage;
        }
    }
}
