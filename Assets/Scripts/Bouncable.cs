using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    [Serializable]
    public struct BouncableComponent : IComponentData
    {
        public float Speed;
        public float UpperLimit, LowerLimit;
        [HideInInspector] public float Velocity;
    }

    public class Bouncable : ComponentDataProxy<BouncableComponent>
    {
    }

    public class BounceSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;
            Entities.WithAll<Transform, BouncableComponent>()
            .ForEach((Transform transform, ref BouncableComponent bouncable) =>
            {
                if (bouncable.Velocity == 0f)
                    bouncable.Velocity = bouncable.Speed;

                transform.localPosition += new Vector3(0f, bouncable.Velocity * delta, 0f);

                if (transform.localPosition.y < bouncable.LowerLimit)
                {
                    bouncable.Velocity = bouncable.Speed;
                    transform.localPosition = new Vector3
                    (
                        transform.localPosition.x,
                        bouncable.LowerLimit,
                        transform.localPosition.z
                    );
                }

                if (transform.localPosition.y > bouncable.UpperLimit)
                {
                    bouncable.Velocity = -bouncable.Speed;
                    transform.localPosition = new Vector3
                    (
                        transform.localPosition.x,
                        bouncable.UpperLimit,
                        transform.localPosition.z
                    );
                }
            });
        }
    }
}
