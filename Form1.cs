using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace GUI_Razzaq
{
    public partial class Form1 : Form
    {
        string rek;
        int jumlah = 0;
        string message;
        
        public Form1()
        {
            InitializeComponent();            
        }

        private void Judul_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            rek = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            jumlah = 100000;            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            jumlah = 200000;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            jumlah = 300000;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            jumlah = 500000;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            jumlah = 1000000;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            jumlah = 5000000;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            message = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WithdrawConfig withdraw = new WithdrawConfig();
            int saldo = Convert.ToInt32(withdraw.config.saldo);
            if (saldo <= 0)
            {
                MessageBox.Show("Saldo anda tidak mencukupi minimal penarikan tunai");
                MessageBox.Show("Sisa saldo anda " + (saldo));
            }
            else
            {
                if (textBox1.Text == "" && jumlah == 0)
                {
                    MessageBox.Show("Rekening Tujuan Belum Diisi!");
                    textBox1.Focus();                    
                }
                else
                {
                    int sisaSaldo = saldo - jumlah;
                    if (sisaSaldo < 0)
                    {
                        MessageBox.Show("Saldo anda tidak mencukupi minimal penarikan tunai");
                        MessageBox.Show("Sisa saldo anda " + (saldo));
                    }
                    else
                    {
                        MessageBox.Show("Tarik tunai ke rekening " + rek + " dengan nonimal Rp. " + jumlah + " berhasil");
                        MessageBox.Show("Sisa saldo anda " + (saldo - jumlah));
                        Config.UpdateSaldo(saldo, jumlah);
                    }
                    
                }

            }       
            
        }

        private void nominal_Click(object sender, EventArgs e)
        {

        }
    }
    public class WithdrawConfig
    {
        public Config config;
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public string fileConfigName = "withdraw_config.json";

        public WithdrawConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        private Config ReadConfigFile()
        {
            string jsonStringFromFile = File.ReadAllText(path + "/" + fileConfigName);
            config = System.Text.Json.JsonSerializer.Deserialize<Config>(jsonStringFromFile);
            return config;
        }

        private void SetDefault()
        {
            int fee = 2500;

            int saldo = 100000000;

            config = new Config(fee, saldo);
        }

        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string jsonString = System.Text.Json.JsonSerializer.Serialize(config, options);
            string fullFilePath = path + "/" + fileConfigName;
            File.WriteAllText(fullFilePath, jsonString);
        }
    }

    public class Config
    {
        
        public int fee { get; set; }
        public int saldo { get; set; }
        public Config() { }
        public Config(int fee, int saldo)
        {
            this.fee = fee;
            this.saldo = saldo;
        }
        public static void UpdateSaldo(int Saldo, int Jumlah)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fileConfigName = "withdraw_config.json";
            string pathAndFile = path + "/" + fileConfigName;

            string jsonStringFromFile = File.ReadAllText(path + "/" + fileConfigName);
            var config = System.Text.Json.JsonSerializer.Deserialize<Config>(jsonStringFromFile);

            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStringFromFile) as JObject;
            JToken jToken = jObject.SelectToken("saldo");
            if (config.saldo <= 0)
            {
                int Newsaldo = 0;
                jToken.Replace(Newsaldo);
            }
            else
            {
                int Newsaldo = Saldo - Jumlah - config.fee;
                jToken.Replace(Newsaldo);
            }
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(pathAndFile, updatedJsonString);
        }
    }
}

