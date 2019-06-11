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
    public class ThemeNewHandler : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer bg;
        [SerializeField] public SpriteRenderer[] bounds;

        public void Start()
        {
            World.Active.GetExistingSystem<ThemeManager>().ThemeChanged += (_, theme) =>
            {
                bg.sprite = theme.Background;
                foreach (var bound in bounds)
                    bound.color = theme.BoundariesColor;
            };
        }
    }
}
