using System.ComponentModel;

namespace Database.Enums
{
    public enum Category
    {
        [Description("Одежда")]
        Clothes = 1, 
        [Description("Обувь")]
        Footwear, 
        [Description("Спорт")]
        Sportswear
    }
}