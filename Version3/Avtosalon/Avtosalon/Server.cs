using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avtosalon {
    static class Server {
        static string serverName = "localhost"; // Адрес сервера
        static string userName = "root";//
        static string dbName = "Avtosalon";
        static string port = "3306"; // Порт для подключения
        static string password = "admin"; // Пароль для подключения

        public static string connStr = "server=" + serverName +
                   ";user=" + userName +
                   ";database=" + dbName +
                   ";port=" + port +
                   ";password=" + password + ";";
    }
}

