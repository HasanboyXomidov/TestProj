﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.ViewModels.Shops;

public class ShopsViewModel
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;

}
