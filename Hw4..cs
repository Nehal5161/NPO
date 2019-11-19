using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hw4
{
    

    class Chatroom
    {
        List<Thread> ConnectionHandlers = new List<Thread>();
        Dictionary<Socket, string> Connection = new Dictionary<Socket, string>();

        public delegate void EventMessage(Socket s);
        public event EventMessage SendGlobalMessage;
        public void AddConnection(Socket s)
        {
            string username = null;

            s.Send(ASCIIEncoding.ASCII.GetBytes("Enter a username for the chatroom: "));
            Thread t = new Thread(new ParameterizedThreadStart(ReadData));
            t.Start(s);

            Connection.Add(s, username);
            Console.WriteLine(username + " has entered that chatroom");

        }

        public Chatroom()
        {
            // new Thread(ReadData).Start();
            Console.WriteLine("Now reading data");
        }

        public void Message(byte[] data)
        {
            foreach (Socket s in Connection.Keys)
            {
                s.Send(data);
            }

        }

        public void PingConnections()
        {

        }

        public void ReadData(object socket)
        {
           
            Socket s = (Socket)socket;
            byte[] b = new byte[1024];
            int k = s.Receive(b);
            string username = "";
            username = Encoding.ASCII.GetString(b, 0, k);
           
            

            while (true)
            {
                try
                {
                    k = s.Receive(b);
                    for (int i = 0; i < k; i++)
                    {
                        string user_msg = Encoding.ASCII.GetString(b, 0, k);
                        
                    }
                    Console.WriteLine(username + " {0} ", Encoding.ASCII.GetString(b, 0, k));



                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Error code: {1}.", e.Message);
                }
            }

        }


    }
    class Server : TcpListener
    {
        public void handleConnections()
        {
            while (true)
            {
                Socket s = AcceptSocket();
                if (OnSocketAccept != null)
                {
                    OnSocketAccept(s);
                }
            }

        }

        public Server() : base(IPAddress.Parse("127.0.0.1"), 245)
        {
            Start();
            new Thread(handleConnections).Start();
            Console.WriteLine("Connected to the server\n");

        }

        public delegate void EventConnection(Socket s);
        public event EventConnection OnSocketAccept;

    }
    class Program
    {
        static void Main(string[] args)
        {
            Server svr = new Server();
            Chatroom room = new Chatroom();

            svr.OnSocketAccept += room.AddConnection;

            while (true)
            {
                Thread.Sleep(100);
            }

        }
    }
}

