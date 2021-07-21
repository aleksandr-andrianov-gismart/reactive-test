using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ClientConnectionController : MonoBehaviour
{
    private TcpClient m_socketConnection = null;
    private Thread m_clientReceiveThread = null;

    private void Start()
    {
        ConnectToServerByTCP();
    }

    private void ConnectToServerByTCP()
    {
        try
        {
            m_clientReceiveThread = new Thread(new ThreadStart(ListenData));
            m_clientReceiveThread.IsBackground = true;
            m_clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("Exception " + e + " was raised on try to connect to the server.");
        }
    }

    private void ListenData()
    {
        try
        {
            m_socketConnection = new TcpClient("127.0.0.1", 9898);

            byte[] bytes = new byte[1024];

            while (true)
            {		
                using (NetworkStream stream = m_socketConnection.GetStream())
                {
                    int length;

                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var receivedData = new byte[length];
                        Array.Copy(bytes, 0, receivedData, 0, length);
                        
                        string serverMessage = Encoding.ASCII.GetString(receivedData);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception " + socketException + " was raised on listening.");
        }
    }

    public void SendMessageToServer(string command)
    {
        if (m_socketConnection == null) return;
        try
        {
            NetworkStream stream = m_socketConnection.GetStream();

            if (stream.CanWrite)
            {
                byte[] clientData = Encoding.ASCII.GetBytes(command);

                stream.Write(clientData, 0, clientData.Length);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception " + socketException + " was raised on sending message to server.");
        }
    }
}