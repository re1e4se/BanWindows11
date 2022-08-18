using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BanWindows11
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CenterLabel(label1);
            CenterLabel(label2);
            CenterLabel(label4);
            CenterLabel(label5);
            CenterLabel(label6);
            CenterLabel(label7);
            CenterCheckBox(checkBox1);
            this.Load += new EventHandler(Main_Load);
        }

        private void Main_Load(object sender, EventArgs e) => Check();

        private void Check()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\WindowsUpdate"))
                {
                    if (key != null)
                    {
                        var ProductVersion = key.GetValue("ProductVersion");
                        var TargetReleaseVersion = key.GetValue("TargetReleaseVersion");
                        var TargetReleaseVersionInfo = key.GetValue("TargetReleaseVersionInfo");

                        if (ProductVersion != null && TargetReleaseVersion != null && TargetReleaseVersionInfo != null)
                            checkBox1.Checked = true;
                        else
                        {
                            checkBox1.Checked = false;
                            MessageBox.Show("Windows 11 Upgrade IS NOT BANNED on your PC. If you want to BAN THE UPDATE, check the Ban Windows 11 Checkbox.", "WINDOWS 11 IS NOT BANNED!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            label8.Text = "Registry Results in a less complicated way: Oh no! You don't have Windows 11 Banned.";
                            return;
                        }
                        label4.Text = "ProductVersion: " + ProductVersion;
                        label5.Text = "TargetReleaseVersion: " + TargetReleaseVersion;
                        label6.Text = "TargetReleaseVersionInfo: " + TargetReleaseVersionInfo;
                        CenterLabel(label4);
                        CenterLabel(label5);
                        CenterLabel(label6);
                        CenterLabel(label7);
                        label8.Text = $"Registry Results in a less complicated way: You will continue receiving feature updates until the {ProductVersion} {TargetReleaseVersionInfo}, \nafter that you will continue receiving Security Updates for that version. When a new Windows version gets \nreleased, the program will update to get latest feature updates like Windows 10 22H2.";
                        return;
                    }
                    checkBox1.Checked = false;
                    MessageBox.Show("Windows 11 Upgrade IS NOT BANNED on your PC. If you want to BAN THE UPDATE, check the Ban Windows 11 Checkbox.", "WINDOWS 11 IS NOT BANNED!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    label8.Text = "Registry Results in a less complicated way: Oh no! You don't have Windows 11 Banned.";
                }
            }
            catch
            {
                checkBox1.Checked = false;
                MessageBox.Show("Windows 11 Upgrade IS NOT BANNED on your PC. If you want to BAN THE UPDATE, check the Ban Windows 11 Checkbox.", "WINDOWS 11 IS NOT BANNED!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                label8.Text = "Registry Results in a less complicated way: Oh no! You don't have Windows 11 Banned.";
            }
        }

        private void CenterLabel(Label lbl) => lbl.Location = new Point((this.Size.Width - lbl.Size.Width) / 2, lbl.Location.Y);

        private void CenterCheckBox(CheckBox checkBox) => checkBox.Location = new Point((this.Size.Width - checkBox.Size.Width) / 2, checkBox.Location.Y);

        private void label9_Click(object sender, EventArgs e) => Application.Exit();

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                DialogResult Prompt = MessageBox.Show("Are you sure you want to enable Windows 11?", "Enable Windows 11", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Prompt == DialogResult.Yes)
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\WindowsUpdate", true))
                    {
                        key.DeleteValue("ProductVersion");
                        key.DeleteValue("TargetReleaseVersion");
                        key.DeleteValue("TargetReleaseVersionInfo");
                    }
                    MessageBox.Show("Windows 11 Enabled, you made a big mistake...", "Enabled Windows 11", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Check();
                    return;
                }
                checkBox1.Checked = true;
            }
            else if (checkBox1.Checked == true)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\WindowsUpdate", true))
                {
                    key.SetValue("ProductVersion", "Windows 10");
                    key.SetValue("TargetReleaseVersion", 1);
                    key.SetValue("TargetReleaseVersionInfo", "21H2");
                }
                MessageBox.Show("Windows 11 Disabled, you are safe now.", "Disabled Windows 11", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Check();
            }
        }
    }
}
