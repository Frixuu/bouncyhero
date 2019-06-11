using System;
using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Managers
{
    /// <summary> Manages the life of the player. </summary>
    public class LifeManager : ComponentSystem
    {
        private bool isAlive;

        /// <summary> Is the player alive? </summary>
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

        /// <summary> An event raised whenever the player dies. </summary>
        public event EventHandler Killed;
        /// <summary> An event raised whenever the player gets respawned. </summary>
        public event EventHandler Respawned;

        protected override void OnUpdate()
        {
            
        }

        public IEnumerator RespawnAfterTime(TimeSpan time)
        {
            yield return new WaitForSeconds((float)time.TotalSeconds);
            Alive = true;
        }
    }
}
