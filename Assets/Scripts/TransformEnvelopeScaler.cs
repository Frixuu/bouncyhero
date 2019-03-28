using System;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    [Serializable]
    public struct TransformEnvelopeScalerComponent : IComponentData
    {
        public float Size;
        public float TargetAspectRatio;
    }

    public class TransformEnvelopeScaler : ComponentDataProxy<TransformEnvelopeScalerComponent>
    {

    }

    public class TransformEnvelopeScaleSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var screenRatio = (float) Screen.width / (float) Screen.height;
            Entities.WithAll<TransformEnvelopeScalerComponent, Transform>()
            .ForEach((ref TransformEnvelopeScalerComponent data, Transform transform) =>
            {
                var side = data.Size;
                if (screenRatio < data.TargetAspectRatio)
                    side *= data.TargetAspectRatio / screenRatio;
                transform.localScale = new Vector3(side, side, 0);
            });
        }
    }
}
