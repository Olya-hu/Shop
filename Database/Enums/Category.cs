using System.ComponentModel;

namespace Database.Enums
{
    public enum Category
    {
        [Description("Clothes")]
        Clothes = 1, 
        [Description("Footwear")]
        Footwear, 
        [Description("Sportswear")]
        Sportswear
    }
}