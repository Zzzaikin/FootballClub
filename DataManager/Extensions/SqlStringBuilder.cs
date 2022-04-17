using Common.Argument;
using System.Collections.Generic;
using System.Text;

namespace DataManager.Extensions
{
    public static class SqlStringBuilder
    {
        public static string AppendJoins(this string str, List<Dictionary<string, string>> joinsDictionaries)
        {
            var stringBuilder = new StringBuilder(str);

            foreach (var joinDictionary in joinsDictionaries)
            {
                foreach (var join in joinDictionary)
                {
                    var joiningColumnName = join.Key;
                    var targetColumnName = join.Value;
                    var joiningEntityName = join.Value.Split('.')[0];

                    var joinString =
                        $"LEFT JOIN {joiningEntityName} " +
                        $"ON {joiningColumnName} = {targetColumnName} ";

                    stringBuilder.Append(joinString);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
