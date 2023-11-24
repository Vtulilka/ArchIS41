using System.Net.Sockets;
using System.Text;
using NLog;

namespace UdpServer
{
    public class Server
    {
        private const int Port = 8001;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static DataController DC = new DataController();
        static async Task Main()
        {
            using UdpClient udpClient = new UdpClient(Port);

            logger.Info("Сервер запущен и ждет подключения...");

            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                string request = Encoding.UTF8.GetString(result.Buffer);

                logger.Info($"Запрос получен от {result.RemoteEndPoint}: {request}");

                string response = ProcessRequest(request);
                byte[] responseData = Encoding.UTF8.GetBytes(response);

                _ = udpClient.SendAsync(responseData, responseData.Length, result.RemoteEndPoint);

                logger.Info($"Запрос отправлен: {result.RemoteEndPoint}: {response}");
            }
        }

        private static string ProcessRequest(string request)
        {
            string[] data = request.Split(',');
            string str = data[0];
            string result = request.Replace(str + ",", string.Empty);

            switch (str)
            {
                case "1":
                    return GetAllRecords();
                case "2":
                    DC.DeleteAll();
                    AddRecords(result);
                    return "Изменения сохранены";
                default:
                    return "Что-то пошло не так";
            }
        }
        private static string GetAllRecords()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < DC.GetStudents().Count; i++)
            {
                string sch = $"{DC.GetStudents()[i].ID},{DC.GetStudents()[i].Name},{DC.GetStudents()[i].Surname},{DC.GetStudents()[i].Age},{DC.GetStudents()[i].IsBad}";
                
                sb.AppendLine(sch);                
            }
            return sb.ToString();
        }

        private static void AddRecord(string Name, string Surname, int Age, bool IsBad)
        {
            DC.AddStudent(new School { Name = Name, Surname = Surname, Age = Age, IsBad = IsBad });
        }
        private static void AddRecords(string request)
        {
            foreach (string line in request.Split(new char[] { '\n' }))
            {
                if (String.IsNullOrEmpty(line))
                { }
                else
                {
                    string str = line.ToString();
                    string[] data = str.Split(',');
                    AddRecord(data[0], data[1], int.Parse(data[2]), bool.Parse(data[3]));
                }
            }
        }
    }
}



