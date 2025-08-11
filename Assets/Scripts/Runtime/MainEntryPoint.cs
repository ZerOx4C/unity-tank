using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Runtime.Entity;
using Runtime.Presenter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Runtime
{
    public class MainEntryPoint : IAsyncStartable, IDisposable
    {
        private readonly RuntimeSettings _runtimeSettings;
        private readonly Session _session;
        private readonly TankPresenter _tankPresenter;

        private readonly CompositeDisposable _disposables = new();
        private readonly CancellationDisposable _cancellationDisposable = new();

        [Inject]
        public MainEntryPoint(IObjectResolver objectResolver)
        {
            _runtimeSettings = objectResolver.Resolve<RuntimeSettings>();
            _session = objectResolver.Resolve<Session>();
            _tankPresenter = objectResolver.Resolve<TankPresenter>();
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _cancellationDisposable.Dispose();
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            var taskList = new List<UniTask>();

            foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (go.TryGetComponent(out TankModel tankModel))
                {
                    taskList.Add(SetupTank(tankModel, cancellation));
                }
            }

            await taskList;

            Observable.EveryUpdate(_cancellationDisposable.Token)
                .Subscribe(_ =>
                {
                    var keyboard = Keyboard.current;

                    var advance = 0f;
                    advance += keyboard.wKey.isPressed ? 1 :0;
                    advance += keyboard.sKey.isPressed ? -1 :0;
                    _session.PlayerTank.Advance.Value = advance;

                    var turn = 0f;
                    turn += keyboard.aKey.isPressed ? -1 :0;
                    turn += keyboard.dKey.isPressed ? 1 :0;
                    _session.PlayerTank.Turn.Value = turn;
                })
                .AddTo(_disposables);
        }

        private async UniTask SetupTank(TankModel model, CancellationToken cancellation)
        {
            model.transform.GetPositionAndRotation(out var position, out var rotation);

            var behaviour = (await Object.InstantiateAsync(_runtimeSettings.TankBehaviourPrefab,
                position, rotation, default, cancellation))[0];

            behaviour.SetTankModel(model);

            _tankPresenter.Bind(_session.PlayerTank, behaviour);
        }
    }
}
