using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace PresentationLayer
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, EFUnitOfWork>();
            container.RegisterInstance(new TestService(new EFUnitOfWork("DefaultConnection")));
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}