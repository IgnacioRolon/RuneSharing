using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharing
{
    public class RunePage
    {
        public string[] autoModifiedSelections;
        public bool current;
        public long id;
        public bool isActive;
        public bool isDeletable;
        public bool isEditable;
        public bool isValid;
        public long lastModified;
        public string name;
        public int order;
        public int primaryStyleId;
        public int[] selectedPerkIds;
        public int subStyleId;

        public RunePage()
        {
        }

        public override string ToString()
        {
            return id.ToString() + " - " + name;
        }
    }
}
