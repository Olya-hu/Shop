using System.ComponentModel;

namespace Database.Enums
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female, 
        [Description("Unisex")]
        Unisex
    }
}