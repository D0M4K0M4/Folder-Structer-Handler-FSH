using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FSH_filter
{
    public partial class fsh_filter : Form
    {
        private string[] file_type = null;
        private string[] file_containing = null;
        private string dataPth = "settings.json";
        private string checksideprogram = "Folder Structer Handler (FSH).exe";

        //Dll imp

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref int pvAttribute, uint cbAttribute);

        public enum DWMWINDOWATTRIBUTE : uint
        {
            DWMWA_NCRENDERING_ENABLED,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_PASSIVE_UPDATE_MODE,
            DWMWA_USE_HOSTBACKDROPBRUSH,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR,
            DWMWA_CAPTION_COLOR,
            DWMWA_TEXT_COLOR,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            DWMWA_SYSTEMBACKDROP_TYPE,
            DWMWA_LAST
        }

        public fsh_filter()
        {
            InitializeComponent();
            designSet();
            this.Show();

            //check_exist 
              if (!File.Exists(checksideprogram))
              {
                  MessageBox.Show("Error: 'Folder Structer Handler (FSH).exe' not found. The application will terminate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  Environment.Exit(0); // Az alkalmazás leállítása
              }
            if (!File.Exists(dataPth))
            {
                MessageBox.Show("Error: 'settings.json' not found. The application will terminate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0); // Az alkalmazás leállítása
            }
            else
            {
                string json_entry = File.ReadAllText(dataPth);
                dynamic settings = JsonConvert.DeserializeObject(json_entry);

                // Elolvassa a JSON tömböket JArray objektumokként
                JArray fileTypesArray = (JArray)settings["grab_file_types"];
                JArray fileContainingArray = (JArray)settings["grab_file_containing"];

                // Konvertálja a JArray objektumokat string tömbbé
                file_type = fileTypesArray.Select(item => (string)item).ToArray();
                file_containing = fileContainingArray.Select(item => (string)item).ToArray();
                dsp_ext(file_type);
                dsp_cont(file_containing);
            }
        }

        public void designSet()
        {
            Color sysgray = Color.FromArgb(25, 25, 25);
            Color compgray = Color.FromArgb(51, 51, 51);

            this.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + @"\assets\fsh_filter.ico");
            this.Text = "Folder Structure Handler - Filtering";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = sysgray;
            var preference = Convert.ToInt32(true);
            DwmSetWindowAttribute(this.Handle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref preference, sizeof(uint));

            //Set components
            file_type_bx.Cursor = new Cursor(AppDomain.CurrentDomain.BaseDirectory + @"\assets\cursor_scroll.ico");
            file_cont_bx.Cursor = new Cursor(AppDomain.CurrentDomain.BaseDirectory + @"\assets\cursor_scroll.ico");
            hide_p.BackColor = sysgray;
            file_type_bx.BackColor = sysgray;
            file_cont_bx.BackColor = sysgray;
            file_type_bx.ForeColor = Color.Green;
            file_cont_bx.ForeColor = Color.Green;
            file_type_tb.BackColor = compgray;
            file_contain_tb.BackColor = compgray;
            file_type_tb.ForeColor = Color.White;
            file_contain_tb.ForeColor = Color.White;
            cre_by.ForeColor = compgray;
            file_type_l.ForeColor = Color.White;
            file_contain_l.ForeColor = Color.White;
            inf_type_l.ForeColor = Color.White;
            inf_contain_l.ForeColor = Color.White;
        }

        public void dsp_ext(string[] raw_type)
        {
            file_type_bx.AppendText("Applied type:" + Environment.NewLine);
            int step = 0;

            if (raw_type.Length == 0)
            {
                file_type_bx.AppendText("'None'");
            }
            else
            {
                foreach (string type in raw_type)
                {
                    if (step == 0)
                    {
                        file_type_bx.AppendText(type);
                    }
                    else
                    {
                        file_type_bx.AppendText(", " + type);
                    }
                    step++;
                }
            }
        }

        public void dsp_cont(string[] raw_cont)
        {
            file_cont_bx.AppendText("Applied comp:" + Environment.NewLine);
            int step = 0;

            if (raw_cont.Length == 0)
            {
                file_cont_bx.AppendText("'None'");
            }
            else
            {
                foreach (string type in raw_cont)
                {
                    if (step == 0)
                    {
                        file_cont_bx.AppendText(type);
                    }
                    else
                    {
                        file_cont_bx.AppendText(", " + type);
                    }
                    step++;
                }
            }
        }

        private void file_type_bt_Click(object sender, EventArgs e)
        {
            string json_type = File.ReadAllText(dataPth);
            dynamic settings_type = JsonConvert.DeserializeObject(json_type);
            JArray fileTypes = (JArray)settings_type["grab_file_types"];

            string file_type_value = file_type_tb.Text;
            string[] file_ext_value = file_type_value.Split(',');

            if (file_type_value.Length == 0)
            {
                fileTypes.Clear();
                string updatedJson = JsonConvert.SerializeObject(settings_type, Formatting.Indented);
                File.WriteAllText(dataPth, updatedJson);
                file_type_bx.Text = null;
                file_type_bx.AppendText("Applied type:" + Environment.NewLine);
                file_type_bx.AppendText("'None'");
                inf_type_l.Text = "File type extension: 'none' applied...";
                inf_type_l.ForeColor = Color.Green;
            }
            else if (file_type_value.Length > 0)
            {
                string file_types = null;
                int step = 0;
                bool hasError = false;
                fileTypes.Clear();
                foreach (string ext_value in file_ext_value)
                {
                    string ext_type = ext_value.Trim();

                    if (!ext_type.Contains('.') || ext_type.Length == 0)
                    {
                        hasError = true;
                    }

                    fileTypes.Add(ext_type);

                    if (step == 0)
                    {
                        file_types += ext_type;
                    }
                    else
                    {
                        file_types += ", " + ext_type;
                    }
                    step++;
                }
                if (hasError)
                {
                    file_type_bx.Text = null;
                    file_type_bx.AppendText("Applied type:" + Environment.NewLine);
                    file_type_bx.AppendText("'None'");
                    inf_type_l.Text = "Some of the contains are empty or the extension(s) doesn't contain '.'!";
                    inf_type_l.ForeColor = Color.Red;
                    file_type_tb.Text = null;
                }
                else if (!hasError)
                {
                    file_type_bx.Text = null;
                    file_type_bx.AppendText("Applied type:" + Environment.NewLine);
                    file_type_bx.AppendText(file_types + Environment.NewLine);
                    string updatedJson = JsonConvert.SerializeObject(settings_type, Formatting.Indented);
                    File.WriteAllText(dataPth, updatedJson);
                    inf_type_l.Text = $"File type extension: '{file_types}' applied...";
                    inf_type_l.ForeColor = Color.Green;
                    file_type_tb.Text = null;
                }
            }
        }

        private void file_contain_bt_Click(object sender, EventArgs e)
        {
            string json_contain = File.ReadAllText(dataPth);
            dynamic settings_cont = JsonConvert.DeserializeObject(json_contain);
            JArray fileCont = (JArray)settings_cont["grab_file_containing"];

            string file_contain_value = file_contain_tb.Text;
            string[] file_comp_value = file_contain_value.Split(',');

            if (file_contain_value.Length == 0)
            {
                fileCont.Clear();
                string updatedJson = JsonConvert.SerializeObject(settings_cont, Formatting.Indented);
                File.WriteAllText(dataPth, updatedJson);
                file_cont_bx.Text = null;
                file_cont_bx.AppendText("Applied type:" + Environment.NewLine);
                file_cont_bx.AppendText("'None'");
                inf_contain_l.Text = "File contain(s): 'none' applied...";
                inf_contain_l.ForeColor = Color.Green;
            }
            else if (file_contain_value.Length > 0)
            {
                string file_comps = null;
                int step = 0;
                bool hasError = false;
                fileCont.Clear();
                foreach (string comp_value in file_comp_value)
                {
                    string comp_type = comp_value.Trim();

                    if (comp_type.Length == 0)
                    {
                        hasError = true;
                    }

                    fileCont.Add(comp_type);

                    if (step == 0)
                    {
                        file_comps += comp_type;
                    }
                    else
                    {
                        file_comps += ", " + comp_type;
                    }
                    step++;
                }
                if (hasError)
                {
                    file_cont_bx.Text = null;
                    file_cont_bx.AppendText("Applied type:" + Environment.NewLine);
                    file_cont_bx.AppendText("'None'");
                    inf_contain_l.Text = "Some of the contain(s) are empty!";
                    inf_contain_l.ForeColor = Color.Red;
                    file_contain_tb.Text = null;
                }
                else if (!hasError)
                {
                    file_cont_bx.Text = null;
                    file_cont_bx.AppendText("Applied type:" + Environment.NewLine);
                    file_cont_bx.AppendText(file_comps + Environment.NewLine);
                    string updatedJson = JsonConvert.SerializeObject(settings_cont, Formatting.Indented);
                    File.WriteAllText(dataPth, updatedJson);
                    inf_contain_l.Text = $"File contain(s): '{file_comps}' applied...";
                    inf_contain_l.ForeColor = Color.Green;
                    file_contain_tb.Text = null;
                }
            }
        }

        private void ok_bt_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void file_type_bx_TextChanged(object sender, EventArgs e)
        {

        }

        private void file_cont_bx_TextChanged(object sender, EventArgs e)
        {

        }
    }
}