using System.Linq;
using Frixu.BouncyHero.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Frixu.BouncyHero.Systems
{
    /// <summary> A system for moving the player. </summary>
    public class PlayerControllerSystem : JobComponentSystem
    {
        private struct PlayerManageVelocityJob : IJobForEach<PlayerController, Movable>
        {
            public float delta;
            public MovementDirection dir;

            public void Execute(ref PlayerController controller, ref Movable movable)
            {
                float targetVelocity, forceDelta;

                switch (dir)
                {
                    case MovementDirection.Left:
                        targetVelocity = -controller.MaxSpeed;
                        forceDelta = controller.Acceleration;
                        break;
                    case MovementDirection.Right:
                        targetVelocity = controller.MaxSpeed;
                        forceDelta = controller.Acceleration;
                        break;
                    default:
                        targetVelocity = 0f;
                        forceDelta = controller.Decceleration;
                        break;
                }

                movable.Velocity.x = Mathf.MoveTowards
                (
                    movable.Velocity.x,
                    targetVelocity,
                    forceDelta * delta
                );
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            // Get inputs for both the touchscreen and the keyboard
            var dirKey = Input.GetKey(KeyCode.LeftArrow) ? MovementDirection.Left :
                Input.GetKey(KeyCode.RightArrow) ? MovementDirection.Right :
                MovementDirection.None;
            var dirTouch = Input.touches.Length == 0 ? MovementDirection.None :
                Input.touches.First().position.x / Screen.width < 0.5f ? MovementDirection.Left :
                MovementDirection.Right;
            // Prefer the touchscreen, use keyboard if no touch detected
            var dir = dirTouch != MovementDirection.None ? dirTouch : dirKey;

            var job = new PlayerManageVelocityJob
            {
                delta = Time.deltaTime,
                dir = dir
            };
                
            return job.Schedule(this, inputDeps);
        }
    }
}
