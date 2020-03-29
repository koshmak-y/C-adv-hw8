using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool access = true;
            string path = String.Format(Environment.CurrentDirectory + @"\TelephoneBook.xml");
            path = path.Replace("hw3", "hw1");
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
                    if (xmlReader.HasAttributes)
                    {
                        while (xmlReader.MoveToNextAttribute())
                        {
                            if (xmlReader.Name == "TelephoneNumber")
                            {
                                Console.WriteLine(xmlReader.Value);
                            }
                        }
                        
                    }
                        
                }
            }
            Console.ReadKey();
        }
    }
}
