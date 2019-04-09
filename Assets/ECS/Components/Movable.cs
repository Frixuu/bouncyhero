using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Frixu.BouncyHero.Components
{
    [Serializable]
    public struct Movable : IComponentData
    {
        public float2 Velocity;
    }
}
