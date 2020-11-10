using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RuneSharing
{
    public class RuneSeed
    {
        string seed;

        public RuneSeed(string seed)
        {
            this.seed = seed;
        }

        public RuneSeed(RuneSet runeSet)
        {
            this.EncodeSeed(runeSet);       
        }

        public string GetSeed()
        {
            return this.seed;
        }

        public RuneSet DecodeSeed()
        {
            string decodedSeed = Encoding.Unicode.GetString(Convert.FromBase64String(seed));
            return JsonConvert.DeserializeObject<RuneSet>(decodedSeed);
        }

        public void EncodeSeed(RuneSet runeSet)
        {
            string convertedSeed = JsonConvert.SerializeObject(runeSet);
            this.seed = Convert.ToBase64String(Encoding.Unicode.GetBytes(convertedSeed));
        }
    }
}
