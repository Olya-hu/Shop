using System.ComponentModel;

namespace Database.Enums
{
    public enum Color
    {
        [Description("Белый")]
        White = 1,
        [Description("Чёрный")]
        Black, 
        [Description("Чёрно-белый")]
        BlackWhite, 
        [Description("Серый")]
        Grey, 
        [Description("Красный")]
        Red, 
        [Description("Оранжевый")]
        Orange, 
        [Description("Жёлтый")]
        Yellow, 
        [Description("Зелёный")]
        Green,
        [Description("Голубой")]
        Cyan, 
        [Description("Синий")]
        Blue, 
        [Description("Фиолетовый")]
        Purple, 
        [Description("Розовый")]
        Pink, 
        [Description("Разноцветный")]
        Multicolor
    }
}