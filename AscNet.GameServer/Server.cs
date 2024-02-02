using System.Net.Sockets;
using System.Net;
using AscNet.Logging;

namespace AscNet.GameServer
{
    public class Server
    {
        public static Logger log;
        public readonly Dictionary<string, Session> Sessions = new();
        private static Server? _instance;
        private readonly TcpListener listener;

        public static Server Instance
        {
            get
            {
                return _instance ??= new Server();
            }
        }

        static Server()
        {
            // TODO: add loglevel based on appsettings
            LogLevel logLevel = LogLevel.DEBUG;
            LogLevel fileLogLevel = LogLevel.DEBUG;
            log = new(typeof(Server), logLevel, fileLogLevel);
        }

        public Server()
        {
            listener = new(IPAddress.Parse("0.0.0.0"), Common.Common.config.GameServer.Port);
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    listener.Start();
                    log.Info("服务器初始化完成");
                    log.Info(" ");
                    log.Info(" _____ _                        ");
                    log.Info("|  ___| | _____      _____ _ __ ");
                    log.Info("| |_  | |/ _ \\ \\ /\\ / / _ \\ '__|");
                    log.Info("|  _| | | (_) \\ V  V /  __/ |   ");
                    log.Info("|_|   |_|\\___/ \\_/\\_/ \\___|_|   ");
                    log.Info("                           @2024");
                    log.Info(" ");
                    log.Warn("测试构建 时间2024-01-29");
                    log.Info($"{nameof(GameServer)} 已加载并监听端口 {Common.Common.config.GameServer.Port}");

                    while (true)
                    {
                        TcpClient tcpClient = listener.AcceptTcpClient();
                        string id = tcpClient.Client.RemoteEndPoint!.ToString()!;

                        log.Warn($"{id} 已连接");
                        Sessions.Add(id, new Session(id, tcpClient));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("TCP listener error: " + ex.Message);
                    log.Info("Waiting 3 seconds before restarting...");
                    Thread.Sleep(3000);
                }
            }
        }

        public Session? SessionFromUID(long uid)
        {
            return Sessions.Select(x => x.Value).FirstOrDefault(x => x.player.PlayerData.Id == uid);
        }
    }
}