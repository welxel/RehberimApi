using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XNUnitTest.Manager {
    public class GeneralBusiness {
        public string GetJson(string pathName) {
            string json;
            string newPathName;
            newPathName = @"RequestJsons\" + pathName;
            try {
                using (StreamReader r = new StreamReader(newPathName)) {
                    json = r.ReadToEnd();
                }
                return json;
            } catch (Exception e) {
                throw new Exception(e.ToString());
            }
        }
    }
}
