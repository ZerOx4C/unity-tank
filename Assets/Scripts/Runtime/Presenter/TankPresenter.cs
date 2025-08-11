using System;
using System.Collections.Generic;
using R3;
using Runtime.Entity;
using UnityEngine;
using VContainer;

namespace Runtime.Presenter
{
    public class TankPresenter : IDisposable
    {
        private readonly Dictionary<Tank, IDisposable> _disposableTable = new();

        [Inject]
        public TankPresenter()
        {
        }

        public void Dispose()
        {
            foreach (var disposable in _disposableTable.Values)
            {
                disposable.Dispose();
            }

            _disposableTable.Clear();
        }

        public void Bind(Tank tank, TankBehaviour behaviour)
        {
            var disposables = new CompositeDisposable();

            tank.Advance
                .Where(_ => tank.Turn.Value == 0)
                .Subscribe(v =>
                {
                    behaviour.SetLeftTrackForce(v);
                    behaviour.SetRightTrackForce(v);
                })
                .AddTo(disposables);

            tank.Turn
                .Where(_ => tank.Advance.Value == 0)
                .Subscribe(v =>
                {
                    behaviour.SetLeftTrackForce(v);
                    behaviour.SetRightTrackForce(-v);
                })
                .AddTo(disposables);

            _disposableTable.Add(tank, disposables);
        }
    }
}
