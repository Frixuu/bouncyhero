using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Frixu.BouncyHero.Themes
{
    public class ThemeManager
    {
        private List<Theme> AllThemes;
        public Theme CurrentTheme { get; private set; }
        private Random RNG;

        public ThemeManager()
        {
            RNG = new Random();
            AllThemes = new List<Theme>
            {
                new Theme
                {
                    Name = "Calm Lake",
                    Background = Resources.Load<Sprite>("CalmLake"),
                    ObstacleColor = new Color(0.84f, 0.75f, 0.59f, 1f),
                    BoundariesColor = new Color(0.07f, 0.1f, 0.15f, 1f)
                },
                new Theme
                {
                    Name = "Industrial",
                    Background = Resources.Load<Sprite>("Industrial"),
                    ObstacleColor = new Color(0.77f, 0.21f, 0.27f, 1f),
                    BoundariesColor = new Color(0.3f, 0.14f, 0.14f, 1f)
                }
            };
            CurrentTheme = RandomTheme;
        }

        private void ChooseNewTheme()
        {
            Theme newTheme;
            do { newTheme = RandomTheme; } while (newTheme == CurrentTheme);
            CurrentTheme = newTheme;
            ThemeChanged?.Invoke(this, newTheme);
        }

        public event EventHandler<Theme> ThemeChanged; 

        public Theme RandomTheme
        {
            get
            {
                var unlocked = AllThemes.Where(t => t.Unlocked).ToList();
                return unlocked[RNG.Next(unlocked.Count)];
            }
        }
    }
}
