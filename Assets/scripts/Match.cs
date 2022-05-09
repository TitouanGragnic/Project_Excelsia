using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Discovery;


namespace Mirror.Discovery
{
    public class Match : MonoBehaviour
    {

        [SerializeField] InputField room;
        [SerializeField] NetworkManager networkManager;
        private string ip_local;
        private string name;

        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        public void Join()
        {
            name = room.text;
            Debug.Log($"{name}");
            ip_local = GetLocalIPAddress();
            Debug.Log($"{ip_local}");
            string a = Decrypt(ip_local, name);
            Debug.Log($"{a}");
            networkManager.networkAddress = a;
            networkManager.StartClient();

        }
        public static string Decrypt(string ip, string name)
        {
            string _id = string.Empty;
            int x = 0;
            int j = 0;
            while (x < 2)
            {
                _id += ip[j];
                if (ip[j] == '.')
                {
                    x += 1;
                }
                j += 1;
            }
            for (int i = 0; i < name.Length; i++)
            {

                if ((char)name[i] < 60)
                {
                    _id += '.';
                }
                else
                {
                    _id += (char)((int)name[i] - name.Length + i - 20);
                }

            }
            return _id;
        }
        public static string GetRandomMatchID(string ip)
        {
            string _id = string.Empty;
            _id += (char)UnityEngine.Random.Range(64, 90);
            int x = 0;
            for (int i = 8; i < ip.Length; i++)
            {
                if (ip[i] == '.')
                {
                    _id += UnityEngine.Random.Range(1, 9);
                }
                if (ip[i] != '.')
                {
                    _id += (char)((int)ip[i] + ip.Length - i + 20);
                }
            }
            return _id;
        }
    }
}

