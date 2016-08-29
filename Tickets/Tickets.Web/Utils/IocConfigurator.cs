using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;
using Tickets.BLL.Common;
using Tickets.BLL.Core;
using Tickets.DAL.Core;
using Tickets.DAL.Interfaces;
using Tickets.DAL.Models.Entities;
using Tickets.DAL.Repositories;
using Tickets.BLL.Core.Identity;

namespace Tickets.Web.Utils
{
    public class IocConfigurator : IDependencyResolver
    {
        private IKernel kernel;

        public IocConfigurator(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            kernel.Bind<DbContext>()
                .To<TicketsDbContext>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConfigStore.GetConnectionString());
            kernel.Bind<ITicketsUnitOfWork>().To<TicketsUnitOfWork>().InSingletonScope();
            kernel.Bind<IRepository<Ticket>>().To<BaseRepository<Ticket>>().InSingletonScope();
            kernel.Bind<IRepository<User>>().To<BaseRepository<User>>().InSingletonScope();
            kernel.Bind<IRepository<Event>>().To<BaseRepository<Event>>().InSingletonScope();
            kernel.Bind<IRepository<Purchase>>().To<BaseRepository<Purchase>>().InSingletonScope();
            kernel.Bind<UserManager<User, string>>().To<UserManager>().InSingletonScope();
            kernel.Bind<RoleManager<Role>>().To<RoleManager>().InSingletonScope();
            kernel.Bind<IUserStore<User>>().To<UserStore>().InSingletonScope();
            kernel.Bind<IRoleStore<Role>>().To<RoleStore>().InSingletonScope();
            kernel.Bind<IAuthenticationManager>()
                .ToMethod(c => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();
            kernel.Bind<IEventService>().To<EventService>().InSingletonScope();
            kernel.Bind<IPurchaseService>().To<PurchaseService>().InSingletonScope();
            kernel.Bind<ITicketService>().To<TicketService>().InSingletonScope();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}