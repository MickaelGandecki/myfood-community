using myfoodapp.Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Services
{
    public class ProductionUnitOwnerService : IDisposable
    {
        private ApplicationDbContext entities;

        public ProductionUnitOwnerService(ApplicationDbContext entities)
        {
            this.entities = entities;
        }

        public IList<ProductionUnitOwnerViewModel> GetAll()
        {
            IList<ProductionUnitOwnerViewModel> result = new List<ProductionUnitOwnerViewModel>();

            var qry = entities.ProductionUnitOwners.Include(p => p.user)
                                                  .Include(p => p.language)
                                                  .Include(p => p.preferedMoment).ToList();

            qry.ForEach(po =>
            {
                var vm = new ProductionUnitOwnerViewModel()
                {
                    Id = po.Id,
                    pioneerCitizenNumber = po.pioneerCitizenNumber,
                    pioneerCitizenName = po.pioneerCitizenName,
                    contactMail = po.contactMail,
                    office365Mail = po.office365Account,
                    phoneNumber = po.phoneNumber,
                    location = po.location,
                    notificationPushKey = po.notificationPushKey,
                    userId = po.user.Id,
                    user = new ApplicationUserViewModel()
                    {
                        Id = po.user.Id,
                        UserName = po.user.UserName

                    },
                };

                if (po.language != null)
                {
                    vm.languageId = po.language.Id;
                    vm.language = new LanguageViewModel()
                    {
                        Id = po.language.Id,
                        name = po.language.name
                    };
                }

                if (po.preferedMoment != null)
                {
                    vm.preferedMomentId = po.preferedMoment.Id;
                    vm.preferedMoment = new PreferedMomentViewModel()
                    {
                        Id = po.preferedMoment.Id,
                        name = po.preferedMoment.name
                    };
                }

                result.Add(vm);

            });

            return result;
        }

        public void Create(ProductionUnitOwnerViewModel currentProductionUnitOwnerViewModel)
        {

            PreferedMoment currentPreferedMoment = new PreferedMoment();
            currentPreferedMoment = entities.PreferedMoments.Where(p => p.Id == currentProductionUnitOwnerViewModel.preferedMomentId).FirstOrDefault();

            var entity = new ProductionUnitOwner();

            entity.Id = currentProductionUnitOwnerViewModel.Id;
            entity.pioneerCitizenNumber = currentProductionUnitOwnerViewModel.pioneerCitizenNumber;
            entity.pioneerCitizenName = currentProductionUnitOwnerViewModel.pioneerCitizenName;
            entity.contactMail = currentProductionUnitOwnerViewModel.contactMail;
            entity.office365Account = currentProductionUnitOwnerViewModel.office365Mail;
            entity.phoneNumber = currentProductionUnitOwnerViewModel.phoneNumber;
            entity.location = currentProductionUnitOwnerViewModel.location;
            entity.notificationPushKey = currentProductionUnitOwnerViewModel.notificationPushKey;


            if (entity.user == null)
            {
                var user = entities.Users.Where(p => p.Id == currentProductionUnitOwnerViewModel.userId).FirstOrDefault();
                entity.user = user;

                if (user != null)
                    user.Email = currentProductionUnitOwnerViewModel.contactMail;
            }

            if (entity.language == null)
            {
                var language = entities.Languages.Where(p => p.Id == currentProductionUnitOwnerViewModel.languageId).FirstOrDefault();
                entity.language = language;
            }

            if (entity.preferedMoment == null)
            {
                var preferedMoment = entities.PreferedMoments.Where(p => p.Id == currentProductionUnitOwnerViewModel.preferedMomentId).FirstOrDefault();
                entity.preferedMoment = preferedMoment;
            }

            entities.ProductionUnitOwners.Add(entity);

            entities.SaveChanges();

            currentProductionUnitOwnerViewModel.Id = entity.Id;
        }

        public void Update(ProductionUnitOwnerViewModel currentProductionUnitOwnerViewModel)
        {
            ProductionUnitOwner target = new ProductionUnitOwner();
            target = entities.ProductionUnitOwners.Where(p => p.Id == currentProductionUnitOwnerViewModel.Id).FirstOrDefault();

            if (target != null)
            {
                target.pioneerCitizenNumber = currentProductionUnitOwnerViewModel.pioneerCitizenNumber;
                target.pioneerCitizenName = currentProductionUnitOwnerViewModel.pioneerCitizenName;
                target.contactMail = currentProductionUnitOwnerViewModel.contactMail;
                target.office365Account = currentProductionUnitOwnerViewModel.office365Mail;
                target.phoneNumber = currentProductionUnitOwnerViewModel.phoneNumber;
                target.location = currentProductionUnitOwnerViewModel.location;
                target.notificationPushKey = currentProductionUnitOwnerViewModel.notificationPushKey;

                ApplicationUser currentApplicationUser = new ApplicationUser();
                currentApplicationUser = entities.Users.Where(p => p.Id == currentProductionUnitOwnerViewModel.userId).FirstOrDefault();

                if(currentApplicationUser != null)
                currentApplicationUser.Email = currentProductionUnitOwnerViewModel.contactMail;

                Language currentLanguagee = new Language();
                currentLanguagee = entities.Languages.Where(p => p.Id == currentProductionUnitOwnerViewModel.languageId).FirstOrDefault();

                PreferedMoment currentPreferedMoment = new PreferedMoment();
                currentPreferedMoment = entities.PreferedMoments.Where(p => p.Id == currentProductionUnitOwnerViewModel.preferedMomentId).FirstOrDefault();
            }

                entities.SaveChanges();           
        }

        public void Destroy(ProductionUnitOwnerViewModel currentProductionUnitOwnerViewModel)
        {
            ProductionUnitOwner currentProductionUnitOwner = new ProductionUnitOwner();
            currentProductionUnitOwner = entities.ProductionUnitOwners.Where(p => p.Id == currentProductionUnitOwner.Id).FirstOrDefault();

            entities.ProductionUnitOwners.Remove(currentProductionUnitOwner);
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}