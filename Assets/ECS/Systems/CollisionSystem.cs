using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frixu.BouncyHero.Components;
using Frixu.BouncyHero.ECS;
using Frixu.BouncyHero.Managers;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Collider = UnityEngine.Collider;

namespace Frixu.BouncyHero.Systems
{
    public class CollisionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Debug.Log("elo kuhwy");
            var lifeManager = World.Active.GetExistingSystem<LifeManager>();
            var colliders = GetEntityQuery
            (
                ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<Collider>(),
                ComponentType.Exclude<PlayerController>()
            ).GetTransformAccessArray().ToEnumerable()
                .Select(t => new Rect
                (
                    new Vector2(t.position.x, t.position.y),
                    new Vector2(t.localScale.x, t.localScale.y)
                )
            );

            Debug.Log("Active enemy rects:");
            foreach (var collider in colliders)
            {
                Debug.Log(collider);
            }

            var players = GetEntityQuery
            (
                ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<Collider>(),
                ComponentType.ReadOnly<PlayerController>()
            ).GetTransformAccessArray().ToEnumerable()
                .Select(t => new Rect
                (
                    new Vector2(t.position.x, t.position.y),
                    new Vector2(t.localScale.x, t.localScale.y)
                )
            );

            Debug.Log("Active player rects:");
            foreach(var player in players)
                Debug.Log(player);

            if ((from player in players
                from collider in colliders
                where player.Overlaps(collider)
                select player).Any())
            {
                Debug.Log("Kolizja!");
                lifeManager.Alive = false;
            }
        }
    }
}
