using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace l4server
{
    internal class Program
    {
        static string GetTime(string timeZone, string format)
        {
            DateTime dateTime = DateTime.UtcNow;
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            DateTime dateTimeInTimeZone = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
            return "Часовий пояс: " + timeZone + "\nДата та час: " + dateTimeInTimeZone.ToString(format);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Сервер запущено.");
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                
                while (true)
                {
                    Socket handler = sListener.Accept();
                    string data = null;
                    
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    if (data == "End|")
                    {
                        Console.WriteLine("Сервер завершив з’єднання з клієнтом.");
                        break;
                    }
                    else
                    {
                        string[] datas = data.Split('|');
                        string reply = GetTime(datas[0], datas[1]);
                        byte[] rpl = Encoding.UTF8.GetBytes(reply);
                        handler.Send(rpl);
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
            }
        }
    }
}
