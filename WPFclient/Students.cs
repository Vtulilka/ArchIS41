using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;

namespace WPFclient
{
    class Students
    {
        public ICommand LoadDataCM { get; set; }
        public ICommand SaveDataCM { get; set; }
        public ObservableCollection<SchoolModel> Student
        {
            get { return student; }
            set
            {
                student = value;
            }
        }
        Dictionary<int, SchoolModel> StudentsDict = new Dictionary<int, SchoolModel>();
        private ObservableCollection<SchoolModel> student = new ObservableCollection<SchoolModel>();
        private const int Port = 8001;
        private readonly string Adress = "127.0.0.1";
        private string? response;

        public Students()
        {
            LoadDataCM = new RelayCommand(LoadData);
            SaveDataCM = new RelayCommand(SaveData);
        }
        public void SendRequest(string request)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Adress), Port);

            byte[] responseData = Encoding.UTF8.GetBytes(request);
            udpClient.Send(responseData, responseData.Length, serverEndPoint);

            IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, Port);
            byte[] receiveData = udpClient.Receive(ref senderEndPoint);
            response = Encoding.UTF8.GetString(receiveData);

            udpClient.Close();
        }

        private void LoadData()
        {
            SendRequest("1");
            foreach (string line in response.Split(new[] {'\n'}))
            {
                string str = line.ToString();
                string[] data = str.Split(',');
                if (data.Length == 5)
                {
                    SchoolModel Schoolstudent = new SchoolModel
                    {
                        ID = int.Parse(data[0]),
                        Name = data[1],
                        Surname = data[2],
                        Age = int.Parse(data[3]),
                        IsBad = bool.Parse(data[4]),
                    };
                    if (!StudentsDict.ContainsKey(Schoolstudent.ID))
                    {
                        StudentsDict.Add(Schoolstudent.ID, Schoolstudent);
                        student.Add(Schoolstudent);
                    }
                }
            }
        }
        private void SaveData()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < student.Count; i++)
            {
                string stStr = $"{student[i].Name}, {student[i].Surname}, {student[i].Age}, {student[i].IsBad}";
                sb.AppendLine(stStr);
            }
            string requestData = sb.ToString();
            SendRequest("2" + "," + requestData);
        }
    }
}

