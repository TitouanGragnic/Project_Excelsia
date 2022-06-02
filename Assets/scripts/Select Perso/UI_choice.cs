using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Security.Cryptography;

namespace scripts
{
    public class UI_choice : MonoBehaviour
    {
        public bool state;
        [SerializeField]
        GameObject Exelcia;
        [SerializeField]
        Text txt;
        [SerializeField]
        GameObject canvas;
        [SerializeField]
        NetworkManager networkManager;

        public string ip_local;
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


        void Start()
        {
            state = true;
            ip_local = GetLocalIPAddress();
            string a = GetRandomMatchID(ip_local);
            txt.text = "room ID : " + a;
        }

        // Update is called once per frame
        void Update()
        {
            if (state)
            {
                state = !GameManager.GetStateStart();
            }  
            else
            {
                Exelcia.SetActive(false);
                canvas.SetActive(false);
            }
        }
        public static string GetRandomMatchID(string ip)
        {
            string _id = string.Empty;
            int x = 0;
            int j = 0;
            while (x < 2)
            {
                if (ip[j] == '.')
                {
                    x += 1;
                }
                j += 1;
            }
            for (int i = j; i < ip.Length; i++)
            { 
                if(ip[i] == '.')
                {
                    _id += ip[i - 1];
                }
                if(ip[i] != '.')
                {
                    _id += (char)((int)ip[i] + ip.Length - i + 20);
                }
            }
            return _id;
        }
    }
    public static class MatchExtensions
    {
        public static Guid ToGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hashBytes = provider.ComputeHash(inputBytes);

            return new Guid(hashBytes);
        }
    }
}