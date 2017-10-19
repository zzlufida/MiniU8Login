/*
 * 由SharpDevelop创建。
 * 用户： zzl
 * 日期: 2017-10-18
 * 时间: 14:14
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using ufsoft.VariousTech.securityAndCreditIdentity;

namespace MiniU8Login
{
	/// <summary>
	/// Description of Tools.
	/// </summary>
	public static class Tools
	{
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetCurrentProcessId();
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int ProcessIdToSessionId(int int_0, ref int int_1);
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetCurrentThreadId();
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetProcessHeap();
		[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetForegroundWindow(IntPtr intptr_0);
		[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetActiveWindow(IntPtr intptr_0);
		[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void SwitchToThisWindow(IntPtr intptr_0, bool bool_0);
		
		
		public static string generateWorkStationGUID()
		{
			var u8SystemInfo = new U8SystemInfo();
			string str = u8SystemInfo.WorkStationGUID();
			return str + Environment.MachineName + "@" + smethod_4();
		}
		
	
		

		
		private static int smethod_4()
		{
			int result = 0;
			try {
				if (ProcessIdToSessionId(GetCurrentProcessId(), ref result) == 0) {
					result = 0;
				}
			} catch (Exception e) {
			}
			return result;
		}
		public static string PrettyXml(string xml)
		{
			var stringBuilder = new StringBuilder();

			var element = XElement.Parse(xml);

			var settings = new XmlWriterSettings();
			settings.OmitXmlDeclaration = true;
			settings.Indent = true;
			settings.NewLineOnAttributes = true;

			using (var xmlWriter = XmlWriter.Create(stringBuilder, settings)) {
				element.Save(xmlWriter);
			}

			return stringBuilder.ToString();
		}
		/// <summary>
		/// 验证IP地址是否合法
		/// </summary>
		/// <param name="ip">要验证的IP地址</param>        
		public static bool IsIP(string ip)
		{
			//如果为空，认为验证合格
			if (string.IsNullOrEmpty(ip)) {
				return true;
			}

			//清除要验证字符串中的空格
			ip = ip.Trim();

			//模式字符串
			const string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

			//验证
			return Regex.IsMatch(ip, pattern);
		}
		
		/// <summary>
		/// 验证及计算机名是否合法
		/// </summary>
		/// <param name="computername">要验证的计算机名</param>    
		public  static  bool isComputerName(string computername)
		{
			//如果为空，认为验证合格
			if (string.IsNullOrEmpty(computername)) {
				return true;
			}
			return Regex.IsMatch(computername, @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$");
		
		}
	
	}
}
