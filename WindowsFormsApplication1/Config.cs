using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Config
{
    public class Server
    {
        public string _serverIp;
        public int _server_port;
        public string _password; 

        public Server()
        {
            _serverIp = "";
            _server_port = 0;
            _password = "";
        }

        public Server(Server input)
        {
            _serverIp = input._serverIp;
            _server_port = input._server_port;
            _password = input._password;
        }

        //construct function
        public Server(string serverIp, int server_port, string password)
        {
            _serverIp = serverIp;
            _server_port = server_port;
            _password = password;
        }
        
        public static Server getDefaultServer()
        {
            Server defaultServer = new Server("us01.us", 23456, "shino");
            return defaultServer;
        }

        public static string toString(Server server)
        {
            string temp = server._serverIp + ":" + server._server_port.ToString();
            return temp;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals(obj as Server);
        }

        private bool Equals(Server server)
        {
            return ( this._password == server._password && 
                this._serverIp == server._serverIp && this._server_port == server._server_port);
        }
        /*
        public override int GetHashCode()
        {
            return this.Id;
        }
        */
    }

    public class config
    {
        private static string CONFIG_FILE = "gui-config.json";

        public List<Server> _configs { set; get; }
        public int _localPort { set; get; }
        public int _serverIndex { set; get; }

        static int defaultLocalPort = 1080;
        static Server defaultServer = Server.getDefaultServer();

        public config()
        {
            _configs = new List<Server>();
            _localPort = defaultLocalPort;
            _serverIndex = 0;
        }

        public Boolean addServer(Server newServer)
        {
            if (!_configs.Contains(newServer))
            {
                _configs.Add(newServer);
                MessageBox.Show("Input New Server Success");
                return true;
            }
            else
            {
                MessageBox.Show("Input New Server Failed");
                return false;
            }
        }

        public void saveToLocal()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(CONFIG_FILE, FileMode.Create)))
                {
                    string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                    sw.Write(jsonString);
                    sw.Flush();
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static void load(ref config loadInst)
        {
            try
            {
                string configContent = File.ReadAllText(CONFIG_FILE);
                loadInst = JsonConvert.DeserializeObject<config>(configContent);
   
                if (loadInst._configs == null)
                    loadInst._configs = new List<Server>();
                if (loadInst._configs.Count == 0)
                    loadInst.addServer(defaultServer);
                if (loadInst._localPort == 0)
                    loadInst._localPort = defaultLocalPort;
                //if (loadInst._serverIndex == 0)
                //   loadInst._serverIndex = 0;

                //config.proxy.CheckConfig();
            }
            catch (Exception e)
            {
                if (!(e is FileNotFoundException))
                    MessageBox.Show("FileNotFoundExceptio");

                loadInst = new config();
                loadInst.addServer(defaultServer);

            }
        }



    }
}
