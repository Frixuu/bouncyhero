using System;
using Unity.Entities;

namespace Frixu.BouncyHero.Components
{
    [Serializable]
    public struct Spawnable : IComponentData
    {
        public float Velocity;
    }
}
