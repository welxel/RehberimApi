using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models {
    public class UserInformationModel:RecordBase {
        public int UserId { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public double Lat { get; set; }
        public double Lang { get; set; }
    }
}
