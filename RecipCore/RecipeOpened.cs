using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RecipCore
{
    public partial class RecipeOpened : Form
    {
        public static string? RecipeRute;
        public static bool OpenedByFile;
        public RecipeOpened()
        {
            InitializeComponent();
            button3.Text = Strings.BtnPublish;
        }

        private void RecipeOpened_Load(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            if (OpenedByFile != true)
            {
                button2.Hide();
            }
            else
            {
                button1.Hide();
            }
            string filePath = RecipeRute ?? "";
            string sectionName = "Ingredients";
            List<string> sectionLines = RecipApp.ReadSection(filePath, sectionName);

            foreach (string line in sectionLines)
            {
                if (string.IsNullOrEmpty(textBox1.Text)) // Verifica si el TextBox está vacío
                {
                    textBox1.Text = line;
                }
                else
                {
                    textBox1.Text += "\r\n" + line; // Agrega un salto de línea solo si ya hay texto
                }
            }

            sectionName = "Steps";
            sectionLines = RecipApp.ReadSection(filePath, sectionName);

            foreach (string line in sectionLines)
            {
                if (string.IsNullOrEmpty(textBox2.Text)) // Verifica si el TextBox está vacío
                {
                    textBox2.Text = line;
                }
                else
                {
                    textBox2.Text += "\r\n" + line; // Agrega un salto de línea solo si ya hay texto
                }
            }


            sectionName = "RecipeName";
            sectionLines = RecipApp.ReadSection(filePath, sectionName);

            foreach (string line in sectionLines)
            {
                Text = line;
            }
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

        private async void button3_Click(object sender, EventArgs e)
        {
            // Guardamos el texto original
            string originalText = button3.Text;

            try
            {
                // Cambiamos a estado "loading"
                button3.Enabled = false;
                button3.Text = Strings.StatusPublishing;

                // Llamada al método async
                await PublicarReceta(
                Text,
                textBox1.Lines,
                textBox2.Lines);
                MessageBox.Show(Strings.PublishSuccess);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Strings.PublishError + " " + ex.Message);
            }
            finally
            {
                // Restauramos el botón pase lo que pase
                button3.Enabled = true;
                button3.Text = originalText;
            }
        }

        public async Task PublicarReceta(string nombre, string[] ingredientes, string[] instrucciones)
        {
            using var client = new HttpClient();
            var values = new Dictionary<string, string>
    {
        { "nombre", nombre },
        { "ingredientes", JsonConvert.SerializeObject(ingredientes) },
        { "instrucciones", JsonConvert.SerializeObject(instrucciones) }
    };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://repoficialx.xyz/recip/uploadRecipe.php", content);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
