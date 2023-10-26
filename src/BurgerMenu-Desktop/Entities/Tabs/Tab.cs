using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities.Tabs;

public class Tab
{
    public long id { get; set; }
    public string name { get; set; }=string.Empty;
    public long kassa_id { get; set; }
}
