using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultuTread
{
    class Lab_1_4
    {
        private const int Port = 11002;
        private static int _requestCount = 0;
        private static int maxListenAmount = 100;
        private static int threadDelay = 2;

        public static async Task Run()
        {
            Console.WriteLine("MultiThread server starting");

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, Port));
            listener.Listen(maxListenAmount);

            try
            {
                while (true)
                {
                    var socket = await listener.AcceptAsync();
                    _ = processSocket(socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Program interrupted: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Program is over");
                listener.Close();
            }
        }

        static async Task processSocket(Socket socket)
        {
            try
            {
                int requestId = Interlocked.Increment(ref _requestCount);
                Console.WriteLine($"Processing request #{requestId} in thread #{Environment.CurrentManagedThreadId}");

                var buffer = new byte[1024];
                int received = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

                string request = Encoding.ASCII.GetString(buffer, 0, received);
                Console.WriteLine($"Gotten: {request}");

                Console.WriteLine("Processing request...");
                await Task.Delay(threadDelay);
                Console.WriteLine("Processing is over");

                string response = $"Response #{requestId}: {request.ToUpper()}";
                byte[] responseData = Encoding.ASCII.GetBytes(response);
                await socket.SendAsync(new ArraySegment<byte>(responseData), SocketFlags.None);
                Console.WriteLine("Response has been sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gotten exception in task: {ex.Message}");
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
