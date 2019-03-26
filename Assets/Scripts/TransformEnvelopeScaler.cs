using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Transforms;

namespace Frixu.BouncyHero.Scripts
{
    [Serializable]
    public struct TransformEnvelopeScaler : IComponentData
    {
        public float Size;
        public float TargetAspectRatio;
    }

    public class TransformEnvelopeScaleSystem : ComponentSystem
    {
        private struct Filter
        {
            public readonly int Length;
            public ComponentDataArray<TransformEnvelopeScaler> scaleData;
            public TransformAccessArray transforms;
        }

        [Inject] private Filter filter;

        protected override void OnUpdate()
        {
            var screenRatio = (float) Screen.width / (float) Screen.height;
            for (var i = 0; i < filter.Length; i++)
            {
                var side = filter.scaleData[i].Size;
                if (screenRatio < filter.scaleData[i].TargetAspectRatio)
                    side *= filter.scaleData[i].TargetAspectRatio / screenRatio;
                filter.transforms[i].localScale = new Vector3(side, side, 0);
            }
        }
    }
}
