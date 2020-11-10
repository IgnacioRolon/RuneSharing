using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RuneSharing
{
    public partial class frmMain : Form
    {
        LCUConnector connector = new LCUConnector("C:/Riot Games/League of Legends");
        RunePage runes;
        RuneInformation runeData = new RuneInformation();
        RuneSet selectedRunes;
        string runePageDataPath = AppDomain.CurrentDomain.BaseDirectory + "/RunePages.cfg";
        List<SeedFile> fileSeeds = new List<SeedFile>();


        string changedCombo = "";

        public frmMain()
        {
            InitializeComponent();            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadChampionList();
            LoadPages();
        }

        private void LoadPages()
        {
            if(File.Exists(runePageDataPath))
            {
                foreach(string line in File.ReadLines(runePageDataPath))
                {
                    RuneSeed newSeed = new RuneSeed(line);
                    RuneSet currentSet = newSeed.DecodeSeed();
                    cbRunePage.Items.Add(currentSet.pageName);
                    SeedFile currentSeed = new SeedFile(currentSet.pageName, currentSet.championName, newSeed);
                    fileSeeds.Add(currentSeed);
                }
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void btnCopySeed_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtSeed.Text);
            MessageBox.Show("Seed copied to clipboard!", "Seed Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            //Call the API to Delete and Recreate the rune page with the new Runes Selected.
            runes = await connector.APICall(LCUConnector.RequestType.GET, "lol-perks/v1/currentpage", null);
            await connector.APICall(LCUConnector.RequestType.DELETE, $"lol-perks/v1/pages/{runes.id}", null);

            GetSelectedRunes(); //This function edits the runes object to create the new page.
            await connector.APICall(LCUConnector.RequestType.POST, "/lol-perks/v1/pages/", runes);
            MessageBox.Show($"Runes imported to League! Name: {runes.name}", "Runes imported.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbMainPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbKeystone.Items.Clear();
            cbSec1.Items.Clear();
            cbSec2.Items.Clear();
            cbSec3.Items.Clear();
            cbSec4.Items.Clear();
            cbSec5.Items.Clear();
            cbSec6.Items.Clear();
            cbSecPath.Items.Clear();

            List<Runes> mainKeyRunes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbMainPath.Text && rune.runeType == "Key";
                });

            List<Runes> mainSec1Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbMainPath.Text && rune.runeType == "Sec1";
                });

            List<Runes> mainSec2Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbMainPath.Text && rune.runeType == "Sec2";
                });

            List<Runes> mainSec3Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbMainPath.Text && rune.runeType == "Sec3";
                });

            foreach(string style in runeData.styleDictionary.Keys)
            {
                if(style != cbMainPath.Text)
                {
                    cbSecPath.Items.Add(style);
                }
            }

            foreach (Runes rune in mainKeyRunes)
            {
                cbKeystone.Items.Add(rune.runeName);
            }

            foreach (Runes rune in mainSec1Runes)
            {
                cbSec1.Items.Add(rune.runeName);
            }

            foreach (Runes rune in mainSec2Runes)
            {
                cbSec2.Items.Add(rune.runeName);
            }

            foreach (Runes rune in mainSec3Runes)
            {
                cbSec3.Items.Add(rune.runeName);
            }

            
        }

        private void cbSecPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSec4.Items.Clear();
            cbSec5.Items.Clear();
            cbSec6.Items.Clear();

            List<Runes> secSec1Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbSecPath.Text && rune.runeType == "Sec1";
                });

            List<Runes> secSec2Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbSecPath.Text && rune.runeType == "Sec2";
                });

            List<Runes> secSec3Runes = runeData.runeList.FindAll(
                delegate (Runes rune)
                {
                    return rune.runePath == cbSecPath.Text && rune.runeType == "Sec3";
                });

            foreach (Runes rune in secSec1Runes)
            {
                cbSec4.Items.Add(rune.runeName);
            }

            foreach (Runes rune in secSec2Runes)
            {
                cbSec5.Items.Add(rune.runeName);
            }

            foreach (Runes rune in secSec3Runes)
            {
                cbSec6.Items.Add(rune.runeName);
            }            
        }

        private void GetSelectedRunes()
        {
            //There're two slots for secondary runes, and 3 combos to choose from. 
            //Since they can't have values all 3 at the same time, this logic allows to get the 4th and 5th selected runes.
            string sec4, sec5;
            if(cbSec4.Text != "")
            {
                sec4 = cbSec4.Text;
                if(cbSec5.Text != "")
                {
                    sec5 = cbSec5.Text;
                }
                else
                {
                    sec5 = cbSec6.Text;
                }
            }
            else
            {
                sec4 = cbSec5.Text;
                sec5 = cbSec6.Text;
            }

            int[] flexRunes = { 0, 0, 0 }; //3 empty slots for the 3 flex runes.            
            //Get the First Flex Rune
            foreach(RadioButton radio in panel1.Controls.OfType<RadioButton>())
            {                
                if (radio.Checked == true)
                {
                    flexRunes[0] = runeData.flexDictionary[radio.Text];
                    break;
                }
            }

            //Get the Second Flex Rune
            foreach (RadioButton radio in panel2.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    flexRunes[1] = runeData.flexDictionary[radio.Text];
                    break;
                }
            }

            //Get the Third Flex Rune
            foreach (RadioButton radio in panel3.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    flexRunes[2] = runeData.flexDictionary[radio.Text];
                    break;
                }
            }

            //This array contains the names of all the runes to be added into the id array, excepting the flex ones.
            string[] selectedRunes = {
                cbKeystone.Text,
                cbSec1.Text,
                cbSec2.Text,
                cbSec3.Text,
                sec4,
                sec5
            };

            int[] selectedIds = {0, 0, 0, 0, 0, 0, 0, 0, 0}; //This array contains 9 empty slots to be filled with the corresponding IDs.
            int i = 0;
            foreach (Runes rune in runeData.runeList)
            {
                if(selectedRunes.Contains(rune.runeName))
                {
                    selectedIds[i] = rune.runeId;
                    i++;
                }
            }

            foreach(int flexRune in flexRunes)
            {
                selectedIds[i] = flexRune;
                i++;
            }

            runes.selectedPerkIds = selectedIds;
            runes.name = cbRunePage.Text;
            runes.primaryStyleId = runeData.styleDictionary[cbMainPath.Text];
            runes.subStyleId = runeData.styleDictionary[cbSecPath.Text];
        }        

        void LoadChampionList()
        {
            foreach(string line in File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "/ChampionNames.cfg"))
            {
                cbChampions.Items.Add(line);
            }
            cbChampions.SelectedIndex = 0;
        }

        private void cbSec4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbSec5.Text != "" && cbSec6.Text != "" && changedCombo == "Sec4")
            {
                cbSec6.Items.Add("");
                cbSec6.Text = "";
                cbSec6.Items.Remove("");

                GenerateSeed();
            }
        }

        private void cbSec5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSec4.Text != "" && cbSec6.Text != "" && changedCombo == "Sec5")
            {
                cbSec6.Items.Add("");
                cbSec6.Text = "";
                cbSec6.Items.Remove("");

                GenerateSeed();
            }
        }

        private void cbSec6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSec5.Text != "" && cbSec4.Text != "" && changedCombo == "Sec6")
            {
                cbSec5.Items.Add("");
                cbSec5.Text = "";
                cbSec5.Items.Remove("");

                GenerateSeed();
            }
        }

        private void cbSec6_Click(object sender, EventArgs e)
        {
            changedCombo = "Sec6";
        }

        private void cbSec5_Click(object sender, EventArgs e)
        {
            changedCombo = "Sec5";
        }

        private void cbSec4_Click(object sender, EventArgs e)
        {
            changedCombo = "Sec4";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtSeed.Text != "")
            {
                if (!File.Exists(runePageDataPath))
                {
                    File.Create(runePageDataPath);
                }

                StreamWriter file = File.AppendText(runePageDataPath);
                file.WriteLine(txtSeed.Text);
                file.Close();

                fileSeeds.Add(new SeedFile(cbRunePage.Text, cbChampions.Text, new RuneSeed(txtSeed.Text)));
                cbRunePage.Items.Add(cbRunePage.Text);
            }            
        }

        private void ReadRunePages()
        {
            
        }

        private string[] GetSecondaryRunes()
        {
            string sec4, sec5;
            if (cbSec4.Text != "")
            {
                sec4 = cbSec4.Text;
                if (cbSec5.Text != "")
                {
                    sec5 = cbSec5.Text;
                }else
                {
                    sec5 = cbSec6.Text;
                }
            }
            else
            {
                sec4 = cbSec5.Text;
                sec5 = cbSec6.Text;
            }

            string[] secRunes = { sec4, sec5 };
            return secRunes;
        }

        private string[] GetFlexRunes()
        {
            string flex1 = "", flex2 = "", flex3 = "";                
            foreach(RadioButton radio in panel1.Controls.OfType<RadioButton>()) //Check if the Radiobuttons have a checked option.
            {
                if(radio.Checked == true)
                {
                    flex1 = radio.Text;
                    break;
                }
            }
            foreach (RadioButton radio in panel2.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    flex2 = radio.Text;
                    break;
                }
            }
            foreach (RadioButton radio in panel3.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    flex3 = radio.Text;
                    break;
                }
            }

            string[] flexRunes = { flex1, flex2, flex3 };
            return flexRunes;
        }

        private void GenerateSeed()
        {
            if(CheckAllValues())
            {
                try
                {
                    string[] secRunes = GetSecondaryRunes();
                    string[] flexRunes = GetFlexRunes();
                    RuneSet runeSet = new RuneSet(cbRunePage.Text, cbChampions.Text, cbMainPath.Text, cbSecPath.Text, cbKeystone.Text,
                                                  cbSec1.Text, cbSec2.Text, cbSec3.Text, secRunes[0], secRunes[1], flexRunes[0], 
                                                  flexRunes[1], flexRunes[2]);

                    RuneSeed seed = new RuneSeed(runeSet);
                    txtSeed.Text = seed.GetSeed();
                }catch(InvalidRuneSetException ex)
                {
                    MessageBox.Show(ex.Message, "Error Generating Seed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtSeed.Text = "";
            }
        }

        private bool CheckAllValues()
        {
            if(cbRunePage.Text != "" && cbKeystone.Text != "" && cbSec1.Text != "" && cbSec2.Text != "" &&
               cbSec3.Text != "" && cbMainPath.Text != "" && cbSecPath.Text != "" && cbChampions.Text != "")
            {
                string[] flexRunes = GetFlexRunes();
                string[] secRunes = GetSecondaryRunes();
                if(flexRunes[0] != "" && flexRunes[1] != "" && flexRunes[2] != "" &&
                   secRunes[0] != "" && secRunes[1] != "")
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadSeed()
        {
            RuneSeed seed = new RuneSeed(txtSeed.Text);
            selectedRunes = seed.DecodeSeed();

            cbRunePage.Text = selectedRunes.pageName;
            cbChampions.Text = selectedRunes.championName;
            cbMainPath.Text = selectedRunes.mainStyle;
            cbSecPath.Text = selectedRunes.secStyle;
            cbKeystone.Text = selectedRunes.keystone;
            cbSec1.Text = selectedRunes.sec1;
            cbSec2.Text = selectedRunes.sec2;
            cbSec3.Text = selectedRunes.sec3;
            //Select Secondary Runes
            switch (runeData.GetRuneType(selectedRunes.sec4))
            {
                case "Sec1":
                    cbSec4.Text = selectedRunes.sec4;
                    break;
                case "Sec2":
                    cbSec5.Text = selectedRunes.sec4;
                    break;
                case "Sec3":
                    cbSec6.Text = selectedRunes.sec4;
                    break;
            }
            switch (runeData.GetRuneType(selectedRunes.sec5))
            {
                case "Sec1":
                    cbSec4.Text = selectedRunes.sec5;
                    break;
                case "Sec2":
                    cbSec5.Text = selectedRunes.sec5;
                    break;
                case "Sec3":
                    cbSec6.Text = selectedRunes.sec5;
                    break;
            }
            //Select Flex Runes
            switch (selectedRunes.flex1)
            {
                case "+9 AF":
                    rbAf1.Checked = true;
                    break;
                case "10% AS":
                    rbAs1.Checked = true;
                    break;
                case "+1-10% CDR":
                    rbCdr1.Checked = true;
                    break;
            }
            switch (selectedRunes.flex2)
            {
                case "+9 AF":
                    rbAf2.Checked = true;
                    break;
                case "+6 AR":
                    rbAr2.Checked = true;
                    break;
                case "+8 MR":
                    rbMr2.Checked = true;
                    break;
            }
            switch (selectedRunes.flex3)
            {
                case "+15-90 HP":
                    rbHp3.Checked = true;
                    break;
                case "+6 AR":
                    rbAr3.Checked = true;
                    break;
                case "+8 MR":
                    rbMr3.Checked = true;
                    break;
            }
        }

        private void btnImportSeed_Click(object sender, EventArgs e)
        {
            LoadSeed();
        }

        private void cbKeystone_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void cbSec1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void cbSec2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void cbSec3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void cbRunePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeedFile foundSeed = fileSeeds.Find(delegate (SeedFile seed)
            {
                return seed.pageName == cbRunePage.Text;
            });

            txtSeed.Text = foundSeed.seed.GetSeed();
            LoadSeed();
        }

        private void cbRunePage_TextChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            GenerateSeed();
        }

        private void cbChampions_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbRunePage.Items.Clear();
            cbRunePage.Text = "";
            foreach(SeedFile runePage in fileSeeds)
            {
                if(runePage.championName == cbChampions.Text)
                {
                    cbRunePage.Items.Add(runePage.pageName);
                }                
            }
            try
            {
                cbRunePage.SelectedIndex = 0;                
            }
            catch(ArgumentOutOfRangeException)
            {
                ResetFields();
            }            
        }

        private void ResetFields()
        {
            cbMainPath.SelectedIndex = 1;
            cbMainPath.SelectedIndex = 0;

            foreach (RadioButton radio in panel1.Controls.OfType<RadioButton>()) //Check if the Radiobuttons have a checked option.
            {
                if (radio.Checked == true)
                {
                    radio.Checked = false;
                    break;
                }
            }
            foreach (RadioButton radio in panel2.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    radio.Checked = false;
                    break;
                }
            }
            foreach (RadioButton radio in panel3.Controls.OfType<RadioButton>())
            {
                if (radio.Checked == true)
                {
                    radio.Checked = false;
                    break;
                }
            }
        }
    }
}
