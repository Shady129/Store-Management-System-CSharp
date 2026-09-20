using DAL.Contracts;
using DAL.Exceptions;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.FileConverters
{
    public class JsonConverter<T> : IConverter<T> where T : Domains.BaseEntity, new()
    {
        public string ConvertData(List<T> data)
        {

            return JsonSerializer.Serialize(data);

        }

        public List<T> ConvertBack(string fileData)
        {
            try
            {
                return JsonSerializer.Deserialize<List<T>>(fileData);
            }
            catch(Exception ex)
            {
                throw new SerlizationnExecption("Error converting data from JSON format.", "", ex);
            }

        }

    }
}
