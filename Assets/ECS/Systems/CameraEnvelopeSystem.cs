using Frixu.BouncyHero.Components;
using Unity.Entities;
using UnityEngine;

namespace Frixu.BouncyHero.Systems
{
    /// <summary> Sets orthographic cameras to envelope a certain area. </summary>
    public class CameraEnvelopeSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var screenRatio = (float)Screen.width / (float)Screen.height;

            Entities.WithAll<OrthographicEnvelope, Camera>()
            .ForEach((ref OrthographicEnvelope data, Camera camera) =>
            {
                var envelopeRatio = data.Width / data.Height;

                if (envelopeRatio > screenRatio)
                    camera.orthographicSize = data.Width / screenRatio;
                else
                    camera.orthographicSize = data.Height;
            });
        }
    }
}
