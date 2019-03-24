using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frixu.BouncyHero.Themes
{
    public class Theme
    {
        /// <summary> Sprite used for the background. </summary>
        public Sprite Background { get; set; }
        /// <summary> Color used for oncoming obstacles and their indicators. </summary>
        public Color ObstacleColor { get; set; }
        /// <summary> Color used for game area boundaries. </summary>
        public Color BoundariesColor { get; set; }
        /// <summary> Determines if the theme is unlocked
        /// (eg. it's a reward or in-game purchase). </summary>
        public bool Unlocked { get; set; } = true;
        /// <summary> Name of the theme. </summary>
        public string Name { get; set; } = "Not named";
        public override string ToString() => $"Theme: {Name}";
    }
}