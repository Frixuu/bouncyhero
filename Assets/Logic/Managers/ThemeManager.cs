using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;
using Random = System.Random;

namespace Frixu.BouncyHero.Managers
{
    /// <summary> Manages themes of the game. </summary>
    public class ThemeManager : ComponentSystem
    {
        protected override void OnUpdate()
        {
            
        }

        /// <summary> Collection of all themes in the game. </summary>
        private List<Theme> AllThemes { get; set; }
        /// <summary> A theme that is currently in use. </summary>
        public Theme CurrentTheme { get; private set; }
        /// <summary> Pseudorandom number generator. </summary>
        private Random RNG { get; }

        public ThemeManager()
        {
            RNG = new Random();
            AllThemes = new List<Theme>
            {
                new Theme
                {
                    Name = "Calm Lake",
                    Background = Resources.Load<Sprite>("Themes/CalmLake"),
                    ObstacleColor = new Color(0.84f, 0.75f, 0.59f, 1f),
                    BoundariesColor = new Color(0.07f, 0.1f, 0.15f, 1f)
                },
                new Theme
                {
                    Name = "Industrial",
                    Background = Resources.Load<Sprite>("Themes/Industrial"),
                    ObstacleColor = new Color(0.77f, 0.21f, 0.27f, 1f),
                    BoundariesColor = new Color(0.3f, 0.14f, 0.14f, 1f)
                }
            };
            CurrentTheme = GetRandomTheme();
        }

        /// <summary> Chooses a new theme, different from the current one. </summary>
        public void ChooseNewTheme()
        {
            Theme newTheme;
            do { newTheme = GetRandomTheme(); } while (newTheme == CurrentTheme);
            CurrentTheme = newTheme;
            ThemeChanged?.Invoke(this, newTheme);
        }

        /// <summary> Event raised when the theme gets changed. </summary>
        public event EventHandler<Theme> ThemeChanged; 

        /// <summary> Fetches a random, unlocked theme.
        /// This can be the same as the currently used theme. </summary>
        public Theme GetRandomTheme()
        {
            var unlocked = AllThemes.Where(t => t.Unlocked).ToList();
            return unlocked[RNG.Next(unlocked.Count)];
        }
    }
}
