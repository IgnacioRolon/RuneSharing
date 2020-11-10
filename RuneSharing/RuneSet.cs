using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharing
{
    public class RuneSet
    {
        static RuneInformation runeData = new RuneInformation();

        public string pageName;
        public string championName;
        public string mainStyle;
        public string secStyle;
        public string keystone;
        public string sec1;
        public string sec2;
        public string sec3;
        public string sec4;
        public string sec5;
        public string flex1;
        public string flex2;
        public string flex3;

        public RuneSet(string pageName, string championName, string mainStyle, string secStyle, string keystone, string sec1, string sec2, string sec3, string sec4, string sec5, string flex1, string flex2, string flex3)
        {
            if(pageName == "" || championName == "" || mainStyle == "" || secStyle == "" ||
               keystone == "" || sec1 == "" || sec2 == "" || sec3 == "" || sec4 == "" ||
               sec5 == "" || flex1 == "" || flex2 == "" || flex3 == "")
            {
                throw new InvalidRuneSetException("Some of the fields used to create this Rune Page are empty.");
            }
            this.pageName = pageName;
            this.championName = championName;
            this.mainStyle = mainStyle;
            this.secStyle = secStyle;
            this.keystone = keystone;
            this.sec1 = sec1;
            this.sec2 = sec2;
            this.sec3 = sec3;
            this.sec4 = sec4;
            this.sec5 = sec5;
            this.flex1 = flex1;
            this.flex2 = flex2;
            this.flex3 = flex3;
        }

        public string GetPageName()
        {
            return this.pageName;
        }

        public string GetChampionName()
        {
            return this.championName;
        }

        public int[] GetSelectedPerks()
        {            
            int[] perks = {
                runeData.GetRuneId(keystone),
                runeData.GetRuneId(sec1),
                runeData.GetRuneId(sec2),
                runeData.GetRuneId(sec3),
                runeData.GetRuneId(sec4),
                runeData.GetRuneId(sec5),
                runeData.GetFlexId(flex1),
                runeData.GetFlexId(flex2),
                runeData.GetFlexId(flex3),
            };
            return perks;
        }

        public int GetMainStyleId()
        {
            return runeData.GetStyleId(mainStyle);
        }   

        public int GetSecStyleId()
        {
            return runeData.GetStyleId(secStyle);
        }
    }
}
