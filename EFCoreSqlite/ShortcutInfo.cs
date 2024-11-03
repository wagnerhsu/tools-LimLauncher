using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSqlite;

public class ShortcutInfo
{
    public Guid ID { get; set; }
    public Guid GroupID { get; set; }
    public string FileFullPath { get; set; }
    public string FileName { get; set; }
}