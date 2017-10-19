using System;
using System.Collections;
using System.Collections.Generic;
using UFSoft.U8.Framework.Login.BO;
using UFSoft.U8.Framework.LoginContext;
using UFSoft.U8.Framework.ServiceMarkable;
using ufsoft.VariousTech.securityAndCreditIdentity;
namespace MiniU8Login
{
	[ServiceVisible(Visible = true, ServiceType = ServiceTypes.SingleCall)]
	public class CreditIdentity
	{
		private I_CreditIdentity _obj;
		//private UFSoft.U8.Framework.InvokeService.LogicalContext ;
		public CreditIdentity(string serverName,string protocolPort)
		{
			try
			{

				string url =string.Format("tcp://{0}:{1}/UFSoft.U8.Framework.Login.BO.CreditIdentity.rem", serverName, protocolPort);
				//this._context = new LogicalContext();
				this._obj = (I_CreditIdentity)Activator.GetObject(typeof(I_CreditIdentity), url);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public SortedList GetAppserver()
		{
			SortedList result;
			try
			{
				SortedList appserver = this._obj.GetAppserver();
				result = appserver;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public List<object> GetDataSource()
		{
			List<object> result;
			try
			{
				List<object> dataSource = this._obj.GetDataSource();
				result = dataSource;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public string GetDefaultConnstring(string LanguageID)
		{
			string result;
			try
			{
				string defaultConnstring = this._obj.GetDefaultConnstring(LanguageID);
				result = defaultConnstring;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public List<object> GetDataSource(bool IsGetAccInfos)
		{
			List<object> result;
			try
			{
				List<object> dataSource = this._obj.GetDataSource(IsGetAccInfos);
				result = dataSource;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public List<object> GetDataSource(bool IsGetAccInfos, string UserId)
		{
			List<object> result;
			try
			{
				List<object> dataSource = this._obj.GetDataSource(IsGetAccInfos, UserId);
				result = dataSource;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public UserData GetConfigFromMem(string userToken)
		{
			UserData result;
			try
			{
				UserData configFromMem = this._obj.GetConfigFromMem(userToken);
				result = configFromMem;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public bool IsDemoToSubSystem(string userToken, string SubID)
		{
			bool result;
			try
			{
				bool flag = this._obj.IsDemoToSubSystem(userToken, SubID);
				result = flag;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public string getAppConnstring(string userToken, string SubID)
		{
			string result;
			try
			{
				string appConnstring = this._obj.getAppConnstring(userToken, SubID);
				result = appConnstring;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public string GetEndSubAuthsWithAuthID(string userToken, string AuthId, bool IsAdmin, string SubId)
		{
			string result;
			try
			{
				string endSubAuthsWithAuthID = this._obj.GetEndSubAuthsWithAuthID(userToken, AuthId, IsAdmin, SubId);
				result = endSubAuthsWithAuthID;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		public void SubLogout(string TaskId, string AppServer, string WorkStationSerial, string SubId, string uuid)
		{
			try
			{
				this._obj.SubLogout(TaskId, AppServer, WorkStationSerial, SubId, uuid);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void BFDispatch(string MethodName, UserData userInfo, ref CommonParameters paras)
		{
			try
			{
				this._obj.BFDispatch(MethodName, userInfo, ref paras);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
