using Frixu.BouncyHero.Components;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Systems
{
    /// <summary>
    /// Automatically moves certain components up and down,
    /// such as the player or collectibles.
    /// </summary>
    public class BounceSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;
            Entities.ForEach((Transform transform, ref Bouncable bouncable) =>
            {
                // Initialize the elements, if needed
                if (bouncable.Velocity == 0f)
                    bouncable.Velocity = bouncable.Speed;

                // Change the element's position
                transform.localPosition += new Vector3(0f, bouncable.Velocity * delta, 0f);

                // Check if the element is out of bounds
                var candidate = Mathf.Clamp
                (
                    transform.localPosition.y,
                    bouncable.LowerLimit,
                    bouncable.UpperLimit
                );

                if (transform.localPosition.y != candidate)
                {
                    // Invert the element's velocity
                    bouncable.Velocity *= -1f;
                    // Move the element back into boundaries
                    transform.localPosition = new Vector3
                    (
                        transform.localPosition.x,
                        candidate,
                        transform.localPosition.z
                    );
                }
            });
        }
    }
}