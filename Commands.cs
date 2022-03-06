using JustCli;
using JustCli.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check
{
    [Command(
    "crpt",
    "Интегральная проверка доступности сервиса приемной точки оператора ГИС МТ от ККТ. По умолчанию crpt.ofd.ru:7000, таймаут 10000 мс.")]
    class Crpt : ICommand
    {
        [CommandArgument("s", "server", Description = "Сервер", DefaultValue = "crpt.ofd.ru")]
        public string Server { get; set; }

        [CommandArgument("p", "port", Description = "Порт", DefaultValue = "7000")]
        public int Port { get; set; }

        [CommandArgument("t", "timeout", Description = "Таймут (мс)", DefaultValue = "10000")]
        public int Timeout { get; set; }

        [CommandOutput]
        public IOutput Output { get; set; }

        public int Execute()
        {
            var buffer = new byte[]
            {
                0xDD, 0x80, 0xCA, 0xA1, // magic word DD80CAA1
                0x82, 0xA2, // s ver
                0x00, 0x01, // a ver
                0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, // fnId
                0x00, 0x00, // entry len 
                0x10, 0x00, // flags
                0x00, 0x00  // entry crc
            };

            var result = Common.SendPacket(buffer, Server, Port, Timeout);
            return result;
        }
    }

    [Command(
    "crptonlyofd",
    "Интегральная проверка доступности сервиса приемной точки ОФД от ККТ. По умолчанию сервер crpt.ofd.ru:7000, таймаут 10000 мс.")]
    class CrptOnlyOfd : Crpt
    {
        public new int Execute()
        {
            var buffer = new byte[]
            {
                0xDD, 0x80, 0xCA, 0xA1, // magic word DD80CAA1
                0x82, 0xFB, // s ver
                0x00, 0x01, // a ver
                0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, // fnId
                0x00, 0x00, // entry len 
                0x10, 0x00, // flags
                0x00, 0x00  // entry crc
            };

            var result = Common.SendPacket(buffer, Server, Port, Timeout);
            return result;
        }
    }
    [Command(
        "okp",
        "Проверка работоспособности сервера АС ОКП. По умолчанию prod01.okp-fn.ru:26101, таймаут 30000 мс.")]
    class ProdFn : ICommand
    {
        [CommandArgument("s", "server", Description = "Сервер", DefaultValue = "prod01.okp-fn.ru")]
        public string Server { get; set; }

        [CommandArgument("p", "port", Description = "Порт", DefaultValue = "26101")]
        public int Port { get; set; }

        [CommandArgument("t", "timeout", Description = "Таймут (мс)", DefaultValue = "30000")]
        public int Timeout { get; set; }

        [CommandOutput]
        public IOutput Output { get; set; }

        public int Execute()
        {
            var buffer = new byte[]
            {
                0xDD, 0x80, 0xCA, 0xA1, // magic word DD80CAA1
                0x82, 0xA1, 0x00, 0x01, // S/P ver
                0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, 0x39, // fnId
                0x00, 0x00, // entry len 
                0x10, 0x00, // flags
                0x00, 0x00  // entry crc
            };

            var result = Common.SendPacket(buffer, Server, Port, Timeout);
            return result;
        }
    }
}
