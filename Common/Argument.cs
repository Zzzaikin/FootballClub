using System;
using System.Collections.Generic;

namespace Common.Argument
{
    public static class Argument
    {
        public static void IntegerMoreThanZero(int validatingInteger, string parameterName)
        {
            if (validatingInteger < 0)
            {
                throw new ArgumentException($"{parameterName} оказался меньше нулю.");
            }
        }

        public static void IntegerNotZero(int validatingInteger, string parameterName)
        {
            if (validatingInteger == 0)
            {
                throw new ArgumentException($"{parameterName} оказался меньше нулю.");
            }
        }

        public static void ValidateDictionariesByAllPolicies(List<Dictionary<string, string>> validatingDictionaries)
        {
            foreach (var validatingDictionary in validatingDictionaries)
            {
                foreach (var item in validatingDictionary)
                {
                    ValidateStringByAllPolicies(item.Key, nameof(item.Key));
                    ValidateStringByAllPolicies(item.Value, nameof(item.Value));
                }
            }
        }

        public static void NotNull(object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException($"Параметр {parameterName} равен null.");
        }

        public static void RecordsCountLessThanMaxValue(int count)
        {
            if (count > 1000) 
                throw new ArgumentOutOfRangeException($"Количество записей для выборки превысило максимально допустимое значение в 1000 записей");
        }

        public static void ValidateStringByAllPolicies(string validatingString, string parameterName)
        {
            StringNotNullOrEmpty(validatingString, parameterName);
            StringNotContainsSqlInjections(validatingString, parameterName);
        }

        public static void GuidNotEmpty(Guid guid, string parameterName)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException($"Параметр {parameterName} типа Guid не может быть пустым.");
        }

        public static void StringNotNullOrEmpty(string validatingString, string parameterName)
        {
            if (string.IsNullOrEmpty(validatingString))
                throw new ArgumentException($"Параметр {parameterName} типа String не может быть null или пустым.");
        }

        public static void StringNotContainsSqlInjections(string validatingString, string parameterName)
        {
            var validatingStringInLowerRegistry = validatingString.ToLower();

            if (validatingStringInLowerRegistry.Contains("drop table") ||
                validatingStringInLowerRegistry.Contains("drop database") ||
                validatingStringInLowerRegistry.Contains("create table") ||
                validatingStringInLowerRegistry.Contains("alter table") ||
                validatingStringInLowerRegistry.Contains("delete from") ||
                validatingStringInLowerRegistry.Contains("update ") ||
                validatingStringInLowerRegistry.Contains("update`") ||
                validatingStringInLowerRegistry.Contains("insert into") ||
                validatingStringInLowerRegistry.Contains("select*") ||
                validatingStringInLowerRegistry.Contains("select`") ||
                validatingStringInLowerRegistry.Contains("select "))
            {
                throw new SqlInjectionException($"В параметре {parameterName} обнаружена потенциальная sql-иньекция. " +
                    $"Значение параметра: {validatingString}");
            }
        }
    }
}
