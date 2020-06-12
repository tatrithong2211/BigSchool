using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool Isvalid(object value)

        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(Convert.ToString(Value),

                "dd/M/yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out DateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}