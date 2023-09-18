using System;
using System.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Folder_Structer_Handler__FSH_;

namespace Folder_Structer_Handler__FSH_
{
    public partial class fsh_main : Form
    {

        public string args_pth;
        public Restruct _restruct;
        public Destruct _destruct;
        public bool progress_ena = false;
        public string[] file_type = null;
        public string[] file_containing = null;
        private string dataPth = Path.Combine(Application.StartupPath, "settings.json");
        private string checksideprogram = Path.Combine(Application.StartupPath, "FSH_filter.exe");

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

        public fsh_main(string[] args)
        {
            InitializeComponent();
            designSet();
            this.Show();

            //check_exist 
            if (!File.Exists(checksideprogram))
            {
                MessageBox.Show("Error: 'FSH_filter.exe' not found. The application will terminate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }

            Task.Run(() =>
            {
                bool args_ex = HandleArgs(args);

                //Determine args status

                if (!args_ex)
                {
                    args_pth = args[0];
                    string arg_type = ArgsType(args_pth);

                    switch (arg_type)
                    {
                        case "folder":
                            PathCheck();
                            break;
                        case "extension":
                            int linecount = dataColl(args_pth);
                            restruction(linecount);
                            break;
                    }
                }
                else if (args_ex)
                {
                    MessageBox.Show("No folder or .struct file selected for destructure or restructure subfolders!", "FSH - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            });
        }

        public void designSet()
        {
            Color sysgray = Color.FromArgb(25, 25, 25);
            Color compgray = Color.FromArgb(51, 51, 51);

            this.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + @"\assets\fsh_logo.ico");
            this.Text = "Folder Structure Handler - v0.0.1";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.BackColor = sysgray;
            var preference = Convert.ToInt32(true);
            DwmSetWindowAttribute(this.Handle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref preference, sizeof(uint));

            //Set components

            Prog_line.Cursor = new Cursor(AppDomain.CurrentDomain.BaseDirectory + @"\assets\cursor_scroll.ico");
            Prog_line.BackColor = sysgray;
            Prog_line.ForeColor = Color.White;
            cre_by.ForeColor = compgray;
            Prog_tit.ForeColor = Color.White;
            hideout_p.BackColor = sysgray;
            finish_l.ForeColor = Color.White;
        }
        private bool HandleArgs(string[] args)
        {
            //Handle args exist

            bool args_exist = false;
            if (args.Length > 0)
            {
                args_exist = false;
            }
            else
            {
                args_exist = true;
            }
            return args_exist;
        }

        private string ArgsType(string arg_pth)
        {
            string arg_type = Path.GetExtension(arg_pth);

            if (arg_type == ".struct")
            {
                arg_type = "extension";
            }
            else
            {
                arg_type = "folder";
            }
            return arg_type;
        }

        public void PathCheck()
        {
            string[] getSubFolders = Directory.GetDirectories(args_pth);

            if (getSubFolders.Length == 0)
            {
                MessageBox.Show($"There is no subfolders in {args_pth} to destructure!", "FSH - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (getSubFolders.Length > 0)
            {
                string[] getSubFiles = Directory.GetFiles(args_pth, "*", SearchOption.AllDirectories);

                if (getSubFiles.Length > 0)
                {
                    destruction(getSubFiles);
                }
                else if (getSubFiles.Length == 0)
                {
                    MessageBox.Show("Subfolder(s) not containing file(s)!", "FSH - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
        }

        public int dataColl(string struct_file_pth)
        {
            int linecount = 0;
            string curr_line;
            TextReader readlines = new StreamReader(struct_file_pth);
            while ((curr_line = readlines.ReadLine()) != null)
            {
                linecount++;
            }
            readlines.Close();

            if (linecount == 0)
            {
                MessageBox.Show($"'{struct_file_pth}': .struct file is corrupted!", "FSH - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            return linecount;
        }

        public int FilesCheck(string[] subfolders)
        {
            int countfiles = 0;

            foreach (string subfolder in subfolders)
            {
                string[] getSubFileCount = Directory.GetFiles(subfolder);

                foreach (string subfile in getSubFileCount)
                {
                    countfiles++;
                }
            }
            return countfiles;
        }

        public void destruction(string[] filecount)
        {
            this.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + @"\assets\destruct_logo.ico");

            Prog_tit.Text = "Destructure from: " + args_pth;

            _destruct = new Destruct(this);
            _destruct.entry_folder = args_pth;
            _destruct.destruct_folder = args_pth + "_(Destructured)";
            _destruct.FolderFile = filecount;
            _destruct.FileCount = filecount.Length;
            _destruct.destruct_pth_ex();

            //new destructed path is like args_pth but with a plus + _(Destructed) sign like that: args_pth = C://random/random/thatsthelast in the creatory version is like C://random/random/thatsthelast_(Destructed)
        }

        public void restruction(int linecount)
        {
            this.Icon = new Icon(AppDomain.CurrentDomain.BaseDirectory + @"\assets\restruct_logo.ico");
            string get_struct = Path.GetFileName(args_pth);
            Prog_tit.Text = "Restructure from: " + get_struct;

            _restruct = new Restruct(this);
            _restruct.entry_folder = args_pth;
            _restruct.LineCount = linecount;
            _restruct.restruct();
        }

        public void Create_newline(string newline)
        {
            Prog_line.AppendText(newline + Environment.NewLine);
        }

        public void Updt_Prog_percent(int current_percent)
        {
            Prog_percent.Value = current_percent;
            this.Text = $"FSH - Finished: {current_percent}%";
        }

        public string Finish_l
        {
            get { return finish_l.Text; }
            set { finish_l.Text = "Finished in: " + value; }
        }

        private void ok_bt_Click(object sender, EventArgs e)
        {
            if (!progress_ena)
            {
                Environment.Exit(0);
            }
        }
    }

    public class Destruct
    {
        //Time fields
        Stopwatch proctime = new Stopwatch();
        TimeSpan procelapse;
        private string sectime;

        public string entry_folder;
        public string destruct_folder;
        public string[] FolderFile;
        public int FileCount;

        private fsh_main Set_entry;
        private bool File_type_filter = false;
        private bool File_contain_filter = false;
        private string[] contain_holder = null;
        private string[] file_types_holder = null;
        private string file_type_disp = null;
        private string file_contain_disp = null;
        private List<string> prevaliddata = new List<string>();
        int dynamic_signer = 0;
        int Proc_count = 0;

        public Destruct(fsh_main set_entry)
        {
            this.Set_entry = set_entry;

            contain_holder = Set_entry.file_containing;
            file_types_holder = Set_entry.file_type;

            if (contain_holder.Length != 0)
            {
                File_contain_filter = true;
                int step = 0;

                foreach (string line_type in contain_holder)
                {
                    if (step == 0)
                    {
                        file_contain_disp += line_type;
                    }
                    else if (step > 0)
                    {
                        file_contain_disp += ", " + line_type;
                    }
                    step++;
                }
            }
            if (file_types_holder.Length != 0)
            {
                File_type_filter = true;

                int step = 0;

                foreach (string line_type in file_types_holder)
                {
                    if (step == 0)
                    {
                        file_type_disp += line_type;
                    }
                    else if (step > 0)
                    {
                        file_type_disp += ", " + line_type;
                    }
                    step++;
                }
            }
        }

        public void destruct_pth_ex()
        {
            List<int> sub_collect = new List<int>();
            string env_folder = entry_folder.Remove(entry_folder.LastIndexOf('\\'));

            //Set new destruct folder 
            if (Directory.Exists(destruct_folder))
            {
                string[] env_subs = Directory.GetDirectories(env_folder);

                foreach (string env_sub in env_subs)
                {
                    string env_sub_name = Path.GetFileName(env_sub);

                    if (env_sub_name.Contains("(Destructured)"))
                    {
                        string env_sub_root = env_sub_name.Remove(env_sub_name.LastIndexOf('_'));
                        int env_sub_count = env_sub_root.Length + 1;
                        string env_sub_sign = env_sub_name.Substring(env_sub_count);

                        if (env_sub_sign.All(char.IsDigit))
                        {
                            int checklaststring = int.Parse(env_sub_sign);
                            sub_collect.Add(checklaststring);
                        }
                    }
                }

                if (!sub_collect.Any())
                {
                    destruct_folder += "_1";
                }
                else
                {
                    int updt_num = sub_collect.Max() + 1;
                    destruct_folder = destruct_folder + "_" + updt_num.ToString();
                }
            }

            //Create destruct folder
            Directory.CreateDirectory(destruct_folder);

            createstruct(destruct_folder);
            filesmanage(destruct_folder);
        }

        public bool createstruct(string destruct_pth)
        {
            string struct_pth = destruct_pth + "\\resigner.struct";
            bool struct_ex = false;

            try
            {
                using (FileStream new_struct = File.Create(struct_pth))
                {
                    struct_ex = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured when .struct is about to restruct!" + ex.Message);
                Environment.Exit(0);
            }
            return struct_ex;
        }

        public void filesmanage(string destruct_pth)
        {
            Set_entry.progress_ena = true;
            prevaliddata.Clear();
            dynamic_signer = 0;
            double allsize = 0;
            string log_struct_pth = destruct_pth + "\\resigner.struct";
            string[] file_type = Set_entry.file_type;
            string[] file_containing = Set_entry.file_containing;

            proctime.Start();
            foreach (string subfile in FolderFile)
            {
                dynamic_signer++;
                bool get_hitted_pre = false;
                bool get_hitted_out = false;

                string env_folder = entry_folder.Remove(entry_folder.LastIndexOf('\\')) + "\\";
                string subfile_folder = subfile.Remove(subfile.LastIndexOf('\\'));
                string inner_pth = subfile_folder.Replace(env_folder, "");

                string file_name = Path.GetFileName(subfile);
                string raw_name = Path.GetFileNameWithoutExtension(subfile);
                string file_ext = Path.GetExtension(subfile);
                string file_path = Path.Combine(destruct_pth, file_name);


                if (prevaliddata.Contains(file_path))
                {
                    string dynamic_file_name = raw_name + "_(" + dynamic_signer.ToString() + ")" + file_ext;
                    string dynamic_file_path = Path.Combine(destruct_pth, dynamic_file_name);

                    if (!File_type_filter && !File_contain_filter)
                    {
                        File.Copy(subfile, dynamic_file_path);

                        using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                        {
                            log_innit.WriteLine($"{inner_pth};{file_name};{dynamic_file_name}");
                        }
                        Proc_count++;
                        get_hitted_pre = true;
                    }
                    else if (File_type_filter && File_contain_filter)
                    {
                        if (file_type.Contains(file_ext))
                        {
                            int hit_check = 0;
                            foreach (string file_exam in file_containing)
                            {
                                if (raw_name.Contains(file_exam))
                                {
                                    hit_check++;
                                }
                            }
                            if (hit_check > 0)
                            {
                                File.Copy(subfile, dynamic_file_path);

                                using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                                {
                                    log_innit.WriteLine($"{inner_pth};{file_name};{dynamic_file_name}");
                                }
                                Proc_count++;
                                get_hitted_pre = true;
                            }
                        }
                    }
                    else if (File_type_filter && !File_contain_filter)
                    {
                        if (file_type.Contains(file_ext))
                        {
                            File.Copy(subfile, dynamic_file_path);

                            using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                            {
                                log_innit.WriteLine($"{inner_pth};{file_name};{dynamic_file_name}");
                            }
                            Proc_count++;
                            get_hitted_pre = true;
                        }
                    }
                    else if (!File_type_filter && File_contain_filter)
                    {
                        int hit_check = 0;
                        foreach (string file_exam in file_containing)
                        {
                            if (raw_name.Contains(file_exam))
                            {
                                hit_check++;
                            }
                        }
                        if (hit_check > 0)
                        {
                            File.Copy(subfile, dynamic_file_path);

                            using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                            {
                                log_innit.WriteLine($"{inner_pth};{file_name};{dynamic_file_name}");
                            }
                            Proc_count++;
                            get_hitted_pre = true;
                        }
                    }

                    if (get_hitted_pre && dynamic_signer == FileCount)
                    {
                        double length = new System.IO.FileInfo(dynamic_file_path).Length;
                        double lengthinbyte = length / 1024;
                        double lengthinmegabyte = lengthinbyte / 1024;
                        double roundmegabyte = Math.Round(lengthinmegabyte, 2);
                        allsize += lengthinmegabyte;
                        string output_inf = $"File '{inner_pth + "\\" + dynamic_file_name}' --> {dynamic_file_path} -- Size: {roundmegabyte} MB";

                        Set_entry.Create_newline("");
                        Set_entry.Create_newline(output_inf);
                        break;
                    }
                    else if (get_hitted_pre)
                    {
                        double length = new System.IO.FileInfo(dynamic_file_path).Length;
                        double lengthinbyte = length / 1024;
                        double lengthinmegabyte = lengthinbyte / 1024;
                        double roundmegabyte = Math.Round(lengthinmegabyte, 2);
                        allsize += lengthinmegabyte;
                        string output_inf = $"File '{inner_pth + "\\" + dynamic_file_name}' --> {dynamic_file_path} -- Size: {roundmegabyte} MB";

                        //Send update value
                        int prog_value = (int)((double)Proc_count / FileCount * 100);
                        Set_entry.Updt_Prog_percent(prog_value);

                        Set_entry.Create_newline("");
                        Set_entry.Create_newline(output_inf);
                    }
                }
                else
                {

                    if (!File_type_filter && !File_contain_filter)
                    {
                        File.Copy(subfile, file_path);

                        using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                        {
                            log_innit.WriteLine($"{inner_pth};{file_name};{file_name}");
                        }
                        Proc_count++;
                        get_hitted_out = true;
                    }
                    else if (File_type_filter && File_contain_filter)
                    {
                        if (file_type.Contains(file_ext))
                        {
                            int hit_check = 0;
                            foreach (string file_exam in file_containing)
                            {
                                MessageBox.Show(file_exam);
                                if (raw_name.Contains(file_exam))
                                {
                                    hit_check++;
                                }
                            }
                            if (hit_check > 0)
                            {
                                File.Copy(subfile, file_path);

                                using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                                {
                                    log_innit.WriteLine($"{inner_pth};{file_name};{file_name}");
                                }
                                Proc_count++;
                                get_hitted_out = true;
                            }
                        }
                    }
                    else if (File_type_filter && !File_contain_filter)
                    {
                        if (file_type.Contains(file_ext))
                        {
                            File.Copy(subfile, file_path);

                            using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                            {
                                log_innit.WriteLine($"{inner_pth};{file_name};{file_name}");
                            }
                            Proc_count++;
                            get_hitted_out = true;
                        }
                    }
                    else if (!File_type_filter && File_contain_filter)
                    {
                        int hit_check = 0;
                        foreach (string file_exam in file_containing)
                        {
                            if (raw_name.Contains(file_exam))
                            {
                                hit_check++;
                            }
                        }
                        if (hit_check > 0)
                        {
                            File.Copy(subfile, file_path);

                            using (StreamWriter log_innit = new StreamWriter(log_struct_pth, true))
                            {
                                log_innit.WriteLine($"{inner_pth};{file_name};{file_name}");
                            }
                            Proc_count++;
                            get_hitted_out = true;
                        }
                    }

                    if (get_hitted_out && dynamic_signer == FileCount)
                    {
                        double length = new System.IO.FileInfo(file_path).Length;
                        double lengthinbyte = length / 1024;
                        double lengthinmegabyte = lengthinbyte / 1024;
                        double roundmegabyte = Math.Round(lengthinmegabyte, 2);
                        allsize += lengthinmegabyte;
                        string output_inf = $"File '{inner_pth + "\\" + file_name}' --> {file_path} -- Size: {roundmegabyte} MB";

                        Set_entry.Create_newline("");
                        Set_entry.Create_newline(output_inf);
                        Set_entry.progress_ena = false;
                        break;
                    }
                    else if (get_hitted_out)
                    {
                        double length = new System.IO.FileInfo(file_path).Length;
                        double lengthinbyte = length / 1024;
                        double lengthinmegabyte = lengthinbyte / 1024;
                        double roundmegabyte = Math.Round(lengthinmegabyte, 2);
                        allsize += lengthinmegabyte;
                        string output_inf = $"File '{inner_pth + "\\" + file_name}' --> {file_path} -- Size: {roundmegabyte} MB";

                        //Send update value
                        int prog_value = (int)((double)Proc_count / FileCount * 100);
                        Set_entry.Updt_Prog_percent(prog_value);

                        Set_entry.Create_newline("");
                        Set_entry.Create_newline(output_inf);
                        Set_entry.progress_ena = false;
                    }
                }
                if (get_hitted_out)
                {
                    prevaliddata.Add(file_path);
                }
            }
            proctime.Stop();
            procelapse = proctime.Elapsed;
            sectime = procelapse.ToString(@"s\.fffffff") + "s";
            Set_entry.Finish_l = sectime;

            double allsize_formated = Math.Round(allsize, 2);
            string output_size = $"{allsize_formated} MB data destructured";
            Set_entry.Create_newline("");
            Set_entry.Create_newline(output_size + $" -- {Proc_count} File handled");

            //Send update value
            Set_entry.Updt_Prog_percent(100);

            //Disp file filtering inf 
            string file_filter_check = $"{File_type_filter.ToString()}-{File_contain_filter.ToString()}";

            switch (file_filter_check)
            {
                case "True-True":
                    Set_entry.Create_newline($"File filtering is ON: File type filtering: '{file_type_disp}' -- File contain filtering: '{file_contain_disp}'.");
                    break;
                case "False-False":
                    Set_entry.Create_newline($"File filtering is OFF: You can set File type and File contain filtering in 'Folder Structer Handler - Filtering'.");
                    break;
                case "True-False":
                    Set_entry.Create_newline($"File filtering is ON: File type filtering: '{file_type_disp}' -- File contain filtering: 'none'.");
                    break;
                case "False-True":
                    Set_entry.Create_newline($"File filtering is ON: File type filtering: 'none' -- File contain filtering: '{file_contain_disp}'.");
                    break;
            }
            SystemSounds.Beep.Play();
            Set_entry.progress_ena = false;
        }
    }
}

    public class Restruct
{
    //Time fields
    Stopwatch proctime = new Stopwatch();
    TimeSpan procelapse;
    private string sectime;

    private fsh_main Set_entry;
    public string entry_folder;
    public int LineCount;

    int FileCount = 0;
    int FolderCount = 0;

    public Restruct(fsh_main set_entry)
    {
        this.Set_entry = set_entry;
    }

    public void restruct()
    {
        FileCount = 0;
        double allsize = 0;
        Set_entry.progress_ena = true;
        string set_folder = entry_folder.Remove(entry_folder.LastIndexOf('\\'));
        string restruct_folder = set_folder + "\\" + "Restructured";
        List<int> sub_collect = new List<int>();

        if (Directory.Exists(restruct_folder))
        {
            string[] restruct_subs = Directory.GetDirectories(set_folder);

            foreach (string restruct_sub in restruct_subs)
            {
                string restruct_sub_name = Path.GetFileName(restruct_sub);
                if (restruct_sub_name.Contains("Restructured"))
                {
                    string restruct_sub_root = restruct_sub_name.Remove(restruct_sub_name.LastIndexOf('_'));
                    int restruct_sub_count = restruct_sub_root.Length + 1;
                    string restruct_sub_sign = restruct_sub_name.Substring(restruct_sub_count);

                    if (restruct_sub_sign.All(char.IsDigit))
                    {
                        int checklaststring = int.Parse(restruct_sub_sign);
                        sub_collect.Add(checklaststring);
                    }
                }
            }

            if (!sub_collect.Any())
            {
                restruct_folder += "_1";
            }
            else
            {
                int updt_num = sub_collect.Max() + 1;
                restruct_folder = restruct_folder + "_" + updt_num.ToString();
            }
        }

        //Create restruct folder
        Directory.CreateDirectory(restruct_folder);

        string[] mov_files = Directory.GetFiles(set_folder);

        proctime.Start();
        foreach (string file in mov_files)
        {
            string file_name = Path.GetFileName(file);

            using (StreamReader read_lines = new StreamReader(entry_folder))
            {
                string inline;

                while ((inline = read_lines.ReadLine()) != null)
                {
                    string[] datapart = inline.Split(';');

                    if (datapart.Length == 3)
                    {
                        string subfoldername = datapart[0];
                        string originalname = datapart[1];
                        string actualname = datapart[2];

                        if (file_name == actualname)
                        {
                            FileCount++;
                            string dynamic_folder = restruct_folder + "\\" + subfoldername;
                            string full_pth = restruct_folder + "\\" + subfoldername + "\\" + originalname;

                            if (!Directory.Exists(dynamic_folder))
                            {
                                Directory.CreateDirectory(dynamic_folder);
                                FolderCount++;
                            }

                            File.Copy(file, full_pth);

                            //Send update value
                            int prog_value = (int)((double)FileCount / LineCount * 100);
                            Set_entry.Updt_Prog_percent(prog_value);

                            double length = new System.IO.FileInfo(full_pth).Length;
                            double lengthinbyte = length / 1024;
                            double lengthinmegabyte = lengthinbyte / 1024;
                            double roundmegabyte = Math.Round(lengthinmegabyte, 2);
                            allsize += lengthinmegabyte;

                            string output_inf = $"File '{file_name}' --> {full_pth} -- Size: {roundmegabyte} MB";
                            Set_entry.Create_newline("");
                            Set_entry.Create_newline(output_inf);
                        }
                    }
                }
            }
        }
        proctime.Stop();
        procelapse = proctime.Elapsed;
        sectime = procelapse.ToString(@"s\.fffffff") + "s";
        Set_entry.Finish_l = sectime;

        double allsize_formated = Math.Round(allsize, 2);
        string output_size = $"{allsize_formated} MB data restructured";
        int check_missed = LineCount - FileCount;
        Set_entry.Create_newline("");
        Set_entry.Create_newline(output_size + $" -- {FolderCount} Different path created -- {FileCount} File handled -- {check_missed} File missed");
        SystemSounds.Beep.Play();
        Set_entry.progress_ena = false;
    }
}