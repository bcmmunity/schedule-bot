using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace TelegrammAppMvcDotNetCore___Buisness_Logic
{
   public class UserOperation
   {
        public void CheckDoc()
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var xDoc = XDocument.Load("users.xml");
            }
            catch(FileNotFoundException)
            {
                XDocument xDoc = new XDocument(new XElement("users", ""));
                xDoc.Save("users.xml");
            }
        }

        public void CreateUser(long userId)
        {
            CheckDoc();
            XDocument xDoc = XDocument.Load("users.xml");
            XElement xRoot = xDoc.Element("users");
            XElement newUser = new XElement("user",
                new XAttribute("id", userId),
                new XElement("university", ""), 
                new XElement("faculty", ""),
                new XElement("course", ""),
                new XElement("group", ""));
            xRoot?.Add(newUser);
            xDoc.Save("users.xml");
        }

        public bool CheckUser(long userId) //Проверка существования пользователя
        {
            //Делаю через System.Xml
            CheckDoc();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode node = xRoot.SelectSingleNode("user[@id='" + userId + "']");
            if (node != null)
            {
                return true;
            }
            return false;
        }

        public void RecreateUser(long userId)
        {

            //Делаю через System.Xml
            CheckDoc();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode node = xRoot.SelectSingleNode("user[@id='" + userId + "']");

            xRoot.RemoveChild(node);
            xDoc.Save("users.xml");

            CreateUser(userId);
        }
		
		public string GetUserInfo(long userId, string type)
		{
			//Делаю через System.Xml
			CheckDoc();
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load("users.xml");
			XmlElement xRoot = xDoc.DocumentElement;
			XmlNode node = xRoot.SelectSingleNode("user[@id='" + userId + "']");

			foreach (XmlNode find in node.ChildNodes)
			{
				if (find.Name == type)
				{
					return find.InnerText;
				}
			}
			return "";
		}

		public void EditUser(long userId, string type, string param)
        {
            //Делаю через System.Xml
            CheckDoc();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode node = xRoot.SelectSingleNode("user[@id='" + userId + "']");

            foreach (XmlNode find in node.ChildNodes)
            {
                if (find.Name == type)
                {
                    node.RemoveChild(find);
                    XmlElement el = xDoc.CreateElement(type);
                    XmlText text = xDoc.CreateTextNode(param);
                    node.AppendChild(el);
                    el.AppendChild(text);
                    xDoc.Save("users.xml");
                    break;
                }
            }
        }
        public string CheckUserElements(long userId, string type) 
        {
            //Делаю через System.Xml
            CheckDoc();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("users.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNode node = xRoot.SelectSingleNode("user[@id='" + userId + "']");

            string ans ="";
            foreach (XmlNode find in node.ChildNodes) //Запутался
            {
                if (find.Name == type)
                {
                        ans = find.InnerText;
                }
            }
            return ans;
        }
    }
}
