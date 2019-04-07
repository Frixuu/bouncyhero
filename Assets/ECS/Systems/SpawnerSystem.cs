using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Systems
{
    public class SpawnerSystem : ComponentSystem
    {
        /// <summary> The longest time in seconds between two spawns. (Easiest) </summary>
        private const float TIME_SPAWN_MAX = 1.35f;
        /// <summary> The shortest time in seconds between two spawns. (Hardest) </summary>
        private const float TIME_SPAWN_MIN = 0.3f;
        /// <summary> How fast should the cooldown decrease? </summary>
        private const float TIME_SPAWN_MULTIPLIER = 0.014f;

        private float timeToSpawn = TIME_SPAWN_MAX;

        /// <summary> Calculates period between spawning two objects. </summary>
        /// <param name="currentTime"> Current game time, in seconds. </param>
        private static float TimeToNextSpawn(float currentTime) =>
            Mathf.Lerp(TIME_SPAWN_MAX, TIME_SPAWN_MIN, currentTime * TIME_SPAWN_MULTIPLIER);

        private static void Spawn()
        {
            Debug.Log("Spawned object!");
        }

        protected override void OnUpdate()
        {
            if (World.Active.GetExistingSystem<GameManager>().Paused) return;

            timeToSpawn -= Time.deltaTime;

            if (timeToSpawn < 0f)
            {
                Spawn();
                var now = World.Active.GetExistingSystem<TimeManager>().CurrentTime;
                timeToSpawn += TimeToNextSpawn((float)now.TotalSeconds);
            }
            
        }
    }
}