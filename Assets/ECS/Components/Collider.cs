using System;
using Unity.Entities;

namespace Frixu.BouncyHero.Components
{
    [Serializable]
    public struct Collider : IComponentData
    {
        public bool Active;
    }
}
