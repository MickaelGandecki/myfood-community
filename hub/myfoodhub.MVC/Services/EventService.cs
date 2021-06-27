using myfoodapp.Hub.Models;
using myfoodapp.Hub.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services
{
    public class EventService : IDisposable
    {
        private ApplicationDbContext entities;

        public EventService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<EventViewModel> GetAll()
        {
            IList<EventViewModel> result = new List<EventViewModel>();

            result = entities.Events.Select(ev => new EventViewModel
            {
                Id = ev.Id,
                date = ev.date,
                description = ev.description,
                isOpen = ev.isOpen,
                createdBy = ev.createdBy,
                productionUnitId = ev.productionUnit.Id,
                productionUnit = new ProductionUnitViewModel()
                {
                    Id = ev.productionUnit.Id,
                    info = ev.productionUnit.info,
                    ownerId = ev.productionUnit.owner.Id,
                    owner = new OwnerViewModel()
                    {
                        Id = ev.productionUnit.owner.Id,
                        pioneerCitizenName = ev.productionUnit.owner.pioneerCitizenName,
                        pioneerCitizenNumber = ev.productionUnit.owner.pioneerCitizenNumber
                    }
                },
                eventTypeId = ev.eventType.Id,
                eventType = new EventTypeViewModel()
                {
                    Id = ev.eventType.Id,
                    name = ev.eventType.name
                },
                details = ev.details,
                picture = ev.picture

            }).ToList();

            result.ToList().ForEach(ev => {

                ev.productionUnit.info = String.Format("{0} - {1} #{2}", ev.productionUnit.info, ev.productionUnit.owner.pioneerCitizenName, ev.productionUnit.owner.pioneerCitizenNumber);
            });

            return result;
        }

        public IList<EventViewModel> GetAll(int currentProductionId)
        {
            IList<EventViewModel> result = new List<EventViewModel>();

            result = entities.Events.Include(e => e.productionUnit).Where(ev => ev.productionUnit.Id == currentProductionId)
                                                              .Select(ev => new EventViewModel
            {
                Id = ev.Id,
                date = ev.date,
                description = ev.description,
                isOpen = ev.isOpen,
                createdBy = ev.createdBy,
                productionUnitId = ev.productionUnit.Id,
                productionUnit = new ProductionUnitViewModel()
                {
                    Id = ev.productionUnit.Id,
                    info = ev.productionUnit.info
                },
                eventTypeId = ev.eventType.Id,
                eventType = new EventTypeViewModel()
                {
                    Id = ev.eventType.Id,
                    name = ev.eventType.name
                },
                details = ev.details,
                picture = ev.picture

            }).ToList();

            return result;
        }

        public IEnumerable<EventViewModel> Read()
        {
            return GetAll();
        }

        public EventViewModel One(Func<EventViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Update(UpdateEvent currentEventViewModel)
        {
            Event target = new Event();
            target = entities.Events.Where(p => p.Id == currentEventViewModel.Id).Include(e=>e.eventType).Include(e => e.productionUnit).FirstOrDefault();

            if (target != null)
            {
                target.date = currentEventViewModel.CurrentDate;
                target.description = currentEventViewModel.Note;
				target.details = currentEventViewModel.Details;
                target.isOpen = currentEventViewModel.IsOpen;
            }

                entities.SaveChanges();
           
        }

        public void Destroy(EventViewModel currentEventViewModel)
        {
            Event currentEventType = new Event();
            currentEventType = entities.Events.Where(p => p.Id == currentEventViewModel.Id).FirstOrDefault();

            entities.Events.Remove(currentEventType);
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}