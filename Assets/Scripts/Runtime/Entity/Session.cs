using System;
using System.Collections.Generic;
using VContainer;

namespace Runtime.Entity
{
    public class Session : IDisposable
    {
        private readonly List<Tank> _tankList = new();

        [Inject]
        public Session()
        {
            PlayerTank = new Tank();
            _tankList.Add(PlayerTank);
        }

        public Tank PlayerTank { get; }
        public IReadOnlyList<Tank> TankList => _tankList;

        public void Dispose()
        {
            foreach (var tank in _tankList)
            {
                tank.Dispose();
            }

            _tankList.Clear();
        }
    }
}
