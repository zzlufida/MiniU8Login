/*
 * Created by SharpDevelop.
 * User: zzl
 * Date: 2016-04-19
 * Time: 15:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Sockets;

namespace MiniU8Login
{
	/// <summary>
	/// 端口扫描工具类.
	/// </summary>
	public class Scanner
	{

		string m_host;
		int m_port;

		public Scanner(string host, int port)
		{
			m_host = host;
			m_port = port;
		}

		public bool Scan()
		{
			//我们直接使用比较高级的TcpClient类
			TcpClient tc = new TcpClient();
			//设置超时时间
			tc.SendTimeout = tc.ReceiveTimeout = 1000;
			try {
				//Console.Write("Checking port: {0}", m_port);
				//尝试连接
				tc.Connect(m_host, m_port);
				if (tc.Connected) {
					//如果连接上，证明此端口为开放状态
					Console.WriteLine("Port {0} is Open", m_port.ToString().PadRight(6));
					//Program.openedPorts.Add(m_port);
					return true;
				}
			} catch (System.Net.Sockets.SocketException e) {
				//容错处理
				Console.WriteLine("Port {0} is closed", m_port.ToString().PadRight(6));
				return false;
				//Console.WriteLine(e.Message);
			} finally {
				tc.Close();
				tc = null;
				//Program.scannedCount++;
				//Program.runningThreadCount--;

				//Console.WriteLine(Program.scannedCount);

			}
			return false;
		}
    
	}
}
