# MiniU8Login
这是一个绿色U8登录模块,用于快速拿到登录token,绕过login的限制.  

```csharp
var miniLogin=new  	MU8login(string strServer, //服务器地址  
			string strUser, //用户名  
			string strPassword,//密码  
			string strDate, //登录日期  
			string strDataSource)//数据源 XXX@001  
//然后  
miniLogin.Login();  
```
