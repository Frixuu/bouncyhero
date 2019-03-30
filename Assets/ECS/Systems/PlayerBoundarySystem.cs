using Frixu.BouncyHero.Components;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Frixu.BouncyHero.Systems
{
    /// <summary>
    /// Wraps the screen horizontally,
    /// so the player can warp between left and right side of the game area.
    /// </summary>
    public class PlayerBoundarySystem : JobComponentSystem
    {
        private struct PlayerTeleportJob : IJobParallelForTransform
        {
            private const float HalfWidth = 10f;
            private static readonly Vector3 TeleportDistance = new Vector3(2 * HalfWidth, 0f, 0f);

            public void Execute(int index, TransformAccess transform)
            {
                if (transform.localPosition.x < -HalfWidth)
                    transform.localPosition += TeleportDistance;
                else if (transform.localPosition.x > HalfWidth)
                    transform.localPosition -= TeleportDistance;
            }
        }

        /// <summary> Checks the player's position every frame. </summary>
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var group = GetComponentGroup
            (
                ComponentType.ReadWrite<Transform>(),
                ComponentType.ReadOnly<PlayerController>()
            );
            var transforms = group.GetTransformAccessArray();
            return new PlayerTeleportJob().Schedule(transforms, inputDeps);
        }
    }
}
