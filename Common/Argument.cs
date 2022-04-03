using System;

namespace Common.Argument
{
    /// <summary>
    /// Валидатор параметров.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Проверяет Guid на пустоту.
        /// </summary>
        /// <param name="guid">Проверяемый на пустоту Guid.</param>
        /// <param name="parameterName">Название валидируемого параметра.</param>
        /// <exception cref="ArgumentException">Генерируется при пустом Guid.</exception>
        public static void GuidNotEmpty(Guid guid, string parameterName)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException($"Параметр {parameterName} типа Guid не может быть пустым.");
        }

        /// <summary>
        /// Проверяет строку на пустоту и null.
        /// </summary>
        /// <param name="validatingString">Проверяемая строка.</param>
        /// <param name="parameterName">Название валидируемого параметра.</param>
        /// <exception cref="ArgumentException">Генерируется при пустой строка или при строке, равной null.</exception>
        public static void StringNotNullOrEmpty(string validatingString, string parameterName)
        {
            if (string.IsNullOrEmpty(validatingString))
                throw new ArgumentException($"Параметр {parameterName} типа String не может быть null или пустым.");
        }

        /// <summary>
        /// Проверяет строку на потенциальную sql-иньекцию.
        /// </summary>
        /// <param name="validatingString">Валидируемая строка.</param>
        /// <param name="parameterName">Название валидируемого параметра.</param>
        /// <exception cref="SqlInjectionException">Генерируется при нахождении в валидируемой строке потенциальной sql-иньекции.</exception>
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
