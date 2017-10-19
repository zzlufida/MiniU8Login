/*
 * Created by SharpDevelop.
 * User: zzl
 * Date: 2016-04-15
 * Time: 16:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using UFSoft.U8.Framework.LoginContext;
using ufsoft.VariousTech.securityAndCreditIdentity;

namespace MiniU8Login
{
	/// <summary>
	/// Description of UserDataTools.
	/// </summary>
	public static  class UserDataTools
	{
		static int MAXLENOFFIELDNAME;
		static int MAXLENOFFIELDTYPE;
		static readonly Dictionary<string,Type> FIELDTYPEMAP = new Dictionary<string, Type>();
		static readonly Dictionary<string,FieldInfo> FIELDMAP = new Dictionary<string, FieldInfo>();
		public static string UserDataFormatString;
		
		
		public static bool IsContainField(string field)
		{
			
			return 	FIELDTYPEMAP.ContainsKey(field);
		}
		
		public static Type GetFieldType(string field)
		{
			return FIELDTYPEMAP.ContainsKey(field) ? FIELDTYPEMAP[field] : null;
		}
		
		static void initUserDataType()
		{
			MAXLENOFFIELDNAME = 0;
			MAXLENOFFIELDTYPE = 0;
			var userDataType = typeof(UserData);
	
			foreach (var field in userDataType.GetFields()) {
				if (MAXLENOFFIELDNAME < field.Name.Length)
					MAXLENOFFIELDNAME = field.Name.Length;
				if (MAXLENOFFIELDTYPE < field.FieldType.Name.Length)
					MAXLENOFFIELDTYPE = field.FieldType.Name.Length;
				
				FIELDTYPEMAP.Add(field.Name, field.FieldType);
				FIELDMAP.Add(field.Name, field);
			}
			MAXLENOFFIELDNAME++;
			MAXLENOFFIELDTYPE++;
			UserDataFormatString = string.Format("{{0,-{0}}},{{1,-{1}}}", MAXLENOFFIELDNAME, MAXLENOFFIELDTYPE);

		}
		static UserDataTools()
		{
			initUserDataType();
		}
		// UFSoft.U8.Framework.Login.UI.clsLogin
		public static UserData DeUserData(string cryptuuid, string s, string string_17)
		{
			UserData userData = null;
			if (s.Length != 0) {
				byte[] iV = null;
				byte[] key = null;
				SymmetricCryptography symm = new SymmetricCryptography(SymmetricProvider.Rijndael);
				CommonAPI.GenerateKey(cryptuuid, ref key, ref iV, SymmetricProvider.Rijndael);
				symm.Key = key;
				symm.IV = iV;
				StringReader stringReader = new StringReader(s);
				XmlTextReader xmlTextReader = CustomTypeAlias.CreateXmlReader(stringReader);
				userData = new UserData();
				while (xmlTextReader.Read()) {
					if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "Entry") {
						userData.UserId = symm.DecryptString(xmlTextReader.GetAttribute("user"));
						userData.Password = symm.DecryptString(xmlTextReader.GetAttribute("data"));
						userData.AccID = symm.DecryptString(xmlTextReader.GetAttribute("accid"));
						userData.AppServer = symm.DecryptString(xmlTextReader.GetAttribute("appserver"));
						userData.cSubID = symm.DecryptString(xmlTextReader.GetAttribute("subid"));
						userData.iYear = symm.DecryptString(xmlTextReader.GetAttribute("iyear"));
						userData.ConnString = symm.DecryptString(xmlTextReader.GetAttribute("connstring"));
						userData.operDate = symm.DecryptString(xmlTextReader.GetAttribute("operdate"));
						userData.DataSource = symm.DecryptString(xmlTextReader.GetAttribute("datasource"));
						userData.LanguageID = symm.DecryptString(xmlTextReader.GetAttribute("languageid"));
						userData.WorkStationSerial = symm.DecryptString(xmlTextReader.GetAttribute("workstationserial"));
						userData.RightServer = symm.DecryptString(xmlTextReader.GetAttribute("rightserver"));
						userData.IsCompanyVer = bool.Parse(symm.DecryptString(xmlTextReader.GetAttribute("iscompanyver")));
						userData.SecondConnString = (Hashtable)CustomXmlSerializer.Deserialize(symm.DecryptString(xmlTextReader.GetAttribute("secondconnstring")), "Hashtable");
						userData.EmployeeId = symm.DecryptString(xmlTextReader.GetAttribute("employeeid"));
						userData.IsAdmin = bool.Parse(symm.DecryptString(xmlTextReader.GetAttribute("isadmin")));
						userData.UserName = symm.DecryptString(xmlTextReader.GetAttribute("username"));
						userData.AccName = symm.DecryptString(xmlTextReader.GetAttribute("accname"));
						userData.EntTypeID = symm.DecryptString(xmlTextReader.GetAttribute("enttypeid"));
						userData.iMonth = int.Parse(symm.DecryptString(xmlTextReader.GetAttribute("imonth")));
						userData.AppServerSerial = symm.DecryptString(xmlTextReader.GetAttribute("appServerserial"));
						userData.Roles = symm.DecryptString(xmlTextReader.GetAttribute("roles"));
						userData.ProtocolPort = (Hashtable)CustomXmlSerializer.Deserialize(symm.DecryptString(xmlTextReader.GetAttribute("protocolport")), "protocolport");
						userData.BarCode = symm.DecryptString(xmlTextReader.GetAttribute("barcode"));
						userData.Customer = symm.DecryptString(xmlTextReader.GetAttribute("customer"));
						userData.AuthenMode = int.Parse(symm.DecryptString(xmlTextReader.GetAttribute("authenmode")));
						userData.AuthenExtraInfo = symm.DecryptString(xmlTextReader.GetAttribute("authenextrainfo"));
						userData.IndustryType = symm.DecryptString(xmlTextReader.GetAttribute("industrytype"));
						userData.iBeginYear = symm.DecryptString(xmlTextReader.GetAttribute("ibeginyear"));
						userData.AIOServer = symm.DecryptString(xmlTextReader.GetAttribute("aiosrv"));
						
						if (xmlTextReader.GetAttribute("crmsrv") != null && FIELDMAP.ContainsKey("CrmServer")) {
							//userData.CrmServer = symm.DecryptString(xmlTextReader.GetAttribute("crmsrv"));
						
							FIELDMAP["CrmServer"].SetValue(userData, symm.DecryptString(xmlTextReader.GetAttribute("crmsrv")));
						}
						
						
						if (xmlTextReader.GetAttribute("utusrv") != null && FIELDMAP.ContainsKey("UTUServer")) {
							//userData.UTUServer = symm.DecryptString(xmlTextReader.GetAttribute("utusrv"));
						
							FIELDMAP["UTUServer"].SetValue(userData, symm.DecryptString(xmlTextReader.GetAttribute("utusrv")));
						}
						
						
						if (xmlTextReader.GetAttribute("remind") != null && FIELDMAP.ContainsKey("isRemind")) {
							FIELDMAP["isRemind"].SetValue(userData, bool.Parse(symm.DecryptString(xmlTextReader.GetAttribute("remind"))));	
						}
						if (xmlTextReader.GetAttribute("usermode") != null && FIELDMAP.ContainsKey("UserMode")) {
							FIELDMAP["UserMode"].SetValue(userData, int.Parse(symm.DecryptString(xmlTextReader.GetAttribute("usermode"))));	
						}
						if (xmlTextReader.GetAttribute("sps") != null && FIELDMAP.ContainsKey("ValidateSPS")) {
							FIELDMAP["ValidateSPS"].SetValue(userData, bool.Parse(symm.DecryptString(xmlTextReader.GetAttribute("sps"))));	
						}
						if (xmlTextReader.GetAttribute("sysdate") != null && FIELDMAP.ContainsKey("sysdate")) {
							FIELDMAP["sysdate"].SetValue(userData, symm.DecryptString(xmlTextReader.GetAttribute("sysdate")));
						}
						
						if (string.IsNullOrEmpty(string_17)) {
							userData.Auditor = new AuditorContext {
								AuditorId = userData.UserId,
								AuditorName = userData.UserName
							};
						} else {
							userData.Auditor = (AuditorContext)CustomXmlSerializer.Deserialize(symm.DecryptString(string_17), "AuditorContext");
						}

						xmlTextReader.Close();
						stringReader.Close();
					}
				}
			}
			userData.uuid = cryptuuid;
			return userData;
		}

		public static string userData2String(UserData userData)
		{
			var bStrFormat = new StringBuilder();
			var fieldList = new List<string>(new [] {"UserId", "Password", "AccID", "iYear", "cSubID", "AppServer", "uuid",
				"DataSource", "ConnString", "operDate", "WorkStationSerial", "RightServer",
				"LanguageID", "IsCompanyVer"
			});
			var i = 0;
			
			bStrFormat.AppendLine(string.Format("UserId            string          登录名            :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("Password          string          密码              :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("AccID             string          帐套号            :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("iYear             string          年度              :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("cSubID            string          模块              :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("AppServer         string          应用服务器        :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("uuid              string          加密UUID          :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("DataSource        string          数据源            :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("ConnString        string          数据库连接字符串  :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("operDate          string          登录日期          :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("WorkStationSerial string          WorkStationSerial :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("RightServer       string          权限服务器        :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("LanguageID        string          语言              :{{{0}}}", i++));
			bStrFormat.AppendLine(string.Format("IsCompanyVer      bool            IsCompanyVer      :{{{0}}}", i++));
			
			
			if (UserDataTools.IsContainField("SecondConnString")) {
				bStrFormat.AppendLine(string.Format("SecondConnString  Hashtable       SecondConnString  :{{{0}}}", i++));
				fieldList.Add("SecondConnString");
			}
			if (UserDataTools.IsContainField("EmployeeId")) {
				bStrFormat.AppendLine(string.Format("EmployeeId        string          职员              :{{{0}}}", i++));
				fieldList.Add("EmployeeId");
			}
			if (UserDataTools.IsContainField("EntTypeID")) {
				bStrFormat.AppendLine(string.Format("EntTypeID         string          EntTypeID         :{{{0}}}", i++));
				fieldList.Add("EntTypeID");
			}
			if (UserDataTools.IsContainField("IsAdmin")) {
				bStrFormat.AppendLine(string.Format("IsAdmin           bool            是否帐套管理员    :{{{0}}}", i++));
				fieldList.Add("IsAdmin");
			}
			if (UserDataTools.IsContainField("UserName")) {
				bStrFormat.AppendLine(string.Format("UserName          string          登录用户名        :{{{0}}}", i++));
				fieldList.Add("UserName");
			}
			if (UserDataTools.IsContainField("AccName")) {
				bStrFormat.AppendLine(string.Format("AccName           string          帐套名称          :{{{0}}}", i++));
				fieldList.Add("AccName");
			}
			if (UserDataTools.IsContainField("iMonth")) {
				bStrFormat.AppendLine(string.Format("iMonth            int             登录月份          :{{{0}}}", i++));
				fieldList.Add("iMonth");
			}
			if (UserDataTools.IsContainField("AppServerSerial")) {
				bStrFormat.AppendLine(string.Format("AppServerSerial   string          AppServerSerial   :{{{0}}}", i++));
				fieldList.Add("AppServerSerial");
			}
			if (UserDataTools.IsContainField("Roles")) {
				bStrFormat.AppendLine(string.Format("Roles             string          Roles             :{{{0}}}", i++));
				fieldList.Add("Roles");
			}
			if (UserDataTools.IsContainField("Auditor")) {
				bStrFormat.AppendLine(string.Format("Auditor           _AuditorContext Auditor           :{{{0}}}", i++));
				fieldList.Add("Auditor");
			}
			if (UserDataTools.IsContainField("ProtocolPort")) {
				bStrFormat.AppendLine(string.Format("ProtocolPort      Hashtable       ProtocolPort      :{{{0}}}", i++));
				fieldList.Add("ProtocolPort");
			}
			if (UserDataTools.IsContainField("BarCode")) {
				bStrFormat.AppendLine(string.Format("BarCode           string          加密狗号          :{{{0}}}", i++));
				fieldList.Add("BarCode");
			}
			if (UserDataTools.IsContainField("Customer")) {
				bStrFormat.AppendLine(string.Format("Customer          string          Customer          :{{{0}}}", i++));
				fieldList.Add("Customer");
			}
			if (UserDataTools.IsContainField("AuthenMode")) {
				bStrFormat.AppendLine(string.Format("AuthenMode        int             AuthenMode        :{{{0}}}", i++));
				fieldList.Add("AuthenMode");
			}
			if (UserDataTools.IsContainField("AuthenExtraInfo")) {
				bStrFormat.AppendLine(string.Format("AuthenExtraInfo   string          AuthenExtraInfo   :{{{0}}}", i++));
				fieldList.Add("AuthenExtraInfo");
			}
			if (UserDataTools.IsContainField("IndustryType")) {
				bStrFormat.AppendLine(string.Format("IndustryType      string          IndustryType      :{{{0}}}", i++));
				fieldList.Add("IndustryType");
			}
			if (UserDataTools.IsContainField("isSendIM")) {
				bStrFormat.AppendLine(string.Format("isSendIM          bool            isSendIM          :{{{0}}}", i++));
				fieldList.Add("isSendIM");
			}
			if (UserDataTools.IsContainField("iBeginYear")) {
				bStrFormat.AppendLine(string.Format("iBeginYear        string          iBeginYear        :{{{0}}}", i++));
				fieldList.Add("AuthenMode");
			}
			if (UserDataTools.IsContainField("AIOServer")) {
				bStrFormat.AppendLine(string.Format("AIOServer         string          AIOServer         :{{{0}}}", i++));
				fieldList.Add("AIOServer");
			}
			if (UserDataTools.IsContainField("CrmServer")) {
				bStrFormat.AppendLine(string.Format("CrmServer         string          CrmServer         :{{{0}}}", i++));
				fieldList.Add("CrmServer");
			}
			if (UserDataTools.IsContainField("UTUServer")) {
				bStrFormat.AppendLine(string.Format("UTUServer         string          UTUServer         :{{{0}}}", i++));
				fieldList.Add("UTUServer");
			}
			if (UserDataTools.IsContainField("isRemind")) {
				bStrFormat.AppendLine(string.Format("isRemind          bool            isRemind          :{{{0}}}", i++));
				fieldList.Add("isRemind");
			}
			if (UserDataTools.IsContainField("UserMode")) {
				bStrFormat.AppendLine(string.Format("UserMode          int             UserMode          :{{{0}}}", i++));
				fieldList.Add("UserMode");
			}
			if (UserDataTools.IsContainField("ValidateSPS")) {
				bStrFormat.AppendLine(string.Format("ValidateSPS       bool            ValidateSPS       :{{{0}}}", i++));
				fieldList.Add("ValidateSPS");
			}
			if (UserDataTools.IsContainField("SysUpgradeDate")) {
				bStrFormat.AppendLine(string.Format("SysUpgradeDate    string          SysUpgradeDate    :{{{0}}}", i++));
				fieldList.Add("SysUpgradeDate");
			}

			var objsPara = new String[i];
			for (int j = 0; j < i; j++) {
				objsPara[j] = Convert.ToString(FIELDMAP[fieldList[j]].GetValue(userData));
			}
			
			
			return  string.Format(bStrFormat.ToString(), objsPara);
			
			
		}
	}
}
