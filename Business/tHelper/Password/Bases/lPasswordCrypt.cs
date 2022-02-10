using System;
using System.Collections.Generic;
using System.Text;

namespace Business.tHelper.Bases
{
    public interface lPasswordCrypt
    {
        public string Crypt(string text);
        public bool Check(string text, string compare);
    }
}
