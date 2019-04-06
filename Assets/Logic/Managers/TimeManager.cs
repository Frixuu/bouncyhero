using System;
using System.Text;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Managers
{
    public class TimeManager : ComponentSystem
    {
        public TimeSpan CurrentTime { get; private set; }

        public TimeManager()
        {
            CurrentTime = TimeSpan.Zero;
        }

        protected override void OnUpdate()
        {
            var delta = Time.deltaTime;
            if (World.Active.GetExistingManager<GameManager>().Paused) return;
            if (!World.Active.GetExistingManager<LifeManager>().Alive) return;

            CurrentTime += TimeSpan.FromSeconds(delta);
        }

        public static string TimeFormatter(TimeSpan span)
        {
            var builder = new StringBuilder();
            if (span.Minutes > 0) builder.Append(span.Minutes).Append(":");
            builder.Append((span.TotalSeconds % 60d).ToString("00.00"));
            return builder.ToString();
        }
    }
}
