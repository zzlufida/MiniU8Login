using System;
using System.Collections;
using System.IO;
using System.Xml;
using UFSoft.U8.Framework.LoginContext;
namespace MiniU8Login
{
	public static class CustomXmlSerializer
	{
		const string auditorContext = "AuditorContext";

		public static string Serialize(string type, object value)
		{
			string result = string.Empty;
			var stringWriter = new StringWriter();
			var xmlTextWriter = new XmlTextWriter(stringWriter);
			if (type != null && type == "AuditorContext")
			{
				xmlTextWriter.WriteStartElement("auditor");
				xmlTextWriter.WriteAttributeString("id", ((AuditorContext)value).AuditorId);
				xmlTextWriter.WriteAttributeString("name", ((AuditorContext)value).AuditorName);
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.Close();
			result = stringWriter.ToString();
			stringWriter.Close();
			return result;
		}
		public static object Deserialize(string SerializedValue, string type)
		{
			var xmlTextReader = new XmlTextReader(new StringReader(SerializedValue));
			object obj = null;
			while (xmlTextReader.Read())
			{
				if (type != null)
				{
					if (type != auditorContext) {
						if (type != "Hashtable") {
							if (type == "protocolport") {
								obj = new Hashtable();
								if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "protocolport") {
									((Hashtable)obj)["HTTP"] = xmlTextReader.GetAttribute("HTTP");
									((Hashtable)obj)["TCP"] = xmlTextReader.GetAttribute("TCP");
									((Hashtable)obj)["RePr"] = xmlTextReader.GetAttribute("repr");
									((Hashtable)obj)["RePt"] = xmlTextReader.GetAttribute("rept");
								}
							}
						} else {
							obj = new Hashtable();
							if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "secondconnstring") {
								((Hashtable)obj)["DAC"] = xmlTextReader.GetAttribute("DAC");
								((Hashtable)obj)["META"] = xmlTextReader.GetAttribute("META");
								((Hashtable)obj)["WF"] = xmlTextReader.GetAttribute("WF");
							}
						}
					} else {
						obj = new AuditorContext();
						if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.LocalName == "auditor") {
							((AuditorContext)obj).AuditorId = xmlTextReader.GetAttribute("id");
							((AuditorContext)obj).AuditorName = xmlTextReader.GetAttribute("name");
						}
					}
				}
			}
			return obj;
		}
	}
}
