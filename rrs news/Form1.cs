using System.Net;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text;
using System.Data.SQLite;
using Microsoft.VisualBasic;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;
using System.Data.Common;

namespace _7_8_laba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(textBox1.Text);
            HttpWebResponse rs = (HttpWebResponse)rq.GetResponse();
            Stream stream = rs.GetResponseStream();
            StreamReader read = new StreamReader(stream);
            string temp = read.ReadToEnd();
            XmlDocument xmlNews = new XmlDocument();
            xmlNews.LoadXml(temp);
            XmlNodeList childNodeList = xmlNews.DocumentElement.SelectSingleNode("channel").SelectNodes("item");
            SQLiteConnection db = new SQLiteConnection("Data Source = dataBase.db;");
            db.Open();
            SQLiteCommand command = new SQLiteCommand("PRAGMA synchronous = 1; DELETE FROM News; CREATE TABLE IF NOT EXISTS" +
                " News(Id INTEGER PRIMARY KEY AUTOINCREMENT, Title, Link, Description, PubDate); ",db);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("INSERT INTO News(Title,Link,Description,PubDate) VALUES(@title, @link," +
                " @description, @pubDate)", db);

            foreach (XmlNode xmlNode in childNodeList)
            {
                command.Parameters.AddWithValue("@title", xmlNode.SelectSingleNode("title").InnerText);
                command.Parameters.AddWithValue("@link", xmlNode.SelectSingleNode("link").InnerText);
                command.Parameters.AddWithValue("@description", xmlNode.SelectSingleNode("description").InnerText);
                command.Parameters.AddWithValue("@pubDate", xmlNode.SelectSingleNode("pubDate").InnerText);
                command.ExecuteNonQuery();
            }
            db.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            SQLiteConnection db = new SQLiteConnection("Data Source = dataBase.db;");
            db.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM news", db);
            foreach(DbDataRecord item in command.ExecuteReader())
            {
                richTextBox1.AppendText(item["title"]+"\n\n");
                richTextBox1.AppendText(item["description"] + "\n");
                richTextBox1.AppendText("Дата: " + item["pubDate"] + "\n");
                richTextBox1.AppendText("Полностью: " + item["link"] + "\n\n\n");
            }
            db.Close();
        }
    }
}