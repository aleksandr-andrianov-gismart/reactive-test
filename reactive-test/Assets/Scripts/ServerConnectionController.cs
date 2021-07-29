using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UniRx;
using UnityEngine;

public class ServerConnectionController : MonoBehaviour
{
    [SerializeField] private ServerSceneManager m_sceneController = null;

    private TcpListener m_tcpListener = null;
    private Thread m_tcpThreadListener = null;
    private TcpClient m_connectedTcpClient = null;

    private readonly string m_lightSecretKeyHash = "light".GetHashCode().ToString();
    private readonly string m_explosionSecretKeyHash = "explode".GetHashCode().ToString();

    private const string m_localIP = "127.0.0.1";
    private const int m_testPort = 9898;

    private void Start()
    {
        m_tcpThreadListener = new Thread(new ThreadStart(ListenClientRequests))
        {
            IsBackground = true
        };
        m_tcpThreadListener.Start();
    }

    private void ListenClientRequests()
    {
        try
        {
            m_tcpListener = new TcpListener(IPAddress.Parse(m_localIP), m_testPort);
            m_tcpListener.Start();

            byte[] bytes = new byte[1024];
            while (true)
            {
                using (m_connectedTcpClient = m_tcpListener.AcceptTcpClient())
                {
                    using (NetworkStream stream = m_connectedTcpClient.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            string clientMessage = Encoding.ASCII.GetString(incommingData);

                            if (clientMessage == m_lightSecretKeyHash)
                            {
                                MainThreadDispatcher.Post(_ => m_sceneController.ChangeLightIndex(), null);

                                continue;
                            }
                            if (clientMessage == m_explosionSecretKeyHash)
                            {
                                MainThreadDispatcher.Post(_ => m_sceneController.EmitParticles(), null);
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
}