using Frixu.BouncyHero.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

namespace Frixu.BouncyHero.Systems
{
    /// <summary>
    /// Automatically moves certain components up and down,
    /// such as the player or collectibles.
    /// </summary>
    public class BounceSystem : JobComponentSystem
    {
        private struct BounceChangeVelocityJob : IJobForEachWithEntity<Bouncable, Movable>
        {
            [DeallocateOnJobCompletion, ReadOnly]
            public NativeArray<Translation> transforms;

            public void Execute(Entity e, int i, ref Bouncable bouncable, ref Movable movable)
            {
                var pos = transforms[i].Value;

                if (pos.y < bouncable.LowerLimit)
                {
                    movable.Velocity.y = bouncable.Speed;
                }
                else if (pos.y > bouncable.UpperLimit)
                {
                    movable.Velocity.y = -bouncable.Speed;
                }
            }
        }

        private struct BounceEnforceBoundsJob : IJobParallelForTransform
        {
            [DeallocateOnJobCompletion, ReadOnly]
            public NativeArray<Bouncable> bouncables;

            public void Execute(int i, TransformAccess transform)
            {
                if (transform.localPosition.y < bouncables[i].LowerLimit)
                {
                    transform.localPosition = new Vector3
                    (
                        transform.localPosition.x,
                        bouncables[i].LowerLimit,
                        transform.localPosition.z
                    );
                }
                else if (transform.localPosition.y > bouncables[i].UpperLimit)
                {
                    transform.localPosition = new Vector3
                    (
                        transform.localPosition.x,
                        bouncables[i].UpperLimit,
                        transform.localPosition.z
                    );
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var query = GetEntityQuery
            (
                ComponentType.ReadWrite<Transform>(),
                ComponentType.ReadWrite<Movable>(),
                ComponentType.ReadOnly<Bouncable>()
            );

            var bounces = query.ToComponentDataArray<Bouncable>(Allocator.TempJob);
            var transforms = query.GetTransformAccessArray();
            var array = new NativeArray<Translation>(transforms.length, Allocator.TempJob);

            for (var i = 0; i < transforms.length; i++)
            {
                var pos = transforms[i].localPosition;
                array[i] = new Translation
                {
                    Value = new float3(pos.x, pos.y, pos.z)
                };
            }

            var job1 = new BounceChangeVelocityJob { transforms = array };
            var job2 = new BounceEnforceBoundsJob { bouncables = bounces };

            var handle1 = job1.Schedule(this, inputDeps);
            return job2.Schedule(query.GetTransformAccessArray(), handle1);
        }
    }
}