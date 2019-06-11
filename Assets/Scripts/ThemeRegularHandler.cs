using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    public class ThemeRegularHandler : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer bg;
        [SerializeField] public SpriteRenderer[] bounds;
        private ThemeManager themeManager;

        public void Start()
        {
            themeManager = World.Active.GetExistingSystem<ThemeManager>();

            World.Active.GetExistingSystem<LifeManager>().Respawned += delegate
            {
                bg.sprite = themeManager.CurrentTheme.Background;
                foreach (var bound in bounds)
                    bound.color = themeManager.CurrentTheme.BoundariesColor;
            };
        }
    }
}
