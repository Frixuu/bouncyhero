using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Frixu.BouncyHero.Scripts
{
    [RequireComponent(typeof(Transform))]
    public class RectScaler : MonoBehaviour
    {
        private new Transform transform;
        public float Size = 10;
        public float AspectRatio = 2f;

        private void Start()
        {
            transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            var screenRatio = (float)Screen.width / (float)Screen.height;
            var side = Size;
            if (screenRatio < AspectRatio) side *= AspectRatio / screenRatio;
            transform.localScale = new Vector3(side, side, 0);
        }
    }
}
