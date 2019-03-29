using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frixu.BouncyHero;
using Unity.Entities;

namespace Frixu.BouncyHero.Managers
{
    public class GameManager : ComponentSystem
    {
        private bool gamePaused;

        public bool Paused
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

        public GameManager()
        {
            //Paused = true;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
