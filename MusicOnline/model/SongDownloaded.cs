using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOnline.model
{
  public class SongDownloaded
    {
        public String id { get; set; }
        public String name { get; set; }
        public String artist { get; set; }
        public String source_list { get; set; }
        public override Boolean Equals(Object obj)
        {
            if (this.id == ((Song)obj).id)
            {
                return true;
            }
            return false;
        }
    }
}
