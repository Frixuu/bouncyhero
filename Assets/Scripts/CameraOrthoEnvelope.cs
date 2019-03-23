using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    /// <summary> Resizes the camera's size to envelope width instead of height. </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraOrthoEnvelope : MonoBehaviour
    {
        [Tooltip("Horizontal Size")] public float Size;
        private new Camera camera;

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            var screenRatio = (float)Screen.width / (float)Screen.height;
            camera.orthographicSize = Size / screenRatio;
        }
    }
}