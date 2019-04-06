using System;
using Unity.Entities;

namespace Frixu.BouncyHero.Managers
{
    public class LifeManager : ComponentSystem
    {
        private bool isAlive;

        public bool Alive
        {
            get => isAlive;
            set
            {
                if (isAlive != value)
                {
                    isAlive = value;
                    if (value) Respawned?.Invoke(this, EventArgs.Empty);
                    else Killed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public LifeManager()
        {
            Alive = true;
        }

        public event EventHandler Killed, Respawned;

        protected override void OnUpdate()
        {
            
        }
    }
}
