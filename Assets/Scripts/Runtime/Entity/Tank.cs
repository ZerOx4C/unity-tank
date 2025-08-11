using System;
using R3;
using UnityEngine;

namespace Runtime.Entity
{
    public class Tank : IDisposable
    {
        public ReactiveProperty<float> Advance { get; } = new();
        public ReactiveProperty<float> Turn { get; } = new();

        public void Dispose()
        {
            Advance.Dispose();
            Turn.Dispose();
        }
    }
}
