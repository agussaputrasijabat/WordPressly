using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordPressly.Abstractions;
using WordPressly.Models;
using WordPressly.Utility;

namespace WordPressly.Repository
{
    public class Events
    {
        private const string _methodPath = "wp-json/tribe/events/v1/events";
        private readonly static string _cachedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "_events.json");
        internal static List<EventCalendar> LocalEvents = new List<EventCalendar>();

        public Events()
        {
            if (File.Exists(_cachedPath))
                LocalEvents = JsonConvert.DeserializeObject<List<EventCalendar>>(File.ReadAllText(_cachedPath));
        }

        public async Task<IEnumerable<EventCalendar>> ListAsync(int Page, int PerPage, DateTime StartDate, DateTime EndDate, string Filter = "", DataSource DataSource = DataSource.Cloud)
        {
            IEnumerable<EventCalendar> Events = null;
            Filter = Filter.ToLower();

            if (DataSource == DataSource.Cloud)
            {

                var TribeEvents = await HttpHelper.GetRequests<string>(_methodPath + $"?page={Page}&per_page={PerPage}&start_date={StartDate.ToString("dd-MM-yyyy HH:mm:ss")}&end_date={EndDate.ToString("dd-MM-yyyy HH:mm:ss")}" + (!string.IsNullOrEmpty(Filter) ? $"&search={Filter}" : ""));
                Events = JsonConvert.DeserializeObject<IEnumerable<EventCalendar>>(((JObject)JsonConvert.DeserializeObject(TribeEvents))["events"].ToString());

                if (Events != null)
                {
                    foreach (var Event in Events)
                    {
                        var LocalEvent = LocalEvents.FirstOrDefault(p => p.Id == Event.Id);
                        if (LocalEvent is EventCalendar)
                        {
                            LocalEvents.Remove(LocalEvent);
                            LocalEvents.Add(Event);
                        }
                        else
                        {
                            LocalEvents.Add(Event);
                        }
                    }

                    try { File.WriteAllText(_cachedPath, JsonConvert.SerializeObject(LocalEvents)); } catch { }
                }
            }
            else
            {
                Events = LocalEvents.AsEnumerable();
                Events = Events.Where(Event => Event.StartDate < StartDate && Event.EndDate > StartDate);
                if (!string.IsNullOrEmpty(Filter)) Events = Events.Where(Event => Event.StartDate <= StartDate && Event.EndDate >= StartDate);
                Events = Events.Skip((Page - 1) * PerPage).Take(PerPage);
            }

            return Events;
        }

        public async Task<EventCalendar> GetById(int Id, DataSource DataSource = DataSource.Cloud)
        {
            EventCalendar Event = null;
            if (DataSource == DataSource.Cloud)
            {
                Event = await HttpHelper.GetRequests<EventCalendar>(_methodPath + "/" + Id);
                if (Event is EventCalendar)
                {
                    var LocalEvent = LocalEvents.FirstOrDefault(p => p.Id == Event.Id);
                    if (LocalEvent is EventCalendar)
                    {
                        LocalEvents.Remove(LocalEvent);
                        LocalEvents.Add(Event);
                    }
                    else LocalEvents.Add(Event);
                }
            }
            else
            {
                Event = LocalEvents.FirstOrDefault(p => p.Id == Id);
            }

            return Event;
        }
    }
}
