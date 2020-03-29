using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hw2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool access = true;
            string path = String.Format(Environment.CurrentDirectory + @"\TelephoneBook.xml");
            path = path.Replace("hw2", "hw1");
            /* проверка доступен ли файл для открытия */
            try
            {
                FileStream k = new FileStream(path, FileMode.Open);
                k.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                access = false;
            }

            /* работаем с файлом если он доступен */
            if (access)
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                XmlTextReader xmlReader = new XmlTextReader(stream);

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element: 
                            if (xmlReader.HasAttributes)
                            {
                                Console.Write("\t<" + xmlReader.Name + " ");
                                while (xmlReader.MoveToNextAttribute())
                                {
                                    Console.Write("{0}={1}",
                                        xmlReader.Name,
                                        xmlReader.Value); 
                                }
                            }
                            else Console.Write("<" + xmlReader.Name);
                            Console.WriteLine(">");
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("</" + xmlReader.Name);
                            Console.WriteLine(">");
                            break;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
