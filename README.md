# MiniU8Login
这是一个绿色U8登录模块,用于快速拿到登录token,绕过login的限制.  

```csharp
var miniLogin=new  	MU8login("192.168.1.2", 	//服务器地址  
				 "demo", 		//用户名  
				 "DEMO",		//密码  
				 "2020-01-01", 		//登录日期  
			         "(default)@999")	//数据源 XXX@001  
//如果 数据源字段不知道怎么写,可以先调用如下函数获取所有数据源
var ret = MU8Login.GetAllDataSource("192.168.1.2");
foreach (var e in ret)
	{
	    Console.WriteLine($"   {e}");
	}
//然后  
miniLogin.Login();
//登陆成功后,会获取到token信息,该信息可以帮助你构建u8login对象,当然要初始化U8login就需要环境里面安装用友客户端了
//我们的本意是绿色环境,所以初始化U8login代码,仅供参考 
var token = miniLogin.TOKEN;
if (token == null)
	{
	    Console.WriteLine(@"登录失败:{0}", mlogin.LastLoginErr);
	    return;
	}
//构建U8login对象		
var u8Login = new clsLogin();
u8Login.ConstructLogin(token);
var subid = "AS"; //U8模块简码,具体号到系统启用里面可以找到
u8Login.Login(ref subid);	

//获取登陆后的明细信息
var connectString=mlogin.CUserData.ConnString;
//其他信息如下
/*

登陆后的详细信息在mlogin.CUserData中，可以使用mlogin.UserDataToString()快速打印出来  
UserId            string          登录名            :demo  
Password          string          密码              :  
AccID             string          帐套号            :999  
iYear             string          年度              :2015  
cSubID            string          模块              :AS  
AppServer         string          应用服务器        :127.0.0.1  
uuid              string          加密UUID          :48141a19-0894-46e3-8146-9c6d137bf62e  
DataSource        string          数据源            :(default)@999  
ConnString        string          数据库连接字符串  :PROVIDER=SQLOLEDB;data source=127.0.0.1;user id=sa;password="Yv2012  
";initial catalog=UFDATA_999_2014;Connect Timeout=30;Persist Security Info=True ;Current Language=Simplified Chinese;  
operDate          string          登录日期          :2015-01-22  
WorkStationSerial string          WorkStationSerial :{9E6C0F0A-5CD6-46E3-95AB-7E343749CFFC}WIN-V9SVNNTKVVP@1  
RightServer       string          权限服务器        :127.0.0.1  
LanguageID        string          语言              :zh-CN  
IsCompanyVer      bool            IsCompanyVer      :False  
SecondConnString  Hashtable       SecondConnString  :System.Collections.Hashtable  
EmployeeId        string          职员              :00001  
EntTypeID         string          EntTypeID         :Industry  
IsAdmin           bool            是否帐套管理员    :True  
UserName          string          登录用户名        :demo  
AccName           string          帐套名称          :星空演示  
iMonth            int             登录月份          :1  
AppServerSerial   string          AppServerSerial   :{9E6C0F0A-5CD6-46E3-95AB-7E343749CFFC}  
Roles             string          Roles             :  
Auditor           _AuditorContext Auditor           :UFSoft.U8.Framework.LoginContext.AuditorContext  
ProtocolPort      Hashtable       ProtocolPort      :System.Collections.Hashtable  
BarCode           string          加密狗号          :  
Customer          string          Customer          :;;0;;0  
AuthenMode        int             AuthenMode        :0  
AuthenExtraInfo   string          AuthenExtraInfo   :  
IndustryType      string          IndustryType      :  
isSendIM          bool            isSendIM          :False  
iBeginYear        string          iBeginYear        :0  
AIOServer         string          AIOServer         :  
CrmServer         string          CrmServer         :WIN-V9SVNNTKVVP:8072  
UTUServer         string          UTUServer         :WIN-V9SVNNTKVVP:11521  
isRemind          bool            isRemind          :False  
UserMode          int             UserMode          :1  
ValidateSPS       bool            ValidateSPS       :True  
SysUpgradeDate    string          SysUpgradeDate    :  
Version           string          Version           :{13.000}  
========================================================================================================================  
*/
```
