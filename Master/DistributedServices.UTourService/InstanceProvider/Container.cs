using ITI.Common.Utilities.Data.Core;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using ITI.Nlayerd.Infrastructure.Data.UTour.UnitOfWork;
using Microsoft.Practices.Unity;
using ITI.Common.Utilities.Diagnostics.Trace;
using Domain.Contracts;
using Application.Contracts;
using Application.Impl;
//using ITI.Nlayerd.Infrastructure.Data.ATM.Context;
//using ITI.Nlayerd.Infrastructure.Data.ATM.UnitOfWork;
//using ITI.Nlayerd.Infrastructure.Data.ATM.Repositories;

namespace DistributedServices.UTourService.InstanceProvider
{
    public static class Container
    {
        #region Properties

        static IUnityContainer m_currentContainer;

        /// <summary>
        /// Get the current configured container
        /// </summary>
        /// <returns>Configured container</returns>
        public static IUnityContainer Current
        {
            get
            {
                return m_currentContainer;
            }
        }

        #endregion

        #region -- Constructor --
        static Container()
        {
            m_currentContainer = new UnityContainer();

            //-> Utilities
            m_currentContainer.RegisterType<ITraceManager, TraceManager>(new ContainerControlledLifetimeManager());

            //-> Unit of Work and Context
            m_currentContainer.RegisterType<UTOURDBEntities>(new InjectionConstructor());
            m_currentContainer.RegisterType<IUTourUnitOfWork, UTOURDBEntities>(new PerResolveLifetimeManager());
            m_currentContainer.RegisterType<IQueryableUnitOfWork, UTOURDBEntities>(new PerResolveLifetimeManager());

            //-> Repositories
            m_currentContainer.RegisterType<IAdminUsersRepository, AdminUsersRepository>();
            m_currentContainer.RegisterType<ILayerHotSpotsRepository, CashedLayerHotSpotsRepository>();
            m_currentContainer.RegisterType<IMonumentPhotosRepository, MonumentPhotosRepository>();
            m_currentContainer.RegisterType<IMonumentRatingRepository, MonumentRatingRepository>();
            m_currentContainer.RegisterType<IMonumentReviewsRepository, MonumentReviewsRepository>();
            m_currentContainer.RegisterType<IMonumentVideosRepository, MonumentVideosRepository>();
            m_currentContainer.RegisterType<ITouristRepository, TouristRepository>();
            m_currentContainer.RegisterType<IUploadedPhotoRepository, UploadedPhotoRepository>();
           
           

            //-> Adapters
            //m_currentContainer.RegisterType<EFG.Common.Utilities.TypeResolution.ITypeAdapter, EFG.Common.Utilities.TypeResolution.TypeAdapter>();
            //m_currentContainer.RegisterType<RegisterTypesMap, EFG.OPS.ATM.Application.ATMModule.Impl.ATMCardManagementModule.ATMCardManagementRegisterTypesMap>("ATMCardManagement");
            //m_currentContainer.RegisterType<RegisterTypesMap, EFG.OPS.ATM.Application.ATMModule.Impl.ATMReconciliationModule.ATMReconciliationRegisterTypesMap>("ATMReconciliation");

            //-> Application services
            m_currentContainer.RegisterType<Application.Contracts.IHotSpotsManagementService, Application.Impl.HotSpotsManagementService>();
            m_currentContainer.RegisterType<ILoginManagementService, LoginManagementService>();
            m_currentContainer.RegisterType<IRatingReviewsManagementService, RatingReviewsManagementService>();
            m_currentContainer.RegisterType<IRegistrationManagementService, RegistrationManagementService>();
            m_currentContainer.RegisterType<IUploadPhotosManagementService, UploadPhotosManagementService>();
           

           

            //-> Distributed Services
            m_currentContainer.RegisterType<DistributedServices.Contracts.IHotSpotsManagmentContract, HotSpotsMgmtService>();
            m_currentContainer.RegisterType<DistributedServices.Contracts.ILoginManagmentContract, LoginManagmentService>();
            m_currentContainer.RegisterType<DistributedServices.Contracts.ILoginRegRevRateMgmtService, LoginRegRevRateMgmtService>();


            
            
        }
        #endregion
    }
}