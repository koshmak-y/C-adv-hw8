using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument TelephoneBook = new XDocument();
            XElement MyContacts = new XElement("MyContacts");
            XElement Contact = new XElement("Contact");
            XAttribute TelephoneNumber = new XAttribute("TelephoneNumber", "+380971111111");
            Contact.Add(TelephoneNumber);
            MyContacts.Add(Contact);
            TelephoneBook.Add(MyContacts);
            TelephoneBook.Save("TelephoneBook.xml");
            Console.WriteLine("Выполняеться проверка наличия файла...\n");

            if(File.Exists(String.Format(Environment.CurrentDirectory + @"\TelephoneBook.xml")))
            {
                Console.WriteLine("Файл TelephoneBook.xml успешно создан и сохранен в директории:");
                Console.WriteLine(Environment.CurrentDirectory);
            }
            else
            {
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
