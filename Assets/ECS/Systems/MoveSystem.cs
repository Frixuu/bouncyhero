using Frixu.BouncyHero.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Frixu.BouncyHero.Systems
{
    public class MoveSystem : JobComponentSystem
    {
        private struct MoveJob : IJobParallelForTransform
        {
            public float delta;

            [DeallocateOnJobCompletion, ReadOnly]
            public NativeArray<Movable> movables;

            public void Execute(int index, TransformAccess transform)
            {
                transform.localPosition += new Vector3
                (
                    movables[index].Velocity.x * delta,
                    movables[index].Velocity.y * delta,
                    0f
                );
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var query = GetEntityQuery
            (
                ComponentType.ReadWrite<Transform>(),
                ComponentType.ReadOnly<Movable>()
            );

            var job = new MoveJob
            {
                delta = Time.deltaTime,
                movables = query.ToComponentDataArray<Movable>(Allocator.TempJob)
            };
                
            return job.Schedule(query.GetTransformAccessArray(), inputDeps);
        }
    }
}
