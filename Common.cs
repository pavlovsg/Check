using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Check
{
    public static class Common
    {
        public static int SendPacket(byte[] buffer, string server, int port, int timeout)
        {
            Console.WriteLine($"Сформирован тестовый пакет к отправке размером {buffer.Length} байт:");
            Console.WriteLine(BitConverter.ToString(buffer));

            Console.WriteLine($"Попытка соединения с {server}:{port}");
            try
            {
                var client = new TcpClient(server, port);
                var stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine($"Тестовый пакет отправлен к {server}:{port}");
                Console.WriteLine($"Ожидаем ответ до таймаута ({timeout} мс)...");

                Thread.Sleep(timeout);
                var dataAvailable = stream.DataAvailable;
                if (dataAvailable == false)
                {
                    Console.WriteLine("Данные не поступили.");
                    client.Close();
                    Console.WriteLine("Соединение закрыто.");
                    return -1;
                }
                var bytesAvailable = client.Available;
                var responseBuffer = new byte[bytesAvailable];
                var bytesCount = stream.Read(responseBuffer, 0, bytesAvailable);

                Console.WriteLine($"Пришёл ответ размером {bytesAvailable} байт:");
                Console.WriteLine(BitConverter.ToString(responseBuffer));

                var correctResponse = new byte[buffer.Length];
                Array.Copy(buffer, correctResponse, buffer.Length);
                correctResponse[correctResponse.Length - 4] = 0;
                Console.WriteLine(BitConverter.ToString(correctResponse));
                Console.WriteLine("^^^ Корректный ответ должен быть такой ^^^");
                if (responseBuffer.SequenceEqual(correctResponse))
                {
                    Console.WriteLine("Ответ корректный.");
                    client.Close();
                    return 1;
                }
                else
                {
                    Console.WriteLine("Ответ некорректный.");
                    client.Close();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение:");
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
