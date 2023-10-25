using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.CasheRegisters;

public class Kassa
{
    public long Id { get; set; }    
    public string Name { get; set; }= string.Empty;
    public long ShopId { get; set; }
}
