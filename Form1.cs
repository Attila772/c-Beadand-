using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace beadando
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        SQLiteDataAdapter sda;
        SQLiteCommand cmd = new SQLiteCommand();
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.roundedButton1 = new beadando.CustomControls.RoundedButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(109, 111);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(273, 22);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(109, 177);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(273, 22);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(162, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 59);
            this.label3.TabIndex = 5;
            this.label3.Text = "Login";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(109, 248);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(305, 39);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "International travel";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.Gray;
            this.roundedButton1.BackGroundColor = System.Drawing.Color.Gray;
            this.roundedButton1.BorderColor = System.Drawing.Color.Silver;
            this.roundedButton1.BorderRadius = 60;
            this.roundedButton1.BorderSize = 3;
            this.roundedButton1.FlatAppearance.BorderSize = 0;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.ForeColor = System.Drawing.Color.White;
            this.roundedButton1.Location = new System.Drawing.Point(109, 322);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(273, 61);
            this.roundedButton1.TabIndex = 7;
            this.roundedButton1.Text = "Login";
            this.roundedButton1.TextColor = System.Drawing.Color.White;
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += new System.EventHandler(this.roundedButton1_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(484, 415);
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

 

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            cmd = new SQLiteCommand("select * from users where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count.ToString() == "1")
            {
                if (dt.Rows[0][3].ToString() == "1")
                {
                    MessageBox.Show("Admin Login Sikeres");
                }
                else 
                {
                   user_form asd = new user_form(this.checkBox1.Checked, textBox1.Text);
                    asd.Show();
                    this.Hide();
                }
                
               
            }
            else
            {
                MessageBox.Show("Rossz felh. név vagy jelszó", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt.Clear();
            }
        }
    }
}
