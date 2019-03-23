using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Frixu.BouncyHero.Scripts
{
    [RequireComponent(typeof(PlayableDirector))]
    [RequireComponent(typeof(SpriteMask))]
    public class SpriteMaskAnimator : MonoBehaviour
    {
        private PlayableDirector director;
        private SpriteMask mask;

        private void Start()
        {
            mask = GetComponent<SpriteMask>();
            director = GetComponent<PlayableDirector>();
            director.stopped += delegate { mask.enabled = false;  };
        }

        public void Play()
        {
            director.Play();
            mask.enabled = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Play();
            }
        }
    }
}