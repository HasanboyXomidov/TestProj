using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.Shops;

public class Shop : Auditable
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string ImagePath { get; set; } = string.Empty;

}
