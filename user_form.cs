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

   // Alap jelszó felhasználó : jegyvasarlo, jegyvasarlo1
    public partial class user_form : Form
    {
        const int FIRST_SECONDCLASS = 25;
        int first_class = 0;
        int second_class = 0;
        bool inter = false;
        DataTable dt = new DataTable();
        SQLiteDataAdapter sda;
        SQLiteCommand cmd = new SQLiteCommand();
        public user_form(bool international,String username)
        {
           
            InitializeComponent();
            inter = international;
            String[] myArray1 = { "Budapest-Hamburg", "Bécs- Róma"};
            String[] myArray2 = { "Veszprém- Budapest", "Szeged-Kecskemét" };
            usernameLBL.Text = username;
            first_count.Text = first_class.ToString();
            second_count.Text = second_class.ToString();
            kupon.LostFocus += new System.EventHandler(kupon_focus);



            if (international) 
            { 
                comboBox1.DataSource = myArray1; 
            }
            else
            {
                comboBox1.DataSource = myArray2;
            }

            comboBox1.LostFocus += new System.EventHandler(changeCombo);


            List<beadando.CustomControls.RoundedButton> lColors = this.Controls.OfType< beadando.CustomControls.RoundedButton> ().ToList();
            foreach(beadando.CustomControls.RoundedButton btn in lColors)
            {
                if(btn.Name != "payBTN" && btn.Name != "focus_lostBTN" && btn.Name != "kuponBTN")
                {
                    btn.Text = "";
                    btn.BackColor = Color.LightGreen;
                    btn.Click += new System.EventHandler(click);
                }
            }
            set_seats();

        }
        public void click(object sender, EventArgs e)
        {
            
            CustomControls.RoundedButton btn = (CustomControls.RoundedButton)sender;
            char[] id_char = btn.Name.ToCharArray();
            int id = 0;
           
            if (btn.Name.Length == 3)
            {
               
                id = Convert.ToInt32(id_char[2].ToString());

            }
            else 
            {

                id =Convert.ToInt32(id_char[2].ToString() + id_char[3].ToString());
            }
          

            if (btn.BackColor == Color.Yellow)
            {
                if (id >= FIRST_SECONDCLASS)
                {
                    second_class--;
                }
                else
                {
                    first_class--;
                }

                btn.BackColor = Color.LightGreen;
            }
            else
            {
                if(btn.BackColor == Color.LightGreen)
                {
                    if (id >= FIRST_SECONDCLASS)
                    {
                        second_class++;
                    }
                    else
                    {
                        first_class++;
                    }
                    btn.BackColor = Color.Yellow;

                 
                }
            }
            first_count.Text = first_class.ToString();
            second_count.Text = second_class.ToString();
            calculate_price();



        }
        public void changeCombo(object sender,EventArgs e)
        {
            if (comboBox1.Text == "Budapest-Hamburg") { stopsLBL.Text = "Budapest Pozsony, \r\n  Prága, Drezda, \r\n Berlin, Hamburg"; }
            if (comboBox1.Text == "Bécs- Róma") { stopsLBL.Text = "Bécs, Klagenfurt, \r\n Velence, Róma"; }
            if (comboBox1.Text == "Veszprém- Budapest") { stopsLBL.Text = "Veszprém, Budapest"; }
            if (comboBox1.Text == "Szeged-Kecskemét") { stopsLBL.Text = "Szeged-Kecskemét"; }
            set_seats();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void kupon_focus(object sender,EventArgs e)
        {
            calculate_price();

        }
        private void calculate_price()
        {
        
            double price = 0;
            int first_c = Convert.ToInt32(first_count.Text);
            int second_c = Convert.ToInt32(second_count.Text);

            if (inter)
            {
                price = (first_c * 10000) + (second_c * 7500);
           
            }
            else
            {
                price = (first_c * 2500) + (second_c * 1750);
            }
            if (kupon.Text == "k10")
            {
                price *= 0.9;
            }
            else if (kupon.Text == "k5")
            {
                price *= 0.95;
            }

            sum_price.Text = price.ToString();
        }

        private void set_seats()
        {
            first_class = 0;
            second_class = 0;
            first_count.Text = "0";
            second_count.Text = "0";
            sum_price.Text = "0";
            int target = 1;
            if (comboBox1.Text == "Budapest-Hamburg") { target = 1; }
            if (comboBox1.Text == "Bécs- Róma") { target = 2; }
            if (comboBox1.Text == "Veszprém- Budapest") { target = 3; }
            if (comboBox1.Text == "Szeged-Kecskemét") { target = 4; }


            List<beadando.CustomControls.RoundedButton> btns = this.Controls.OfType<beadando.CustomControls.RoundedButton>().ToList();
            DB db = new DB();
            cmd = new SQLiteCommand("select * from seats", db.GetConnection());
            sda = new SQLiteDataAdapter(cmd);
            sda.Fill(dt);
            foreach (beadando.CustomControls.RoundedButton btn in btns)
            {
                if (btn.Name != "payBTN" && btn.Name != "focus_lostBTN" && btn.Name != "kuponBTN")
                {
                  
                    btn.BackColor = Color.LightGreen;
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                if (row[target].ToString() != "FREE")
                {
                    String id = "rb" + row[0];
                    Control btn = this.Controls[id];
                    btn.BackColor = Color.Red;
                }
               
            }
        }

        private void pay()
        {

            String col = "";

            if (comboBox1.Text == "Budapest-Hamburg") { col ="bph"; }
            if (comboBox1.Text == "Bécs- Róma") { col = "br"; }
            if (comboBox1.Text == "Veszprém- Budapest") { col = "vpbp"; }
            if (comboBox1.Text == "Szeged-Kecskemét") { col = "szkecs"; }

            List<beadando.CustomControls.RoundedButton> btns = this.Controls.OfType<beadando.CustomControls.RoundedButton>().ToList();
           
            List<beadando.CustomControls.RoundedButton> selected_btns = new List<CustomControls.RoundedButton>();
            
            foreach(CustomControls.RoundedButton btn in btns)
            {
                if (btn.BackColor == Color.Yellow)
                {
                    selected_btns.Add(btn);
                }
            }


                
            foreach(CustomControls.RoundedButton btn in selected_btns)
            {
               
                char[] id_char = btn.Name.ToCharArray();
                int id = 0;

                if (btn.Name.Length == 3)
                {

                    id = Convert.ToInt32(id_char[2].ToString());

                }
                else
                {
                    
                    id = Convert.ToInt32(id_char[2].ToString() + id_char[3].ToString());
                }
                    DB db = new DB();
                    db.openconnection();
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE seats SET "+ col +"='"+this.usernameLBL.Text + "' WHERE id="+ id.ToString() , db.GetConnection());
                    cmd.ExecuteNonQuery();
                    db.closeconnection();

            }

        }

        private void payBTN_Click(object sender, EventArgs e)
        {
            
            pay();
            set_seats();
        }
    }



    class ComboItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
