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
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace SolaireBoard
{
    
    public partial class Form1 : Form
    {

        System.Media.SoundPlayer fat_beats;
        System.IO.StreamReader settings;

        string[] lines = new string[999];
        string line;
        int count = 0;
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
            settings = new System.IO.StreamReader(@"custom.txt");

         
            DirectoryInfo music_dir = new DirectoryInfo(@"Music");
            FileInfo[] Files = music_dir.GetFiles("*.wav");
            string[] names = new string[999];

            int q = 0;
            foreach(FileInfo file in Files)
            {
                names[q] = file.Name;
                //  Debug.WriteLine(names[q]);
                //rtb1.Text += file.Name;
                q++;
            }

            while ((line = settings.ReadLine()) != null )
            {
                if (line != "")
                {
                    lines[count] = line;
                    count++;
                }
            }

            System.IO.File.WriteAllLines(@"all.txt", names);

            btns0.Click += new EventHandler(stop);
            int t = 0;
            int f = 0;
            for (int i = 0; i < count; i++)
            {
                
                TextBox newText = new TextBox();
                {
                    newText.Name = string.Format("txt" + i);

                    newText.Location = new System.Drawing.Point(118 -70, 0 + f);
                    newText.Size = new System.Drawing.Size(169, 20);
                    panel1.Controls.Add(newText);
                  //  newButton.Click += new EventHandler(ButtonHandler);
                }

                f += 35;
                Button newButton = new Button();
                {
                    newButton.Name = string.Format("btn" + i);
                    newButton.Text = string.Format("|>");
                    newButton.Location = new System.Drawing.Point(70 -70 , 0 + t);
                    newButton.Size = new System.Drawing.Size(24, 26);
                    newButton.BackColor = Color.LimeGreen;
                    panel1.Controls.Add(newButton);
                    newButton.Click += new EventHandler(ButtonHandler);
                }
                t += 35;
            }

            for (int i = 0; i < count ; i ++)
            {
                string txt = "txt" + i;

                TextBox texts = this.Controls.Find(txt, true).FirstOrDefault() as TextBox;

                texts.Text = lines[i];
            }
        }

        #region line1


        void stop(object sender, System.EventArgs e)
        {
            fat_beats.Stop();
        }
        void ButtonHandler(object sender, System.EventArgs e)
        {


            string ass = ((Button)sender).Name;
           // lblError.Text = ass;
            var result = Regex.Match(ass, @"\d+$").Value;
            //  char last = ass[ass.Length - 1];
            int a = int.Parse(result);
            
            Debug.WriteLine(a);
            string lol = lines[a];
        
            try
            {
                txtNow.Text = lol;
                fat_beats = new System.Media.SoundPlayer(@"Music\" + lol);     // set up media player with txt1.Text being the name of the file in root
                fat_beats.Play();

                lblError.Text = "Press |> to play song";
                lblError.BackColor = Color.White;
            }


            catch
            {
                lblError.Text = "Error - File not found";
                lblError.BackColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fat_beats.Stop();
            txtNow.Text = "none";
        }
        #endregion
        int rand;
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            rand  = rnd.Next(0, count);
            try
            {
                fat_beats = new System.Media.SoundPlayer(@"Music\" + lines[rand]);     // set up media player with txt1.Text being the name of the file in root
                fat_beats.Play();
                
                lblError.Text = "Press |> to play song";
                lblError.BackColor = Color.White;
            }
            catch { lblError.Text = "Error - FIle not found";
                lblError.BackColor = Color.Red;
            }
            txtNow.Text = lines[rand];
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                fat_beats = new System.Media.SoundPlayer(@"Music\" + txtSearch.Text);     // set up media player with txt1.Text being the name of the file in root
                fat_beats.Play();
                txtNow.Text = txtSearch.Text;

                lblError.Text = "Press |> to play song";
                lblError.BackColor = Color.White;
            }
            catch
            {
                txtNow.Text = "none";
                lblError.Text = "Error - File not found";
                lblError.BackColor = Color.Red;               
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rend = new Random();
            string[] newAry = new string[999];
            newAry = lines.OrderBy(x => rend.Next()).ToArray();
            settings.Close();
            File.Create(@"custom.txt").Close();
            newAry = newAry.Where(x => !string.IsNullOrEmpty(x)).ToArray(); // removes white space
            System.IO.File.WriteAllLines(@"custom.txt", newAry);


            System.Diagnostics.Process.Start(@"SolaireBoard.exe");
            this.Close();

        }

        private void btnAll_Click(object sender, EventArgs e)
        {

        }
    }
}
