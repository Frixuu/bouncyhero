using System;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Components
{
    /// <summary> Determines the horizontal movement of the player. </summary>
    [Serializable]
    public struct PlayerController : IComponentData
    {
        /// <summary> How fast will the player move at full speed? </summary>
        public float MaxSpeed;
        /// <summary> How quickly will the player gain momentum? </summary>
        public float Acceleration;
        /// <summary> How fast will the player brake? </summary>
        public float Decceleration;
        /// <summary> Current speed of the player. </summary>
        [HideInInspector] public float Velocity;
    }
}
