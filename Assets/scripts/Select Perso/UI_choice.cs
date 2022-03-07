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
            txt.text = "ip_local : " + a;
        }

        // Update is called once per frame
        void Update()
        {
            if (state)
                state = !GameManager.GetStateStart();
            else
            {
                Exelcia.SetActive(false);
                canvas.SetActive(false);
            }
        }
        public static string GetRandomMatchID(string ip)
        {
            string _id = string.Empty;
            for (int i = 5; i < ip.Length; i++)
            {
                if((char)((int)ip[i]+65) != 'o')
                {
                    if (i < 8)
                    {
                        _id += ((int)ip[i] % (ip.Length-i));
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