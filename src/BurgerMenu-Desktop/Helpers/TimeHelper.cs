using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TimeHelper
{
     public static DateTime GetDateTime()
    {
        var dTime = DateTime.UtcNow;        
        return dTime;                            
    }
}
