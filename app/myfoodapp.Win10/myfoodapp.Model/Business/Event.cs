using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Business
{
    public class Event
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public string Level { get; set; }

        public Event(String date, String time, String description, String eventType, String level)
        {
            this.Date = date;
            this.Time = time;
            this.Description = description;
            this.EventType = eventType;
            this.Level = level;
        }
    }

    public class Events : ObservableCollection<Event>
    {
        public Events()
        {
            var currentLang = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
            if (currentLang == "fr-FR")
            {
                Add(new Event("02-07-2016", "12:31", "Alimentation Poissons auto", "Aquaponics", "Normal"));
                Add(new Event("02-07-2016", "09:29", "Lampe à Plasma allumée", "PermaBeds", "Normal"));
                Add(new Event("02-07-2016", "08:15", "Réseau de Neurones synchronisé", "Aquaponics", "High"));
                Add(new Event("02-07-2016", "07:21", "La température va chuter demain", "Aquaponics", "Normal"));
            }
            else
            {
                Add(new Event("02-07-2016", "12:31", "Fish Auto-Feeder on", "Aquaponics", "Normal"));
                Add(new Event("02-07-2016", "09:29", "Plasma Lamp off", "PermaBeds", "Normal"));
                Add(new Event("02-07-2016", "08:15", "Neural Network synchronized", "Aquaponics", "High"));
                Add(new Event("02-07-2016", "07:21", "Temperature will drop tomorrow", "Aquaponics", "Normal"));
            }
        }
    }
}
