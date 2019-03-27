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
    public struct PlayerControllerComponent : IComponentData
    {
        public float Speed;
        public float Acceleration;
        public float Decceleration;
        [HideInInspector] public float Velocity;
    }
    public class PlayerController : ComponentDataProxy<PlayerControllerComponent>
    {
    }

    public class PlayerControllerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var dirKey = Input.GetKey(KeyCode.LeftArrow) ? MovementDirection.Left :
                         Input.GetKey(KeyCode.RightArrow) ? MovementDirection.Right :
                         MovementDirection.None;
            var dirTouch = MovementDirection.None;
            var dir = dirTouch != MovementDirection.None ? dirTouch : dirKey;
            var deltaTime = Time.deltaTime;

            Entities.WithAll<Transform, PlayerControllerComponent>()
            .ForEach((Transform transform, ref PlayerControllerComponent player) =>
            {
                float targetVelocity, forceDelta;

                switch (dir)
                {
                    case MovementDirection.Left:
                        targetVelocity = -player.Speed;
                        forceDelta = player.Acceleration;
                        break;
                    case MovementDirection.Right:
                        targetVelocity = player.Speed;
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
