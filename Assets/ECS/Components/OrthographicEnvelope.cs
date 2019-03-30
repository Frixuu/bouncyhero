using System;
using Unity.Entities;

namespace Frixu.BouncyHero.Components
{
    [Serializable]
    public struct OrthographicEnvelope : IComponentData
    {
        public float Width;
        public float Height;
    }
}
