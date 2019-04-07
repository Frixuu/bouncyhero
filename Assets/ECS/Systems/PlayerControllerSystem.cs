using System.Linq;
using Frixu.BouncyHero.Components;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Systems
{
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
            var dirTouch = Input.touches.Length == 0 ? MovementDirection.None :
                Input.touches.First().position.x / Screen.width < 0.5f ? MovementDirection.Left :
                MovementDirection.Right;
            // Prefer the touchscreen, use keyboard if no touch detected
            var dir = dirTouch != MovementDirection.None ? dirTouch : dirKey;

            Entities.ForEach((Transform transform, ref PlayerController player) =>
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
