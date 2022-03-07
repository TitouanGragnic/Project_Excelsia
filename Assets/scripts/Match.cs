using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Match : MonoBehaviour
{
    [SerializeField] InputField room;
    [SerializeField] NetworkManager networkManager;
    public string ip_local;
    public string name;



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
        for (int i = 0; i < ip.Length; i++)
        {
            if (i < 8)
            {
                _id += ip[i];
            }
            else
            {
                if (i == (ip.Length)-3)
                {
                    _id += '.';
                }
                if(i > ip.Length - 4)
                {
                    _id += (char)((int)name[i - 7]-65-ip.Length+i);
                }
                if (i < ip.Length - 4)
                {
                    _id += (char)((int)name[i - 6] - 65 - ip.Length + i);
                }
            }
        }
        return _id;
    }
    public static string GetRandomMatchID(string ip)
    {
        string _id = string.Empty;
        for (int i = 5; i < ip.Length; i++)
        {
            if ((char)((int)ip[i] + 65) != 'o')
            {
                if (i < 8)
                {
                    _id += ((int)ip[i] % (ip.Length - i));
                }
                else
                {
                    _id += (char)((int)ip[i] + 65 + ip.Length - i);
                }
            }
        }
        return _id;
    }
}
