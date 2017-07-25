using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Config;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //Config记录多条ServerIP记录，在以后将该实例转至外层
        private config _config;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            /*if (!SaveOldSelectedServer())
            {
                return;
            }
            if (_modifiedConfiguration.configs.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }*/
            //SaveServers(_modifiedConfiguration.configs, _modifiedConfiguration.localPort);
            Server tServer = new Server();
            tServer._serverIp = IPTextBox.Text.Trim();
            tServer._password = PasswordtextBox.Text;

            if (!int.TryParse(SeverPorttextBox.Text, out tServer._server_port))
            {
                MessageBox.Show("Illegal port number format");
            }

            //_config.addServer(tServer, localPort);
            //SaveServers();

            // SelectedIndex remains valid
            // We handled this in event handlers, e.g. Add/DeleteButton, SelectedIndexChanged
            // and move operations
            //controller.SelectServerIndex(ServersListBox.SelectedIndex);
            //关闭ServerConfig窗口
            this.Close();
        }


        public void SaveServers(List<Server> servers, int localPort)
        {
            //_config.configs = servers;
           // _config.localPort = localPort;
            //Save(_config);
        }




    }
}
