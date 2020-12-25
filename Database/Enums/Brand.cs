using System.ComponentModel;

namespace Database.Enums
{
    public enum Brand
    {
        [Description("Adidas")]
        Adidas = 1,
        [Description("Nike")]
        Nike, 
        [Description("Puma")]
        Puma, 
        [Description("Supreme")]
        Supreme, 
        [Description("Asos")]
        Asos, 
        [Description("Bershka")]
        Bershka
    }
}