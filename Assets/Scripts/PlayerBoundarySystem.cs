using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    public class PlayerBoundarySystem : ComponentSystem
    {
        private const float HalfWidth = 10f;
        private static readonly Vector3 TeleportDistance = new Vector3(2 * HalfWidth, 0f, 0f);

        protected override void OnUpdate()
        {
            Entities.WithAll<Transform, PlayerControllerComponent>()
            .ForEach((Transform transform) =>
            {
                var x = transform.localPosition.x;
                if (x < -HalfWidth) transform.localPosition += TeleportDistance;
                else if (x > HalfWidth) transform.localPosition -= TeleportDistance;
            });
        }
    }
}
