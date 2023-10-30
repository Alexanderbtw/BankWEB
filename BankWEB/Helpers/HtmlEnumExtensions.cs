using Newtonsoft.Json;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BankWEB.Helpers
{
    public static class HtmlEnumExtensions
    {
        public static string EnumToString<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value));
            return JsonConvert.SerializeObject(enumDictionary);
        }
    }
}
