/*
 * 由SharpDevelop创建。
 * 用户： zzl
 * 日期: 2017-10-18
 * 时间: 14:03
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using UFSoft.U8.Framework.LoginContext;
using ufsoft.VariousTech.securityAndCreditIdentity;

namespace MiniU8Login
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class MU8login
	{
		string m_strServer;
		string m_strUser;
		string m_strPassword;
		string m_strAccount;
		string m_strDate;
		string m_strDataSource;
		bool bServerOK = false;
		CreditIdentity credit;
		UserData objUserData;
		List<Object> retDataSources;
		
		public string LoginServer {
			get {
				return m_strServer;
			}
			set {
				m_strServer = value;
			}
		}
		public string LoginUser {
			get {
				return m_strUser;
			}
			set {
				m_strUser = value;
			}
		}
		public string LoginAccount {
			get {
				return m_strAccount;
			}
			set {
				m_strAccount = value;
			}
		}
		public string LoginDate {
			get {
				return m_strDate;
			}
			set {
				m_strDate = value;
			}
		}
		public string LoginDataSource {
			get {
				return m_strDataSource;
			}
			set {
				m_strDataSource = value;
			}
		}
		
		public MU8login()
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN", false);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", false);
		}
		public 	MU8login(string strServer, //服务器地址
			string strUser, //用户名
			string strPassword,//密码
			string strDate, //登录日期
			string strDataSource)//数据源 XXX@001
		{
			m_strServer = strServer;
			m_strUser = strUser;
			m_strPassword = strPassword;
			//m_strAccount = strAccount;
			m_strDate = strDate;
			m_strDataSource = strDataSource;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN", false);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", false);
		}
		
		public string login (){
			
			objUserData = new UserData();
			CommonAPI.LoginInfo = objUserData;
			objUserData.uuid = Guid.NewGuid().ToString();
			objUserData.WorkStationSerial = Tools.generateWorkStationGUID();
			
			SymmetricCryptography symmetricCryptography_0;//加密器
			symmetricCryptography_0 = new SymmetricCryptography(SymmetricProvider.Rijndael);
			byte[] key = null;
			byte[] iV = null;
			
			//系统默认是根据uuid 生成一个算法key的,或者简单理解为uuid就是加密和解密的密码
			CommonAPI.GenerateKey(objUserData.uuid, ref key, ref iV, SymmetricProvider.Rijndael);
			symmetricCryptography_0.Key = key;
			symmetricCryptography_0.IV = iV;
				
			
			objUserData.UserId = symmetricCryptography_0.EncryptString(m_strUser); //demo
			objUserData.Password = symmetricCryptography_0.EncryptString(m_strPassword);//DEMO
			objUserData.AppServer = m_strServer;
			objUserData.LanguageID = "zh-CN";
			objUserData.cSubID = "DP";
			objUserData.operDate = m_strDate;
	
			objUserData.DataSource = m_strDataSource;
			
			Debugger.Log(0, null, UserDataTools.userData2String(objUserData));
			var paras = new CommonParameters();
			var loginHeader = new LoginedDataHead();
			loginHeader.ChangePwd = false;
			loginHeader.KickOutWorkStation = false;
			loginHeader.NewPassword = symmetricCryptography_0.EncryptString("");//DEMO
			
			paras.Para1 = "TARGET\\ENTERPRISEPORTAL.EXE";
			paras.Para2 = true;
			paras.Para3 = loginHeader;
			
			try {
				
				credit.BFDispatch("Authenticate", objUserData, ref paras);
					
				if (!string.IsNullOrEmpty(paras.Para1)) {
					return paras.Para1;
				}
				

			} catch (CustomError cEx) {
				//MessageBox.Show(cEx.Message);
				throw;
			} finally {
				
			}
			

		}
		
		public List<object> getAllDataSource(string strServer)
		{
			
			if (Tools.isComputerName(strServer) || Tools.IsIP(strServer)) {
	
				var objScan = new Scanner(strServer, 11520);
				var bScanRet = objScan.Scan();
				if (bScanRet) {
					//e.Result = "录入合法,链接成功";
					bServerOK = true;
					credit = new CreditIdentity(strServer, "11520");
					retDataSources = credit.GetDataSource(true);
					try {
						var paras = new CommonParameters();
						credit.BFDispatch("serverversion", null, ref paras);
						//e.Result = e.Result + ";版本U8v" + paras.Para1;
						return retDataSources;
					} catch (CustomError cEx) {
						//e.Result = e.Result + cEx.Message;
						throw  cEx;
					}
				} else {
					//e.Result = "失败";
					bServerOK = false;
					return null;
				}
			} else {
				//MessageBox.Show("TxtServerValidating");
				//e.Cancel = true;
				return null;
			}
	
		}
		
	}
}