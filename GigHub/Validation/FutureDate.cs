﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Validation
{
    public class FutureDate:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
          DateTime dateTime;
          bool isValid =  DateTime.TryParseExact(Convert.ToString(value), "d MMM yyyy", 
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, out dateTime);
            return isValid && dateTime > DateTime.Now;
            
        }
    }
}