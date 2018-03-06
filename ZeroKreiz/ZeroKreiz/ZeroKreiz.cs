using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace ZeroKreiz
{
    public partial class GameForm : Form
    {
        private Rectangle [,] ListRect = new Rectangle[3,3];
        Timer tim = new Timer();
        private Gamefield GameFld ;
        Point pxd;
        Winline wl;
        public GameForm()
        {
            InitializeComponent();
            CenterToScreen();
            this.Paint += new PaintEventHandler(GameForm_Paint);
            txtNote.Text = "Ваш ход...";
            txtNote.ForeColor = Color.Blue;
            int dif = 1;
            if (this.нормаToolStripMenuItem.CheckState == CheckState.Checked)
                dif = 1;
            if (this.жестьToolStripMenuItem.CheckState == CheckState.Checked)
                dif = 2;
            GameFld = new Gamefield(dif);
            tim.Interval = 10;
            tim.Tick += new EventHandler(tim_Tick);
            progressBar1.Visible = false;
            
        }

        public void Labeldraw()
        {
            switch (GameFld.Winner)
            {
                case 1:
                    txtNote.Text = "Вы проиграли! Деньги на бочку!";
                    txtNote.ForeColor = Color.Red;
                    Drawwinnerline();
                    break;
                case 2:
                    txtNote.Text = "Вы выиграли, поздравляю!"; ;
                    txtNote.ForeColor = Color.Green;
                    Drawwinnerline();
                    break;
                case 0:
                    txtNote.Text = "Ваш ход...";
                    txtNote.ForeColor = Color.Blue;
                    int busycell = 0;
                    for (int i = 0; i <= 2; i++)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            if (GameFld.field[i, j] != 0)
                                busycell++;

                        }
                    }
                    if (busycell == 9)
                    {
                        txtNote.Text = "Ничья вышла...";
                        txtNote.ForeColor = Color.Blue;

                    }
                    break;

            }
        }


        public void Drawwinnerline()
        {
            Graphics g = Graphics.FromHwnd(this.pictureBox1.Handle);
            Pen pn = new Pen(Color.DarkGreen, 2);
            wl = AutoGamer.Drawwinline(GameFld);
            g.DrawLine(pn, wl.Winp1, wl.Winp2);
        }

        void tim_Tick(object sender, EventArgs e)
        {
 
            progressBar1.Value = progressBar1.Value + 10;
            txtNote.Text = "Думаю...";
            txtNote.ForeColor = Color.Blue;
            if (progressBar1.Value== 100)
            {
                
                Step();
                
                progressBar1.Value = 0;
                tim.Stop();
                progressBar1.Visible = false;
            }
        }

        

        public void Step()
        {
            if (GameFld.Winner != 0)
                return;
            progressBar1.Visible = true;
            


            Rectangle rec = new Rectangle();
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {

                    rec = (Rectangle)ListRect[i, j];

                    if (rec.Contains(pxd))
                    {
                        if (GameFld.field[i, j] != 0)
                        {
                           
                            return;
                        }

                        GameFld.field[i, j] = 2;

                    }
                }

            }


           if (this.нормаToolStripMenuItem.CheckState == CheckState.Checked)
                GameFld.diffic = 1;
            if (this.жестьToolStripMenuItem.CheckState == CheckState.Checked)
                GameFld.diffic = 2;

            GameFld = AutoGamer.NextStep(GameFld);
            this.Fillfield(GameFld.field);
            this.Labeldraw();


        }
        public void Cleargame()
        {
            int dif = 1;
            
            GameFld = new Gamefield(dif);
            txtNote.Text = "Ваш ход...";
            txtNote.ForeColor = Color.Blue;

           pictureBox1.Invalidate();

        }
        public void Fillfield(int[,] fld)
        {
            Graphics g = Graphics.FromHwnd(this.pictureBox1.Handle);
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Rectangle rec=new Rectangle();
                    rec = ListRect[i, j];
                    if (fld[i, j] == 1)
                    {
                        
                        Image bitKreiz = global::ZeroKreiz.Properties.Resources.nol;
                        g.DrawImage(bitKreiz, rec);

                    }
                    if (fld[i, j] == 2)
                    {
                        Image bitKreiz = global::ZeroKreiz.Properties.Resources.crest;
                        g.DrawImage(bitKreiz, rec);

                    }
                    if (fld[i, j] == 0)
                    {
                        //this.pictureBox1.Invalidate(rec);

                    }

                  
                   

                }
            }


                            this.Labeldraw();
                            
                  



        }

        void GameForm_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.TranslateTransform(12, 40);
            //Graphics g = e.Graphics;
            
            //Invalidate();
            this.Fillfield(GameFld.field);
            Drawwinnerline();
            //this.pictureBox1.Invalidate();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{base.OnPaint(e);

        //    this.Fillfield(GameFld.field);
        //    Drawwinnerline();
            
            
        //}

        //public override void Refresh()
        //{
        //    base.Refresh();
        //    this.Fillfield(GameFld.field);
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
           //pictureBox1.BackgroundImage = null;
            Graphics g = Graphics.FromHwnd(this.Handle);
            g.TranslateTransform(12, 40);
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    Rectangle rec = new Rectangle(0 + 77 * j, 0 + 77 * i, 77, 77);
                  //  g.DrawRectangle(Pens.Brown, rec);
                    ListRect[i,j]=rec;
                }
            }
            
        }

            
        private void button1_Click(object sender, EventArgs e)
        {
            Cleargame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти без сохранения?","Сохранение игры",MessageBoxButtons.OKCancel) == DialogResult.OK) 
            Close();
        }

       
     

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            pxd = new Point(e.X, e.Y);
            progressBar1.Visible = true;
            tim.Start();
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //this.fillfield(GameFld.field);
           Invalidate();
        }

        private void нормаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.нормаToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.жестьToolStripMenuItem.CheckState = CheckState.Checked;
                this.нормаToolStripMenuItem.CheckState = CheckState.Unchecked;

            }
            if (this.нормаToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                this.жестьToolStripMenuItem.CheckState = CheckState.Unchecked;
                this.нормаToolStripMenuItem.CheckState = CheckState.Checked;

            }

        }

        private void жестьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.жестьToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.нормаToolStripMenuItem.CheckState = CheckState.Checked;
                this.жестьToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            if (this.жестьToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                this.нормаToolStripMenuItem.CheckState = CheckState.Unchecked;
                this.жестьToolStripMenuItem.CheckState = CheckState.Checked;

            }

        }

        private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cleargame();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выйти без сохранения?", "Сохранение игры", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About Frmabout=new About();
            Frmabout.ShowDialog();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream Savestream = File.Create("savegame.gam");
            BinaryFormatter BF = new BinaryFormatter();
            BF.Serialize(Savestream, this.GameFld);
            Savestream.Close();


        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream Savestream = File.OpenRead("savegame.gam");
            this.GameFld = (Gamefield)BF.Deserialize(Savestream);
            Savestream.Close();
            this.pictureBox1.Invalidate();
        }
    }
}
