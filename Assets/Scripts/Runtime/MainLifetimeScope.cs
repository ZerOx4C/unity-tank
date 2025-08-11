using Runtime.Entity;
using Runtime.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private RuntimeSettings runtimeSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(runtimeSettings);

            builder.Register<Session>(Lifetime.Singleton);

            builder.Register<TankPresenter>(Lifetime.Singleton);

            builder.RegisterEntryPoint<MainEntryPoint>();
        }
    }
}
