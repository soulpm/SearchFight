using System.Web.Script.Serialization;

namespace SearchFight.Utilities
{
    public static class StringExtensions
    {
        public static T DeserializeJson<T>(this string json)
        {
            var utilJson = new JavaScriptSerializer();
            return utilJson.Deserialize<T>(json);
        }

        public static bool IsEmpty(this string text)
        {
            return (text.Trim() != "") ? false : true;
        }


    }
}