using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Utilities
{
    public class Validation
    {
        public static bool IsEmptyTextArray(string [] list) {
            bool value = false;
            foreach (string item in list) {
                if (!item.IsEmpty())
                {
                    value = true;
                    break;
                }
            } 
            return value;
        }


        public static string[] GetArguments(string line) {
            string[] list = null;
            if (line.Contains("\""))
            {
                list = line.Split('\"');
                if (list.Count() == 0) list = line.Split(' ');
            }
            else {
                list = line.Split(' ');
            }
            return list;
        }

        public static string[] GetArgumentsNotEmpty(string[] list)
        {
            List<string> data = new List<string>();
            foreach (string item in list)
            {
                if (item.Trim() != "") { data.Add(item); }
            }
            string[] dataReturn = new string [data.Count];
            data.CopyTo(dataReturn);
            return dataReturn;
        }

    }
}
