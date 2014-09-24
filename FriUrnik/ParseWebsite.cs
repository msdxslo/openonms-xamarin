using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FriUrnik.Models;

namespace FriUrnik
{
    public static class ParseWebsite
    {
        public async static Task<IList<string>> GetTableTitles()
        {
            var result = new List<string>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://urnik.fri.uni-lj.si/timetable/2014_2015_zimski/");
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(await response.Content.ReadAsStringAsync());
                var links = doc.DocumentNode.Descendants("h2");
                foreach (var link in links)
                {
                    result.Add(link.InnerText);
                }
            }
            return result;
        }

        public async static Task<IList<Link>> GetLinks(string columnTitle)
        {
            var result = new List<Link>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://urnik.fri.uni-lj.si/timetable/2014_2015_zimski/");
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(await response.Content.ReadAsStringAsync());
                var links = doc.DocumentNode.Descendants("h2").FirstOrDefault((n) => n.InnerText == columnTitle);
                if (links == null)
                {
                    throw new Exception("Failed to find " + columnTitle + " on website.");
                }
                links = links.NextSibling.NextSibling;
                foreach (var link in links.Elements("a"))
                {
                    result.Add(new Link()
                        {
                            Name = link.InnerText,
                            Url = "https://urnik.fri.uni-lj.si" + link.Attributes["href"].Value
                        });
                }
            }
            return result;
        }

        public async static Task<WeekSchedule> GetSchedule(string url)
        {
            var result = new WeekSchedule();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(await response.Content.ReadAsStringAsync());
                var tds = doc.DocumentNode.Descendants("td").Where((t) => t.Attributes["rowspan"] != null);
                var daysFilled = new int[5];
                int currentDay = 0;
                foreach (var td in tds)
                {
                    var rowSpanAttr = td.Attributes["rowspan"];
                    if (rowSpanAttr == null)
                        continue;
                    var rowSpan = int.Parse(rowSpanAttr.Value);
                    currentDay = 0;
                    for (int i = 0; i < daysFilled.Length; i++)
                    {
                        if (daysFilled[currentDay] > daysFilled[i])
                        {
                            currentDay = i;
                        }
                    }

                    var classInfo = new ClassInfo();
                    var div = td.Element("div");
                    if (div == null)
                    {
                        daysFilled[currentDay] += rowSpan;
                        continue;
                    }
                    var c = div.Element("span").ChildNodes;
                    classInfo.Name = c.Skip(4).First().InnerText.Trim('"', '\n', '\r', ' ', '\t');
                    classInfo.ClassRoom = div.Elements("a").First((a) => a.Attributes["href"].Value.StartsWith("?classroom=")).InnerText;
                    classInfo.Lecturers = (div.Elements("a").Where((a) => a.Attributes["href"].Value.StartsWith("?teacher=")).Select((a) => a.InnerText)).ToList();
                    var startTime = daysFilled[currentDay] + 7;
                    classInfo.Time = startTime.ToString().PadLeft(2, '0') + ":00 - " + (startTime + rowSpan).ToString().PadLeft(2, '0') + ":00";
                    result.Days[currentDay].Classes.Add(classInfo);
                    daysFilled[currentDay] += rowSpan;
                }
            }
            return result;
        }
    }
}
