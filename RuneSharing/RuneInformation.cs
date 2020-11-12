using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharing
{
    public class Runes
    {
        public string runeName;
        public int runeId;
        public string runeType;
        public string runePath;

        public Runes(string runeName, int runeId, string runeType, string runePath)
        {
            this.runeName = runeName;
            this.runeId = runeId;
            this.runeType = runeType;
            this.runePath = runePath;
        }

        public Runes(string runeName)
        {
            RuneInformation runeInformation = new RuneInformation();
            Runes runeResult = runeInformation.runeList.Find(delegate (Runes rune)
            {
                return rune.runeName == runeName;
            });

            this.runeName = runeName;
            this.runeId = runeResult.runeId;
            this.runePath = runeResult.runePath;
            this.runeType = runeResult.runeType;
        }
    }

    public class SeedFile
    {
        public string pageName;
        public string championName;
        public RuneSeed seed;

        public SeedFile(string pageName, string championName, RuneSeed seed)
        {
            this.pageName = pageName;
            this.championName = championName;
            this.seed = seed;
        }
    }


    class RuneInformation
    {
        public Dictionary<string, int> styleDictionary = new Dictionary<string, int>();
        public Dictionary<string, int> flexDictionary = new Dictionary<string, int>();
        public List<Runes> runeList = new List<Runes>();
        

        public RuneInformation()
        {
            runeList.Add(new Runes("Press the Attack", 8005, "Key", "Precision"));
            runeList.Add(new Runes("Lethal Tempo", 8008, "Key", "Precision"));
            runeList.Add(new Runes("Fleet Footwork", 8021, "Key", "Precision"));
            runeList.Add(new Runes("Overheal", 9101, "Sec1", "Precision"));
            runeList.Add(new Runes("Legend: Alacrity", 9104, "Sec2", "Precision"));
            runeList.Add(new Runes("Legend: Tenacity", 9105, "Sec2", "Precision"));
            runeList.Add(new Runes("Coup de Grace", 8014, "Sec3", "Precision"));
            runeList.Add(new Runes("Triumph", 9111, "Sec1", "Precision"));            
            runeList.Add(new Runes("Cut Down", 8017, "Sec3", "Precision"));
            runeList.Add(new Runes("Conqueror", 8010, "Key", "Precision"));
            runeList.Add(new Runes("Presence of Mind", 8009, "Sec1", "Precision"));
            runeList.Add(new Runes("Legend: Bloodline", 9103, "Sec2", "Precision"));
            runeList.Add(new Runes("Last Stand", 8299, "Sec3", "Precision"));
            runeList.Add(new Runes("Electrocute", 8112, "Key", "Domination"));
            runeList.Add(new Runes("Cheap Shot", 8126, "Sec1", "Domination"));
            runeList.Add(new Runes("Zombie Ward", 8136, "Sec2", "Domination"));
            runeList.Add(new Runes("Ravenous Hunter", 8135, "Sec3", "Domination"));
            runeList.Add(new Runes("Predator", 8124, "Key", "Domination"));
            runeList.Add(new Runes("Taste of Blood", 8139, "Sec1", "Domination"));
            runeList.Add(new Runes("Ghost Poro", 8120, "Sec2", "Domination"));
            runeList.Add(new Runes("Ingenious Hunter", 8134, "Sec3", "Domination"));
            runeList.Add(new Runes("Dark Harvest", 8128, "Key", "Domination"));
            runeList.Add(new Runes("Sudden Impact", 8143, "Sec1", "Domination"));
            runeList.Add(new Runes("Eyeball Collection", 8138, "Sec2", "Domination"));
            runeList.Add(new Runes("Relentless Hunter", 8105, "Sec3", "Domination"));
            runeList.Add(new Runes("Hail of Blades", 9923, "Key", "Domination"));
            runeList.Add(new Runes("Ultimate Hunter", 8106, "Sec3", "Domination"));
            runeList.Add(new Runes("Summon Aery", 8214, "Key", "Sorcery"));
            runeList.Add(new Runes("Nullifying Orb", 8224, "Sec1", "Sorcery"));
            runeList.Add(new Runes("Transcendence", 8210, "Sec2", "Sorcery"));
            runeList.Add(new Runes("Scorch", 8237, "Sec3", "Sorcery"));
            runeList.Add(new Runes("Arcane Comet", 8229, "Key", "Sorcery"));
            runeList.Add(new Runes("Manaflow Band", 8226, "Sec1", "Sorcery"));
            runeList.Add(new Runes("Celerity", 8234, "Sec2", "Sorcery"));
            runeList.Add(new Runes("Waterwalking", 8232, "Sec3", "Sorcery"));
            runeList.Add(new Runes("Phase Rush", 8230, "Key", "Sorcery"));
            runeList.Add(new Runes("Nimbus Cloak", 8275, "Sec1", "Sorcery"));
            runeList.Add(new Runes("Absolute Focus", 8233, "Sec2", "Sorcery"));
            runeList.Add(new Runes("Gathering Storm", 8236, "Sec3", "Sorcery"));
            runeList.Add(new Runes("Grasp of the Undying", 8437, "Key", "Resolve"));
            runeList.Add(new Runes("Demolish", 8446, "Sec1", "Resolve"));
            runeList.Add(new Runes("Conditioning", 8429, "Sec2", "Resolve"));
            runeList.Add(new Runes("Overgrowth", 8451, "Sec3", "Resolve"));
            runeList.Add(new Runes("Aftershock", 8439, "Key", "Resolve"));
            runeList.Add(new Runes("Font of Life", 8463, "Sec1", "Resolve"));
            runeList.Add(new Runes("Second Wind", 8444, "Sec2", "Resolve"));
            runeList.Add(new Runes("Revitalize", 8453, "Sec3", "Resolve"));
            runeList.Add(new Runes("Guardian", 8465, "Key", "Resolve"));
            runeList.Add(new Runes("Shield Bash", 8401, "Sec1", "Resolve"));
            runeList.Add(new Runes("Bone Plating", 8473, "Sec2", "Resolve"));
            runeList.Add(new Runes("Unflinching", 8242, "Sec3", "Resolve"));
            runeList.Add(new Runes("Glacial Augment", 8351, "Key", "Inspiration"));
            runeList.Add(new Runes("Hextech Flashtraption", 8306, "Sec1", "Inspiration"));
            runeList.Add(new Runes("Future's Market", 8321, "Sec2", "Inspiration"));
            runeList.Add(new Runes("Cosmic Insight", 8347, "Sec3", "Inspiration"));
            runeList.Add(new Runes("Unsealed Spellbook", 8360, "Key", "Inspiration"));
            runeList.Add(new Runes("Magical Footwear", 8304, "Sec1", "Inspiration"));
            runeList.Add(new Runes("Minion Dematerializer", 8316, "Sec2", "Inspiration"));
            runeList.Add(new Runes("Approach Velocity", 8410, "Sec3", "Inspiration"));
            runeList.Add(new Runes("Prototype: Omnistone", 8358, "Key", "Inspiration"));
            runeList.Add(new Runes("Perfect Timing", 8313, "Sec1", "Inspiration"));
            runeList.Add(new Runes("Biscuit Delivery", 8345, "Sec2", "Inspiration"));
            runeList.Add(new Runes("Time Warp Tonic", 8352, "Sec3", "Inspiration"));

            flexDictionary.Add("+9 AF", 5008);
            flexDictionary.Add("10% AS", 5005);
            flexDictionary.Add("+6 AR", 5002);
            flexDictionary.Add("+15-90 HP", 5001);
            flexDictionary.Add("+8 AH", 5007);
            flexDictionary.Add("+8 MR", 5003);         

            styleDictionary.Add("Precision", 8000);
            styleDictionary.Add("Domination", 8100);
            styleDictionary.Add("Sorcery", 8200);
            styleDictionary.Add("Resolve", 8400);
            styleDictionary.Add("Inspiration", 8300);
        }

        public int GetStyleId(string style)
        {
            return styleDictionary[style];
        }

        public int GetFlexId(string flex)
        {
            return flexDictionary[flex];
        }

        public int GetRuneId(string runeName)
        {
            Runes runeResult = this.runeList.Find(delegate (Runes rune)
            {
                return rune.runeName == runeName;
            });

            return runeResult.runeId;
        }

        public string GetRuneType(string runeName)
        {
            Runes runeResult = this.runeList.Find(delegate (Runes rune)
            {
                return rune.runeName == runeName;
            });

            return runeResult.runeType;
        }
    }
}
