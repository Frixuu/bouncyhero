using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frixu.BouncyHero.Themes;

namespace Frixu.BouncyHero
{
    public static class GameManager
    {
        public static ThemeManager Themes { get; }
        private static bool gamePaused;

        public static bool Paused
        {
            get => gamePaused;
            set
            {
                gamePaused = value;
                if (value) GamePaused?.Invoke(null, EventArgs.Empty);
                else GameResumed?.Invoke(null, EventArgs.Empty);
            }
        }

        public static event EventHandler GamePaused, GameResumed;

        static GameManager()
        {
            Themes = new ThemeManager();
            Paused = true;
        }
    }
}
