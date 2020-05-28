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
//登陆成功后,会获取到token信息,该信息可以帮助你构建u8login对象
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
```
