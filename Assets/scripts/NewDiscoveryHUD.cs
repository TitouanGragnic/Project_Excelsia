using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Discovery
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkDiscoveryHUD")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-discovery")]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class NewDiscoveryHUD : MonoBehaviour
    {
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;

        public NetworkDiscovery networkDiscovery;

        public GUIContent Image;
        public GUIStyle Image1;

        public GUIContent Start;
        public GUIStyle Start1;
        public GameObject screen;
        public bool ok = false;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active && screen.activeSelf)
                DrawGUI();

            //if (NetworkServer.active || NetworkClient.active)
                //StopButtons();
        }

        void DrawGUI()
        {
            if (ok)
            {
                GUILayout.BeginArea(new Rect(1000, 500, 900, 1500));
                GUILayout.BeginHorizontal();

                /*if (GUILayout.Button("Find Servers"))
                {
                    discoveredServers.Clear();
                    networkDiscovery.StartDiscovery();
                }*/

                GUILayout.EndHorizontal();

                // show list of found server
                Start.text = $"Discovered Servers [{discoveredServers.Count}]:";
                GUILayout.Label(Start, Start1, GUILayout.Width(400), GUILayout.Height(75));

                // servers
                scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

                foreach (ServerResponse info in discoveredServers.Values)
                {
                    Image.text = GetRandomMatchID(info.EndPoint.Address.ToString());
                    if (GUILayout.Button(Image, Image1, GUILayout.Width(500), GUILayout.Height(100)))
                    {
                        Connect(info);
                    }
                }


                GUILayout.EndScrollView();
                GUILayout.EndArea();
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
                if (ip[i] == '.')
                {
                    _id += ip[i - 1];
                }
                if (ip[i] != '.')
                {
                    _id += (char)((int)ip[i] + ip.Length - i + 20);
                }
            }
            return _id;
        }

        void StopButtons()
        {
            GUILayout.BeginArea(new Rect(10, 40, 100, 25));

            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Host"))
                {
                    NetworkManager.singleton.StopHost();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {
                    NetworkManager.singleton.StopClient();
                    networkDiscovery.StopDiscovery();
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    NetworkManager.singleton.StopServer();
                    networkDiscovery.StopDiscovery();
                }
            }

            GUILayout.EndArea();
        }

        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

        public void search()
        {
            ok = true;
            networkDiscovery.StartDiscovery();
        }
    }
}
