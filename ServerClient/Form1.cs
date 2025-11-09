using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ServerClient
{
    public partial class Form1 : Form
    {

        private TcpClient server = new TcpClient();
        private CancellationTokenSource cancel = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("enter name"); return;
            }

            string mess = textBox2.Text;
            if (mess == ""){ return;    }
            mess = $"{name}:{mess}";

            byte[] bytes = Encoding.UTF8.GetBytes(mess);   
            await server.GetStream().WriteAsync(bytes,0, bytes.Length);


        }

        private async Task ListenToServer()
        {
            byte[] buffer = new byte[1024];
            while (true) { 
                if(cancel.Token.IsCancellationRequested) return;
                int read = await server.GetStream().ReadAsync(buffer, 0, buffer.Length, cancel.Token);

                if (cancel.Token.IsCancellationRequested) return;

                string mess = Encoding.UTF8.GetString(buffer, 0, read);
                textBox3.Text = $"{textBox3.Text}\r\n{mess}";
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await server.ConnectAsync("127.0.0.1", 2024);
            await ListenToServer();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cancel.Cancel();
        }
    }
}
