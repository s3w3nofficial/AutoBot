using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static TcpClient _client;
        static StreamReader _sr;
        static StreamWriter _sw;

        static void Main(string[] args)
        {
            Console.WriteLine("Raspberry");
            string ip;
            string input = Console.ReadLine();
            if (input == "1")
            {
                ip = "192.168.137.46";
            }
            else
            {
                ip = Console.ReadLine();
            }
            
            try
            {
                Call(ip);
            }
            catch
            {
                Console.WriteLine("Connection forbidden.");
                Thread.Sleep(15000);
                Call(ip);
            }
        }
        
        public static void Call(string ip)
        {
            try
            {
                _client = new TcpClient(ip, 5901);
            }
            catch
            {
                Console.WriteLine("Server down.");
                Thread.Sleep(1500);
                Call(ip);
            }
            _sr = new StreamReader(_client.GetStream());
            _sw = new StreamWriter(_client.GetStream());
            while (true)
            {
                try
                {
                    var controller = new SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.One);
                    Thread.Sleep(200);
                    _sw.WriteLine(Controller(controller));
                    _sw.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Connection Lost :(");
                    Thread.Sleep(1500);
                    Call(ip);
                }
            }
        }
        
        public static string Controller(SharpDX.XInput.Controller controller)
        {
            var tmpX = 0;
            var tmpY = 0;      
            if (controller.IsConnected)
            {
                var state = controller.GetState();
                if (state.Gamepad.LeftThumbX < 0)
                {
                    tmpX = int.Parse((state.Gamepad.LeftThumbX / -327.68).ToString());
                }
                else if (state.Gamepad.LeftThumbX >= 0)
                {
                    tmpX = int.Parse((state.Gamepad.LeftThumbX / 327.67  * -1).ToString());
                }
                if (state.Gamepad.LeftThumbY < 0)
                {
                    tmpY = int.Parse((state.Gamepad.LeftThumbY / -327.68 * -1).ToString());
                }
                else if (state.Gamepad.LeftThumbY >= 0)
                {
                    tmpY = int.Parse((state.Gamepad.LeftThumbY / 327.67).ToString());
                }
                

                Console.WriteLine("{0}, {1}% {2}, {3}%", state.Gamepad.LeftThumbX, tmpX.ToString(), state.Gamepad.LeftThumbY,  tmpY.ToString());
                return tmpX.ToString() + "," + tmpY.ToString();
            }
            return null;
        }
    }

}