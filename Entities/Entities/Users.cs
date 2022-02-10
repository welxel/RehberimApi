using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities {
    public class Users:RecordBase {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public string GUID { get; set; }
        //Kişi kaldırmak için konulan alandır.
        public bool Status { get; set; }
        public UserInformation userInfo { get; set; }
    }
}
