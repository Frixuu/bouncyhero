using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    public static class BouncyHeroBootstrap
    {
        /// <summary> Loads needed scenes and game managers. </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Bootstrap()
        {
            World.Active.GetOrCreateManager<GameManager>();
            World.Active.GetOrCreateManager<TimeManager>();
            World.Active.GetOrCreateManager<ThemeManager>();
            World.Active.GetOrCreateManager<PlayerDataManager>();
            Debug.Log("The game has been successfully bootstrapped.");
        }
    }
}
