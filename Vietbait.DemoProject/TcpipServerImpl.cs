using System;
using System.Net.Sockets;
using System.Reflection;
using Vietbait.Lablink.Utilities;
using Vietbait.Server.TCPIPServer;

namespace Vietbait.DemoProject
{
    internal class TcpipServerImpl : TcpipServer
    {
        private static double _number;
        private static int _times;

        public override void OnBeginReciveData(TcpClient client)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
        }

        public override void OnClientConnected(TcpClient client)
        {
            string serverIP = client.Client.LocalEndPoint.ToString();
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName + serverIP);
        }

        public override void OnClientDisconnected(TcpClient client)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
            _number = 0;
            _times = 0;
        }

        public override void OnEndReciveData(TcpClient client)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
        }

        private void WriteConsoleLog(string funName)
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy - H:mm:s:ms  ") + funName);
        }

        public override void OnStartServerFalse(int port, string exceptionString)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
        }

        public override void OnStartServerSuccessfull(int port, string exceptionString)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
        }

        public override void OnStopServerSuccessfull(int port, string exceptionString)
        {
            string funName = MethodBase.GetCurrentMethod().Name;
            WriteConsoleLog(funName);
        }

        public override void OnIncommingData(DataHeader dataHeader, byte[] data)
        {
            _number += data.Length;
            _times += 1;
            string funName = MethodBase.GetCurrentMethod().Name;

            WriteConsoleLog(_times + ":" + funName + " " + dataHeader + " " + _number);
        }
    }
}