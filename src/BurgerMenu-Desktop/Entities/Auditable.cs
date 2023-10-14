using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Entities;

public class Auditable : BaseEntity
{
    [Required]
    public DateTime? CreatedAt { get; set; }
    [Required]
    public DateTime? UpdatedAt { get;set; }
}
