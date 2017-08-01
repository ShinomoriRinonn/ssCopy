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




namespace MyAppSS
{
    public partial class Form1 : Form
    {

        //Config记录多条ServerIP记录，在以后将该实例转至外层
        private Server _server;
        private int _serverPort, _localPort;
        private Boolean _isAdd;

        private config _config;
        private int _selectedIndex;

        public Form1()
        {
            InitializeComponent();

            _selectedIndex = 0;
            _serverPort = 0;
            _localPort = 0;
            _config = new config();
            _server = new Server();

            config.load(ref _config);
            displayConfig();
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

            getServerInTextBox();
            saveTextChange();
            // SelectedIndex remains valid
            // We handled this in event handlers, e.g. Add/DeleteButton, SelectedIndexChanged
            // and move operations
            //controller.SelectServerIndex(ServersListBox.SelectedIndex);

            //关闭ServerConfig窗口
            this.Close();
        }

        private void displayConfig()
        {
            if (_config._serverIndex != 0)
                _selectedIndex = _config._serverIndex;

            disPlayServerListBox();
            disPlaySelectedServer();
        }

        /**
         * 根据textbox的内容维护私有变量_server
         * */
        private void getServerInTextBox()
        {
            _server._serverIp = IPTextBox.Text.Trim();
            _server._password = PasswordtextBox.Text;

            if (!int.TryParse(SeverPorttextBox.Text, out _serverPort))
            {
                MessageBox.Show("Illegal port number format");
            }

            if (!int.TryParse(ProxyPorttextBox.Text, out _localPort))
            {
                MessageBox.Show("Illegal port number format");
            }
            _server._server_port = _serverPort;
        }

        public void disPlayServerListBox()
        {
            ServersListBox.Items.Clear();
            foreach (Server server in _config._configs)
            {
                ServersListBox.Items.Add(Server.toString(server));
            }
            ServersListBox.SelectedIndex = _selectedIndex;
        }

        public void disPlaySelectedServer()
        {
            int index = ServersListBox.SelectedIndex;
            IPTextBox.Text = _config._configs[index]._serverIp.ToString();
            SeverPorttextBox.Text = _config._configs[index]._server_port.ToString();
            PasswordtextBox.Text = _config._configs[index]._password;
            ProxyPorttextBox.Text = _config._localPort.ToString();
        }

        private void temp()
        {
            getServerInTextBox();
            _config._configs[_selectedIndex] = new Server(_server);
        }


        private void ServersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nextSelectedIndex = ServersListBox.SelectedIndex;

            if (!ServersListBox.CanSelect)
            {
                return;
            }
            if (_selectedIndex == nextSelectedIndex)
            {
                // we are moving back to oldSelectedIndex or doing a force move
                return;
            }

            getServerInTextBox();
            if (!_config._configs[_selectedIndex].Equals(_server) && !_isAdd)
                _config._configs[_selectedIndex] = new Server(_server);

            _isAdd = false;
            _selectedIndex = nextSelectedIndex;
            disPlaySelectedServer();
            disPlayServerListBox();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            getServerInTextBox();
            _isAdd = _config.addServer(new Server(_server));
            disPlayServerListBox();
            if (_isAdd)
                ServersListBox.SelectedIndex = _config._configs.Count - 1;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //usetoDebug
            int a = 0;
        }

        /**
         * 该函数将Form1中私有变量的值存入本地（config/localport/selectedIndex）
         **/
        private void saveTextChange()
        {
            _config._configs[_selectedIndex] = _server;
            _config._localPort = _localPort;
            _config._serverIndex = _selectedIndex;
            _config.saveToLocal();
        }
    }
}
