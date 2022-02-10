using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;


namespace Şifreleme
{
   public partial class Form1 : Form
    {
        Stopwatch watch = new Stopwatch();
        CryptAndDecrypt cryptAndDecrypt = new CryptAndDecrypt();
       
        public Form1()
        {
            InitializeComponent();
           
        }  
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            //listBox1.Items.Clear();
            listBox3.Items.Clear();
            label4.Text = "Encrypt Time";
            label6.Text = "Decrypt Time";
            watch.Reset();
        }//Sil butonuna tıklandığı zaman listboxtaki veriler silinir.

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.ShowDialog();
                if (op.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = op.FileName;
                }
            }
            catch { }
        }//tıklandığında dosyayı seçmesini sağlar.

        private void btn_load_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(textBox1.Text.Trim());
                foreach (string line in lines)
                {
                    listBox1.Items.Add(line);

                }
            }
            catch
            {

            }
        }//txt dosyasını listbox1'e aktaran koddur.

       

        private void button1_Click(object sender, EventArgs e)
        {
            
           
            switch (comboBox1.Text)
                {
                    
                    case "Caesar":
                    
                    if (listBox1.Items.Count > 0)
                        {
                        watch.Start();
                        foreach (string item in listBox1.Items)
                            { listBox2.Items.Add(cryptAndDecrypt.Ceaser(item, int.Parse(textBox2.Text))); 
                        }
                        watch.Stop();
                        double totalMillisecondsCaesar = watch.Elapsed.TotalMilliseconds;
                        label4.Text = totalMillisecondsCaesar + "milisaniyede sifrelendi";
                        watch.Reset();
                    }
                     
                    break;
                    case "MD5":
                    if (listBox1.Items.Count > 0)
                    {
                        watch.Start();

                        foreach (string item in listBox1.Items)
                        {
                            listBox2.Items.Add(cryptAndDecrypt.md5(item));
                          
                        }
                        watch.Stop();
                        double totalMillisecondsMD5 = watch.Elapsed.TotalMilliseconds;
                        label4.Text =totalMillisecondsMD5 + "milisaniyede sifrelendi";
                        watch.Reset();
                    }
                    break;
                    case"DES":
                   
                    if (listBox1.Items.Count>0)
                    {
                        watch.Start();
                        foreach (string item in listBox1.Items)
                        {                          
                            listBox2.Items.Add(cryptAndDecrypt.Des(item, textBox2.Text));
                        }
                        watch.Stop();
                        double totalMillisecondsDes = watch.Elapsed.TotalMilliseconds;
                        label4.Text = totalMillisecondsDes + "milisaniyede sifrelendi";
                        watch.Reset();
                    }
                       break;
                    case "AES":
                    
                    if(listBox1.Items.Count>0)
                    {
                        watch.Start();
                        foreach (string item in listBox1.Items)
                        {
                            
                            listBox2.Items.Add(cryptAndDecrypt.Aes(item));
                        }

                        watch.Stop();
                        double totalMillisecondsAes = watch.Elapsed.TotalMilliseconds;
                        label4.Text = totalMillisecondsAes + "milisaniyede sifrelendi";
                        watch.Reset();

                    }
                    break;
                case "RSA":
                    CryptAndDecrypt.generateKeys();
               
                    if(listBox1.Items.Count>0)
                    {
                        watch.Start();
                        foreach (string item in listBox1.Items)
                        {
                            byte[] encrypt = CryptAndDecrypt.RSA(Encoding.UTF8.GetBytes(item));
                            listBox2.Items.Add(BitConverter.ToString(encrypt).Replace("-",""));
                        }
                        watch.Stop();
                        double totalMillisecondsBlowFish = watch.Elapsed.TotalMilliseconds;
                        label4.Text = totalMillisecondsBlowFish + "milisaniyede sifrelendi";
                        watch.Reset();

                    }

                        break;
                    default:
                        MessageBox.Show("Tekrar deneyiniz", "Program", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            
           
        
        }//Encrypt the text butonuna basıldığı zaman gerçekleştiği koddur.
        private void button4_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Caesar":
                    if (listBox2.Items.Count > 0)
                    {
                        watch.Start();
                        foreach (string item in listBox2.Items)
                        {
                            listBox3.Items.Add(cryptAndDecrypt.CeaserCozme(item, int.Parse(textBox2.Text)));
                        }
                        watch.Stop();
                        double totalMillisecondsCeaser = watch.Elapsed.TotalMilliseconds;
                        label6.Text = totalMillisecondsCeaser + "milisaniyede çözüldü";
                        watch.Reset();
                    }
                    break;
                case "DES":
                    if(listBox2.Items.Count>0)
                    {
                        watch.Start();
                        foreach (string item in listBox2.Items)
                        {
                            listBox3.Items.Add(cryptAndDecrypt.DesCozme(item,textBox2.Text));
                        }
                        watch.Stop();
                        double totalMillisecondsDes = watch.Elapsed.TotalMilliseconds;
                        label6.Text = totalMillisecondsDes + "milisaniyede çözüldü";
                        watch.Reset();
                    }                     
                       break;
                case "AES":
                    if(listBox2.Items.Count>0)
                    {
                        watch.Start();
                        foreach(string item in listBox2.Items)
                        {
                            listBox3.Items.Add(cryptAndDecrypt.AesCozme(item));
                        }
                        watch.Stop();
                        double totalMillisecondsDes = watch.Elapsed.TotalMilliseconds;
                        label6.Text = totalMillisecondsDes + "milisaniyede çözüldü";
                        watch.Reset();
                    }
                    break;
                case "RSA":                 
                    CryptAndDecrypt.generateKeys();                
                    if(listBox2.Items.Count>0)
                    {
                        watch.Start();
                        foreach (string item in listBox2.Items)
                        {
                            foreach (string items in listBox1.Items)
                            {
                                byte[] encrypt = CryptAndDecrypt.RSA(Encoding.UTF8.GetBytes(items));
                                byte[] decrypt = CryptAndDecrypt.RSACozme(encrypt);
                                listBox3.Items.Add(Encoding.UTF8.GetString(decrypt));
                            }
                        }
                        watch.Stop();
                        double totalMillisecondsDes = watch.Elapsed.TotalMilliseconds;
                        label6.Text = totalMillisecondsDes + "milisaniyede çözüldü";
                        watch.Reset();
                    }
                    break;
            }
        }//Decrypt the text butonuna basıldığı zaman gerçekleştiği koddur.
        


    }
}
