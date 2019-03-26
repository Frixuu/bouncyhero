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
            World.Active.GetOrCreateManager<TimeManager>();
            World.Active.GetOrCreateManager<ThemeManager>();
            World.Active.GetOrCreateManager<GameManager>();
            Debug.Log("The game has been successfully bootstrapped.");
        }
    }
}
