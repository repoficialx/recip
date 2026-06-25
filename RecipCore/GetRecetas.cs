using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Contacts;

namespace RecipCore
{
    public partial class GetRecetas : Form
    {
        public GetRecetas()
        {
            InitializeComponent();
            button1.Text = Strings.BtnOpen;
            listBox1.Items.Clear();
            listBox1.Items.Add(Strings.StatusLoading);
        }

        public class Receta
        {
            public string nombre { get; set; }
            public string[] ingredientes { get; set; }
            public string[] instrucciones { get; set; }
        }

        public async Task<List<RecetaV2>> GetRecetasOnline()
        {
            using var client = new HttpClient();
            var json = await client.GetStringAsync("https://repoficialx.xyz/recip/recetas.json");
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<RecetaV2>>>(json);
            return data["recetas"];
        }

        private void GetRecetas_Load(object sender, EventArgs e)
        {
            GetRecetasOnline().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    MessageBox.Show(Strings.ErrorRetrievingRecipes + " " + task.Exception.Message);
                    return;
                }
                var recetas = task.Result;
                listBox1.Items.Clear();
                foreach (var receta in recetas)
                {
                    listBox1.Items.Add(receta.name);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == "Couldn't retrieve recipes.")
            {
                this.Close();
                return;
            }
            OnlineRecipeOpened.RecipeName = listBox1.SelectedItem?.ToString();
            var recetaForm = new OnlineRecipeOpened();
            recetaForm.Show();
            /*this.FormClosing += (s, e) => {
                recetaForm.Focus();
            };
            this.Close();*/
        }
    }
}
