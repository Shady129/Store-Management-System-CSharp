using BL.Contracts;
using Domains;
using Domains.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class Validator<T> : IValidator<T> where T : Domains.BaseEntity, new()
    {
        public bool Validate(T model)
        {

            if (model == null)
                return false;

            if (model.Id <= 0)
                return false;



            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);


            foreach (var prop in props)
            {
                if (prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                    continue;


                var value = prop.GetValue(model);


                if (prop.PropertyType == typeof(string))
                {
                    var str = value as string;

                    if (string.IsNullOrWhiteSpace(str))
                        return false;
                }



                if (prop.PropertyType == typeof(int) &&
                    value != null && (int)value < 0)
                    return false;

                if (prop.PropertyType == typeof(decimal) &&
                    value != null && (decimal)value < 0)
                    return false;

                if (prop.PropertyType == typeof(double) &&
                    value != null && (double)value < 0)
                    return false;



                if (prop.PropertyType == typeof(DateTime) &&
                    value != null && (DateTime)value == default)
                    return false;



                if (Attribute.IsDefined(prop, typeof(RequiredAttribute)))
                {
                    if (value == null)
                        return false;

                    if (prop.PropertyType == typeof(string) &&
                        string.IsNullOrWhiteSpace((string)value))
                        return false;
                }


                if (value == null)
                    continue;






                if (prop.PropertyType == typeof(string))
                {
                    var str = (string)value;

                    var minLen = prop.GetCustomAttribute<MinLengthAttribute>();

                    if (minLen != null && str.Length < minLen.Length)
                        return false;

                    var maxLen = prop.GetCustomAttribute<MaxLengthAttribute>();

                    if (maxLen != null && str.Length > maxLen.Length)
                        return false;




                    if (Attribute.IsDefined(prop, typeof(EmailAttribute)))
                    {
                        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                        if (!Regex.IsMatch(str, emailPattern))
                            return false;
                    }



                    var phoneAttr = prop.GetCustomAttribute<PhoneAttribute>();

                    if (phoneAttr != null)
                    {
                        if (!str.All(char.IsDigit))
                            return false;

                        if (phoneAttr.Length > 0 &&
                            str.Length != phoneAttr.Length)
                            return false;

                    }

                }

                var range = prop.GetCustomAttribute<RangeAttribute>();

                if (range != null)
                {
                    try
                    {
                        decimal number = Convert.ToDecimal(value);

                        if (number < range.Min || number > range.Max)
                            return false;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }


            return true;
        }

    }
}
    



