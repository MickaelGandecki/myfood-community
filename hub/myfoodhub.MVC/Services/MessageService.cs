using myfoodapp.Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services
{
    public class MessageService : IDisposable
    {
        private ApplicationDbContext entities;


        public MessageService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<MessageViewModel> GetAll()
        {
            IList<MessageViewModel> result = new List<MessageViewModel>();

            result = entities.Messages.OrderByDescending(m => m.date).Take(1000).Select(mess => new MessageViewModel
            {
                Id = mess.Id,
                content = mess.content,
                date = mess.date,
                device = mess.device,
                messageTypeId = mess.messageType.Id,
                messageType = new MessageTypeViewModel()
                {
                    Id = mess.messageType.Id,
                    name = mess.messageType.name
                }

            }).ToList();

            return result;
        }

        public IEnumerable<MessageViewModel> Read()
        {
            return GetAll();
        }

        public void Create(MessageViewModel message)
        {
                var entity = new Message();

                entity.Id = message.Id;
                entity.content = message.content;
                entity.date = message.date;
                entity.device = message.device;

            if (entity.messageType == null)
            {
                entity.messageType = new MessageType();
                entity.messageType.Id = message.messageTypeId;
            }

            entities.Messages.Add(entity);
            entities.SaveChanges();

                message.Id = entity.Id;
        }

        public void Update(MessageViewModel message)
        {
            Message target = new Message();
            target = entities.Messages.Where(m => m.Id == message.Id).Include(m => m.messageType).FirstOrDefault();

            if (target != null)
            {
                target.content = message.content;
                target.date = message.date;
                target.device = message.device;

                MessageType currentMessageType = new MessageType();
                currentMessageType = entities.MessageTypes.Where(m => m.Id == message.messageTypeId).FirstOrDefault();

                target.messageType = currentMessageType;
            }      

                entities.SaveChanges();  
        }

        public void Destroy(MessageViewModel message)
        {
                var entity = new Message();

                entity.Id = message.Id;

                entities.Messages.Attach(entity);
                entities.Messages.Remove(entity);

                entities.SaveChanges();

        }

        public MessageViewModel One(Func<MessageViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}