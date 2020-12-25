using System.ComponentModel;

namespace Database.Enums
{
    public enum Country
    {
        [Description("Russia")]
        Russia = 1,
        [Description("USA")]
        Usa,
        [Description("UK")]
        Uk,
        [Description("France")]
        France
    }
}