using System.ComponentModel;

namespace Database.Enums
{
    public enum Country
    {
        [Description("Россия")]
        Russia = 1,
        [Description("США")]
        Usa,
        [Description("Великобритания")]
        Uk,
        [Description("Франция")]
        France
    }
}