using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities {
    public class UserInformation:RecordBase {
        public decimal TelNo { get; set; }
        public string Email { get; set; }
        public double Lat { get; set; }
        public double Lang { get; set; }

        public int UserId { get; set; }
        public Users Users { get; set; }
    }
}
