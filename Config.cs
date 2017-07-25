using System;


namespace Config
{
    public class Server
    {
        public string _serverIp;
        public int _server_port;
        public string _password;

        //construct function
        public Server(string serverIp, int server_port, string password)
        {
            _serverIp = serverIp;
            _server_port = server_port;
            _password = password;
        }

        public Server()
        {


        }
        //destructor function
        ~Server()
        {

        }

    }

    public class config
    {
        public List<Server> _configs;
        public int _localPort;
        const int defalutLocalPort = 1080;
        public config()
        {
            _configs = new List<Server>();
            _localPort = defalutLocalPort;
        }

        public void addServer(Server newServer, int localPort = defalutLocalPort)
        {
            _localPort = localPort;

            if (!_configs.Contains(newServer))
            {
                _configs.Add(newServer);
                MessageBox.Show("Input New Server Success");
            }
            else
                MessageBox.Show("Input New Server Failed");
        }

        ~config()
        {

        }

    }
}