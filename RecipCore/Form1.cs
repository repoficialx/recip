using RecipCore.Properties;
using System.Windows.Forms;

namespace RecipCore
{
    public partial class Form1 : Form
    {
        /*
         * ENUM: COLORNEW: Color (nuevo)
         * COLORRESCNEW: Color (reescalado)
         * COLORMONONEW: Color (monocromático)
         * COLORBNNEW: Color (B/N)
         */
        public enum LogoOpt
        {
            COLORNEW,
            COLORRESCNEW,
            COLORMONONEW,
            COLORBNNEW
        }
        public static LogoOpt selLogo;
        public Form1()
        {
            InitializeComponent();
            // Obtener el logo desde la configuración
            string logo = Settings.Default.SavedLogo;
            if (Enum.TryParse(logo, out LogoOpt miOpcion))
            {
                switch (miOpcion)
                {
                    case LogoOpt.COLORNEW:
                        // Asume que tienes un byte[] llamado iconBytes
                        using (MemoryStream ms = new MemoryStream(Resources.recipiconnbnew))
                        {
                            this.Icon = new Icon(ms);
                        }
                        pictureBox1.Image = Resources.recip_new;
                        comboBox1.Text = Strings.ComboColor;
                        break;
                    case LogoOpt.COLORRESCNEW:
                        using (MemoryStream ms = new MemoryStream(Resources.recipiconrescnew))
                        {
                            this.Icon = new Icon(ms);
                        }
                        pictureBox1.Image = Resources.reciplogoresc;
                        comboBox1.Text = Strings.ComboColorRescaled;
                        break;
                    case LogoOpt.COLORMONONEW:
                        using (MemoryStream ms = new MemoryStream(Resources.recipiconmononew))
                        {
                            this.Icon = new Icon(ms);
                        }
                        pictureBox1.Image = Resources.reciplogomono;
                        comboBox1.Text = Strings.ComboMonochromatic;
                        break;
                    case LogoOpt.COLORBNNEW:
                        using (MemoryStream ms = new MemoryStream(Resources.recipiconbnnew))
                        {
                            this.Icon = new Icon(ms);
                        }
                        pictureBox1.Image = Resources.reciplogobn;
                        comboBox1.Text = Strings.ComboBW;
                        break;
                }
            }
            else
            {
                MessageBox.Show("ERROR 0x0001: Logo not found", "RECIP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Text = Settings.Default.LastRecipe;
            button4.Text = Settings.Default.penLastRecipe;
            fileToolStripMenuItem.Text = Strings.MenuFile;
            newToolStripMenuItem.Text = Strings.MenuFileNew;
            openToolStripMenuItem.Text = Strings.MenuFileOpen;
            helpToolStripMenuItem.Text = Strings.MenuHelp;
            aboutRecipToolStripMenuItem.Text = Strings.MenuHelpAbout;
            button1.Text = Strings.BtnOpen;
            button2.Text = Strings.BtnMake;
            groupBox2.Text = Strings.GroupAppSettings;
            comboBox1.Items.Clear();
            comboBox1.Items.Add(Strings.ComboBW);
            comboBox1.Items.Add(Strings.ComboColor);
            comboBox1.Items.Add(Strings.ComboMonochromatic);
            groupBox1.Text = Strings.GroupLatestRecipes;
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(button7, Strings.ToolTipReset);
            ToolTip restart = new ToolTip();
            restart.SetToolTip(button8, Strings.ToolTipRestart);
            languageToolStripMenuItem.Text = Strings.MenuLanguage;
            toolStripMenuItem2.Text = Strings.MenuLanguageChange;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text.ToUpper())
            {
                case "B&W":
                    Icon = seticon(Resources.recipiconbnnew);
                    pictureBox1.Image = Resources.reciplogobn;
                    Settings.Default.SavedLogo = LogoOpt.COLORBNNEW.ToString();
                    break;
                case "B/N":
                    Icon = seticon(Resources.recipiconbnnew);
                    pictureBox1.Image = Resources.reciplogobn;
                    Settings.Default.SavedLogo = LogoOpt.COLORBNNEW.ToString();
                    break;
                case "COLOR":
                    Icon = seticon(Resources.recipiconnbnew);
                    pictureBox1.Image = Resources.recip_new;
                    Settings.Default.SavedLogo = LogoOpt.COLORNEW.ToString();
                    break;
                case "COLOR (REESCALADO)":
                    Icon = seticon(Resources.recipiconrescnew);
                    pictureBox1.Image = Resources.reciplogoresc;
                    Settings.Default.SavedLogo = LogoOpt.COLORRESCNEW.ToString();
                    break;
                case "COLOR (RESCALED)":
                    Icon = seticon(Resources.recipiconrescnew);
                    pictureBox1.Image = Resources.reciplogoresc;
                    Settings.Default.SavedLogo = LogoOpt.COLORRESCNEW.ToString();
                    break;
                case "MONOCHROMATIC":
                    Icon = seticon(Resources.recipiconmononew);
                    pictureBox1.Image = Resources.reciplogomono;
                    Settings.Default.SavedLogo = LogoOpt.COLORMONONEW.ToString();
                    break;
                case "MONOCROMÁTICO":
                    Icon = seticon(Resources.recipiconmononew);
                    pictureBox1.Image = Resources.reciplogomono;
                    Settings.Default.SavedLogo = LogoOpt.COLORMONONEW.ToString();
                    break;
            }
            Settings.Default.Save();
        }
        Icon seticon(byte[] x)
        {
            using (MemoryStream ms = new MemoryStream(x))
            {
                return new Icon(ms);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "-")
            {
                Size = MinimumSize;
                button8.Text = "+";
            }
            else if (button8.Text == "+")
            {
                Size = this.MaximumSize;
                button8.Text = "-";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string msg = Strings.MsgResetSettings;
            var dr = MessageBox.Show(msg, "RECIP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (dr)
            {
                case DialogResult.Yes:
                    ResetSettings();
                    break;
                case DialogResult.No:
                    break;
            }
        }
        void ResetSettings()
        {
            Settings.Default.Reset();
            Settings.Default.Save();
            MessageBox.Show("Settings reseted", "RECIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new lang().ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Recipes|*.rec|All files|*.*";
                dialog.Title = "Where is the recipe?";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Guardar la ruta del último archivo abierto
                        Settings.Default.penLastRecipe = Settings.Default.LastRecipe;
                        Settings.Default.pLRRoute = Settings.Default.LRRoute;
                        Settings.Default.LastRecipe = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\') + 1);
                        Settings.Default.LRRoute = dialog.FileName;
                        // Aplica los cambios
                        Settings.Default.Save();
                        RecipeOpened.RecipeRute = dialog.FileName;
                        // Abre la receta
                        new RecipeOpened().ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Gestiona el error, mostrando un Msgbox
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Recipes|*.rec|All files|*.*";
                dialog.Title = "Where is the recipe?";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Settings.Default.penLastRecipe = Settings.Default.LastRecipe;
                        Settings.Default.pLRRoute = Settings.Default.LRRoute;
                        Settings.Default.LastRecipe = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\') + 1);
                        Settings.Default.LRRoute = dialog.FileName;
                        Settings.Default.Save();
                        RecipeOpened.RecipeRute = dialog.FileName;
                        new RecipeOpened().ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MakeRecipe().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nlr = Strings.BtnNoLastRecipe;
            string nr = Strings.BtnNoRecipe;
            string nrs = Strings.MsgNoRecipeSelected;
            if (button3.Text == nlr || button3.Text == nr)
            {//NLR: No Last Recipe | NR: No Recipe | NRS: No Recipe Selected
                MessageBox.Show(nrs, "RECIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            RecipeOpened.RecipeRute = Settings.Default.LRRoute;
            new RecipeOpened().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nlr = Strings.BtnNoLastRecipe;
            string nr = Strings.BtnNoRecipe;
            string nrs = Strings.MsgNoRecipeSelected;
            if (button4.Text == nr || button4.Text == nlr)
            {
                MessageBox.Show(nrs, "RECIP", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            RecipeOpened.RecipeRute = Settings.Default.pLRRoute;
            new RecipeOpened().ShowDialog();
        }

        private void aboutRecipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MakeRecipe().ShowDialog();
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GetRecetas().ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            new GetRecetas().ShowDialog();
        }
    }
}
