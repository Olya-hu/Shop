using System.ComponentModel;

namespace Database.Enums
{
    public enum Status
    {
        [Description("В обработке")]
        Processing = 1,
        [Description("В пути")]
        Shipping,
        [Description("Доставлен")]
        Delivered
    }
}