using AppCore.Records.Bases;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models {
    public class UsersModel:RecordBase {
        public string Ad { get; set; }
        public bool Status { get; set; }
        public string Soyad { get; set; }
        public string Guid { get; set; }
        public int UserInformationId { get; set; }
        public string Firma { get; set; }
        public UserInformation userInfo { get; set; }
    }
}
