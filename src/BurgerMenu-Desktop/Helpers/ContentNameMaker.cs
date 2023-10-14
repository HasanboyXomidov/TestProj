using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMenu_Desktop.Helpers;

public class ContentNameMaker
{
    public static string GetImageName (string FilePath)
    {
        FileInfo fileInfo = new FileInfo(FilePath);
        return "IMG_" + Guid.NewGuid().ToString() + fileInfo.Extension;
    }
}
