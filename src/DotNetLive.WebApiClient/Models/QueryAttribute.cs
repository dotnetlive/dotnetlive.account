using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient
{
    public class QueryAttribute : Attribute
    {
        private IConverter _converter;
        public string Name { get; set; }
        public string Format { get; set; }
        public string ConverterType { get; set; }

        public IConverter Converter
        {
            get
            {
                if (_converter == null)
                {
                    _converter = new SimpleConverter();
                    if (!string.IsNullOrEmpty(ConverterType))
                    {
                        try
                        {
                            Type type = Type.GetType(ConverterType);
                            _converter = Activator.CreateInstance(type) as IConverter;
                        }
                        catch
                        {
                        }
                    }
                }
                return _converter;
            }
        }
    }

    public class SimpleConverter : IConverter
    {
        public object Convert(object value, string format)
        {
            return value;
        }
    }

    public interface IConverter
    {
        object Convert(object value, string format);
    }
}
