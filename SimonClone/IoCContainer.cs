using Autofac;
using Autofac.Core;
using SimonClone.Scene;
using SimonClone.ViewModel;

namespace SimonClone
{
    public static class IoCContainer
    {
        #region -- Properties --
        private static IContainer BaseContainer { get; set; }
        #endregion

        #region -- Public Static Methods --
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register SbKGame
            builder.RegisterSbkGame(typeof(StartMenuScene));

            // Register Scenes
            builder.RegisterType<StartMenuScene>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<GameScene>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            // Register ViewModels
            builder.RegisterType<MainViewModel>();

            BaseContainer = builder.Build();
        }

        public static TService Resolve<TService>()
        {
            return BaseContainer.Resolve<TService>();
        }

        public static TService Resolve<TService>(params Parameter[] parameters)
        {
            return BaseContainer.Resolve<TService>(parameters);
        }
        #endregion
    }
}
