using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Business.Models.Bases
{
    public interface IResultData<out TResultType>
    {
        TResultType Data { get; }
    }
}
