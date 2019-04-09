using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;
using Random = System.Random;

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

        private readonly Random RandomGenerator = new Random();

        /// <summary> Calculates period between spawning two objects. </summary>
        /// <param name="currentTime"> Current game time, in seconds. </param>
        private static float TimeToNextSpawn(float currentTime) =>
            Mathf.Lerp(TIME_SPAWN_MAX, TIME_SPAWN_MIN, currentTime * TIME_SPAWN_MULTIPLIER);

        private GameObject ObstaclePrefab;

        protected override void OnCreate()
        {
            ObstaclePrefab = Resources.Load<GameObject>("Prefabs/Obstacle");
        }

        /// <summary> Spawns a new obstacle. </summary>
        private void Spawn()
        {
            // Technically, we could be using pure ECS / RenderMesh here,
            // but I really wanted SpriteRenderer to show a *sliced* sprite
            // and the new method doesn't let me do it as easily.
            // So, we're doing it the old-fashioned, more convenient way.
            var position = new Vector3
            (
                (float)RandomGenerator.NextDouble() * 20f - 10f,
                RandomGenerator.Next(0, 5) * 1.4f - 2.8f,
                7f
            );
            var obstacle = Object.Instantiate
            (
                ObstaclePrefab,
                position,
                Quaternion.identity,
                GameObject.Find("Track Elements").GetComponent<Transform>()
            );
            obstacle.GetComponent<SpriteRenderer>().color =
                World.Active.GetExistingSystem<ThemeManager>().CurrentTheme.ObstacleColor;

            Debug.Log("Spawned object!");
        }

        protected override void OnUpdate()
        {
            if (World.Active.GetExistingSystem<GameManager>().Paused) return;

            timeToSpawn -= Time.deltaTime;

            if (timeToSpawn < 0f)
            {
                var now = World.Active.GetExistingSystem<TimeManager>().CurrentTime;
                timeToSpawn += TimeToNextSpawn((float)now.TotalSeconds);
                Spawn();
            }
            
        }
    }
}