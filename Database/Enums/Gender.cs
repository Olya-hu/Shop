using System.ComponentModel;

namespace Database.Enums
{
    public enum Gender
    {
        [Description("Мужской")]
        Male = 1,
        [Description("Женский")]
        Female, 
        [Description("Унисекс")]
        Unisex
    }
}