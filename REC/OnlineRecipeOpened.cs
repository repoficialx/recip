using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace recip
{
    public partial class OnlineRecipeOpened : Form
    {
        public static string RecipeName;
        public OnlineRecipeOpened()
        {
            InitializeComponent();
            Text = Strings.StatusLoading;
            textBox1.Text = Strings.StatusLoading;
            textBox2.Text = Strings.StatusLoading;
        }
        public class Receta
        {
            public string nombre { get; set; }
            public string[] ingredientes { get; set; }
            public string[] instrucciones { get; set; }
        }

        public async Task<List<Receta>> GetRecetasOnline()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync("https://repoficialx.xyz/recip/recetas.json");
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<Receta>>>(json);
            return data["recetas"];
        }
        private void RecipeOpened_Load(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            button2.Hide();
            
            string filePath = RecipeName ?? "";

            GetRecetasOnline().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MessageBox.Show(Strings.ErrorRetrievingRecipes + ": " + task.Exception.Message);
                    return;
                }
                var recetas = task.Result;
                foreach (var receta in recetas)
                {
                    if (filePath == receta.nombre)
                    {
                        filePath = receta.nombre;
                        textBox1.Text = string.Join("\r\n", receta.ingredientes);
                        textBox2.Text = string.Join("\r\n", receta.instrucciones);
                        Text = receta.nombre;
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

            
            //Label1: Ingredients:
            //Label2: Steps:
            //Button1: <Back
            //Button2: Close
            label1.Text = Strings.LabelIngredients;
            label2.Text = Strings.LabelSteps;
            button1.Text = Strings.BtnBack;
            button2.Text = Strings.BtnClose;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
