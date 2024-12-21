using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DovizKurlariDataTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XDocument kurlar = XDocument.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
            XElement rootElement = kurlar.Root;
            DataTable dt = new DataTable();
            dt.Columns.Add("Kod");
            dt.Columns.Add("Isim");
            dt.Columns.Add("Alış");
            dt.Columns.Add("Satış");
            foreach (XElement kur in rootElement.Elements())
            {
                string kod = kur.Attribute("Kod").Value;
                string isim = kur.Element("Isim").Value;
                //double satis = !string.IsNullOrEmpty(kur.Element("ForexSelling").Value) ? Convert.ToDouble(kur.Element("ForexSelling").Value.Replace('.',',')): 0;
                int unit = Convert.ToInt32(kur.Element("Unit").Value);
                double satis = 0;
                if (!string.IsNullOrEmpty(kur.Element("ForexSelling").Value))
                {
                    string strSatis = kur.Element("ForexSelling").Value.Replace('.', ',');
                    satis = Convert.ToDouble(strSatis) / unit;
                }

                double alis = 0;
                if (!string.IsNullOrEmpty(kur.Element("ForexBuying").Value))
                {
                    string strAlis = kur.Element("ForexBuying").Value.Replace('.', ',');
                    alis = Convert.ToDouble(strAlis) / unit;
                }
                dt.Rows.Add(kod, isim, satis, alis);
            }
            DGV_kurlar.DataSource = dt;
            

        }
    }
}
