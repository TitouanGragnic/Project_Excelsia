using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            txt.text = "ip_local : " + ip_local;
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
    }
}