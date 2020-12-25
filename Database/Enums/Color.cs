using System.ComponentModel;

namespace Database.Enums
{
    public enum Color
    {
        [Description("White")]
        White = 1,
        [Description("Black")]
        Black, 
        [Description("Black&White")]
        BlackWhite, 
        [Description("Grey")]
        Grey, 
        [Description("Red")]
        Red, 
        [Description("Orange")]
        Orange, 
        [Description("Yellow")]
        Yellow, 
        [Description("Green")]
        Green,
        [Description("Cyan")]
        Cyan, 
        [Description("Blue")]
        Blue, 
        [Description("Purple")]
        Purple, 
        [Description("Pink")]
        Pink, 
        [Description("Multicolor")]
        Multicolor
    }
}