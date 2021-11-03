using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsFormsApp4
{
    class DataManager
    {
        public static List<Video> Videos = new List<Video>();
        public static List<User> Users = new List<User>();
        public static List<Sub> Subs = new List<Sub>();

        static DataManager()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                string videosOutput = File.ReadAllText(@"./Videos.xml");
                XElement videosXElement = XElement.Parse(videosOutput);

                Videos = (from item in videosXElement.Descendants("video")
                          select new Video()
                          {
                              Category = item.Element("category").Value,
                              TypeA = item.Element("typeA").Value,
                              TypeB = item.Element("typeB").Value,
                              Title = item.Element("title").Value,
                              Nation = item.Element("nation").Value,
                              Date = item.Element("date").Value,
                              Actor = item.Element("actor").Value,
                              OTT = item.Element("ott").Value,
                              Rating = double.Parse(item.Element("rating").Value),
                              Comment = item.Element("comment").Value,
                              UserId = item.Element("userId").Value,
                              Delete = item.Element("delete").Value != "0" ? true : false,
                          }).ToList<Video>();

                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);

                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = item.Element("id").Value,
                             Passwd = item.Element("passwd").Value,
                             Name = item.Element("name").Value,
                             Email = item.Element("email").Value
                         }).ToList<User>();

                string subsOutput = File.ReadAllText(@"./Subs.xml");
                XElement subsXElement = XElement.Parse(subsOutput);

                Subs = (from item in subsXElement.Descendants("sub")
                        select new Sub()
                        {
                            Platform = item.Element("platform").Value,
                            Sdate = DateTime.Parse(item.Element("sDate").Value),
                            Money = int.Parse(item.Element("money").Value),
                            Memo = item.Element("memo").Value,
                            Link = item.Element("link").Value,
                            UserId = item.Element("userId").Value
                        }).ToList<Sub>();
            }
            catch (FileNotFoundException)
            {
                Save();
            }
        }
        public static void Save()
        {
            string videosOutput = "";
            videosOutput += "<videos>\n";
            foreach (var item in Videos)
            {
                videosOutput += "<video>\n";
                videosOutput += "<category>" + item.Category + "</category>\n";
                videosOutput += "<typeA>" + item.TypeA + "</typeA>\n";
                videosOutput += "<typeB>" + item.TypeB + "</typeB>\n";
                videosOutput += "<title>" + item.Title + "</title>\n";
                videosOutput += "<nation>" + item.Nation + "</nation>\n";
                videosOutput += "<date>" + item.Date + "</date>\n";
                videosOutput += "<actor>" + item.Actor + "</actor>\n";
                videosOutput += "<ott>" + item.OTT + "</ott>\n";
                videosOutput += "<rating>" + item.Rating + "</rating>\n";
                videosOutput += "<comment>" + item.Comment + "</comment>\n";
                videosOutput += "<userId>" + item.UserId + "</userId>\n";
                videosOutput += "<delete>" + (item.Delete ? 1 : 0) + "</delete>\n";
                videosOutput += "</video>\n";
            }
            videosOutput += "</videos>";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "<id>" + item.Id + "</id>\n";
                usersOutput += "<passwd>" + item.Passwd + "</passwd>\n";
                usersOutput += "<name>" + item.Name + "</name>\n";
                usersOutput += "<email>" + item.Email + "</email>\n";
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>";

            string subsOutput = "";
            subsOutput += "<subs>\n";
            foreach (var item in Subs)
            {
                subsOutput += "<sub>\n";
                subsOutput += "<platform>" + item.Platform + "</platform>\n";
                subsOutput += "<sDate>" + item.Sdate + "</sDate>\n";
                subsOutput += "<money>" + item.Money + "</money>\n";
                subsOutput += "<memo>" + item.Memo + "</memo>\n";
                subsOutput += "<link>" + item.Link + "</link>\n";
                subsOutput += "<userId>" + item.UserId + "</userId>\n";
                subsOutput += "</sub>\n";
            }
            subsOutput += "</subs>";

            File.WriteAllText(@"./Videos.xml", videosOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);
            File.WriteAllText(@"./Subs.xml", subsOutput);
        }
    }
}
