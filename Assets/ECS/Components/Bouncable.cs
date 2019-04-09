using System;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Components
{
    /// <summary> Component describing an object moving constantly up and down. </summary>
    [Serializable]
    public struct Bouncable : IComponentData
    {
        /// <summary> How fast should this object bounce? </summary>
        public float Speed;
        /// <summary> Maximum Y of this object. </summary>
        public float UpperLimit;
        /// <summary> Minimum Y of this object. </summary>
        public float LowerLimit;
    }
}
