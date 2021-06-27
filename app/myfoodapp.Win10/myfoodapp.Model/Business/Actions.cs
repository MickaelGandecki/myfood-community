using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Business
{
    public class Action
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public string Priority { get; set; }

        public Action(String date, String duration, String description, String actionType, String priority)
        {
            this.Date = date;
            this.Duration = duration;
            this.Description = description;
            this.ActionType = actionType;
            this.Priority = priority;
        }
    }

    public class Actions : ObservableCollection<Action>
    {
        public Actions()
        {
            var currentLang = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
            if(currentLang == "fr-FR")
            {
                Add(new Action("07-07-2016", "10min", "Retirer feuilles dans l'eau", "Aquaponics", "Normal"));
                Add(new Action("07-07-2016", "05min", "Surveiller les escargots", "PermaBeds", "Normal"));
                Add(new Action("02-07-2016", "10min", "Vérifier santé des poissons", "Aquaponics", "High"));
                Add(new Action("02-07-2016", "10min", "Vérifier l'état de la pompe", "Aquaponics", "Normal"));
            }
            else
            { 
                Add(new Action("02-07-2016", "10min", "Remove leaves from water", "Aquaponics", "Normal"));
                Add(new Action("02-07-2016", "05min", "Remove snails from salads", "PermaBeds", "Normal"));
                Add(new Action("02-07-2016", "10min", "Check fish health", "Aquaponics", "High"));
                Add(new Action("02-07-2016", "10min", "Check for solid material in filter", "Aquaponics", "Normal"));
            }
        }
    }
}


