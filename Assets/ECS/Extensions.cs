using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Jobs;

namespace Frixu.BouncyHero.ECS
{
    public static class Extensions
    {
        public static IEnumerable<Transform> ToEnumerable(this TransformAccessArray array)
        {
            var transforms = new List<Transform>();
            for (var i = 0; i < array.length; i++)
            {
                transforms.Add(array[i]);
            }

            return transforms;
        }
    }
}
