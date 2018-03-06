using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ZeroKreiz
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.CenterToParent();
            string abouttxt="Крестики-нолики. Версия "+Assembly.GetAssembly(this.GetType()).GetName().Version;
            string abouttxt1 = Char.ConvertFromUtf32(13)+"     Copyright "+Char.ConvertFromUtf32(169) +" 2010 by M.F. ";
            string abouttxt2 = Char.ConvertFromUtf32(13)+Char.ConvertFromUtf32(13)+"         Free evalution copy.";
            this.txtab.Text = abouttxt+abouttxt1+abouttxt2; 
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
