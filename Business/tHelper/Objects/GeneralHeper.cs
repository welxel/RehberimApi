using AppCore.Business.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.tHelper.Objects
{
    public static class GeneralHeper
    {
        public static Result<object> ObjectIsNull(object o)
        {
            var props = o.GetType().GetProperties();
            if (props.Length == 0)
            {
                if ((o is int && ((int)o == 0)) || (o is string && ((string)o).Length == 0))
                {
                    return new ErrorResult<object>(o.GetType().Name + " Boş bırakılamaz");
                }
            }
            foreach (var item in props)
            {
                var values = item.GetValue(o);
                if (values == null)// || (values is int&& ((int)values==0) ) || (values is string &&((string)values).Length==0)
                {
                    return new ErrorResult<object>(item.Name + " Boş bırakılamaz");
                }

            }
            return new SuccessResult<object>();
        }
    }
}
