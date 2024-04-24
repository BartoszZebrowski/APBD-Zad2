using System.ComponentModel;

namespace WebApplication1.Animals
{
    public enum AnimalColumn
    {
        [Description("Name")]
        Name,

        [Description("Description")]
        Description,

        [Description("Category")]
        Category,

        [Description("Area")]
        Area
    }
}
