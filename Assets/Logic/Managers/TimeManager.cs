using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!World.Active.GetExistingManager<GameManager>().Paused)
            {
                CurrentTime += TimeSpan.FromSeconds(delta);
            }
        }

        public static string TimeFormatter(TimeSpan span)
        {
            var time = (float)span.TotalSeconds;
            var builder = new StringBuilder();
            if (time >= 60f) builder.Append(Mathf.Floor(time / 60f)).Append(":");
            builder.Append((time % 60f).ToString("00.00"));
            return builder.ToString();
        }
    }
}
