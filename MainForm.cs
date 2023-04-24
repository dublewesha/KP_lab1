using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;


namespace KP_lab1
{
    [SoapInclude(typeof(Point3D))]
    [XmlInclude(typeof(Point3D))]
    [Serializable]
    public class Point3D : Point
    {
        public int Z { get; set; }
        public Point3D() : base()
        {
            Z = rnd.Next(10);
        }
        public Point3D(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }
        public override double Metric()
        {
            return Math.Sqrt(Math.Pow(X, X) + Math.Pow(Y, Y) + Math.Pow(Z, Z));
        }
        public override string ToString()
        {
            return string.Format($"({X}, {Y}, {Z})");
        }
    }
    public partial class MainForm : Form
    {
        private Point[]? points = null;
        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            points = new Point[5];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = rnd.Next(3) % 2 == 0 ? new Point() : new Point3D();
            }
            listBox1.DataSource = points;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (points == null)
            {
                return;
            }
            Array.Sort(points);
            listBox1.DataSource = null;
            listBox1.DataSource = points;
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            if (points != null)
            {
                SaveFileDialog dlg = new();
                dlg.Filter = "SOAP|*.soap|XML|*.xml|JSON|*.json|Binary|*.bin";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                using (FileStream fs = new(dlg.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    switch (Path.GetExtension(dlg.FileName))
                    {
                        case ".bin":
                            break;
                        case ".json":
                            string final = JsonConvert.SerializeObject(points);
                            byte[] wr = Encoding.UTF8.GetBytes(final);
                            fs.Write(wr, 0, wr.Length);
                            break;
                        case ".xml":
                            XmlSerializer xml = new(points.GetType());
                            xml.Serialize(fs, points);
                            break;
                        case ".soap":
                            XmlSerializer xmlSerializerxml = new(points.GetType());
                            XmlWriterSettings sett = new XmlWriterSettings
                            {
                                Encoding = Encoding.UTF8,
                                Indent = true,
                                OmitXmlDeclaration = true,
                            };
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("—ериализаци€ невозможна, не заполены точки");
            }
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {

        }
    }
}