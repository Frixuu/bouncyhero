using System;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    /// <summary> Determines the horizontal movement of the player. </summary>
    [Serializable]
    public struct PlayerControllerComponent : IComponentData
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

    /// <summary>
    /// Proxy object for hybrid ECS.
    /// Determines the horizontal movement of the player.
    /// </summary>
    public class PlayerController : ComponentDataProxy<PlayerControllerComponent> { }

    /// <summary> A system for moving the player. </summary>
    public class PlayerControllerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.deltaTime;
            // Get inputs for both the touchscreen and the keyboard
            var dirKey = Input.GetKey(KeyCode.LeftArrow) ? MovementDirection.Left :
                         Input.GetKey(KeyCode.RightArrow) ? MovementDirection.Right :
                         MovementDirection.None;
            var dirTouch = MovementDirection.None; //TODO Touch input
            // Prefer the touchscreen, use keyboard if no touch detected
            var dir = dirTouch != MovementDirection.None ? dirTouch : dirKey;

            Entities.WithAll<Transform, PlayerControllerComponent>()
            .ForEach((Transform transform, ref PlayerControllerComponent player) =>
            {
                float targetVelocity, forceDelta;

                switch (dir)
                {
                    case MovementDirection.Left:
                        targetVelocity = -player.MaxSpeed;
                        forceDelta = player.Acceleration;
                        break;
                    case MovementDirection.Right:
                        targetVelocity = player.MaxSpeed;
                        forceDelta = player.Acceleration;
                        break;
                    default:
                        targetVelocity = 0f;
                        forceDelta = player.Decceleration;
                        break;
                }

                player.Velocity = Mathf.MoveTowards
                (
                    player.Velocity,
                    targetVelocity,
                    forceDelta * deltaTime
                );

                transform.localPosition += new Vector3(player.Velocity * deltaTime, 0f, 0f);
            });
        }
    }
}
