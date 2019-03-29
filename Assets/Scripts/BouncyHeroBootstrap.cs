using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    public static class BouncyHeroBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Bootstrap()
        {
            World.Active.GetOrCreateManager<GameManager>();
            World.Active.GetOrCreateManager<TimeManager>();
            World.Active.GetOrCreateManager<ThemeManager>();
            Debug.Log("The game has been successfully bootstrapped.");
        }
    }
}
