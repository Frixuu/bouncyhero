using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Frixu.BouncyHero.Scripts
{
    [RequireComponent(typeof(PlayableDirector))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioPitchAnimator : MonoBehaviour
    {
        private PlayableDirector director;
        private new AudioSource audio;

        private void Start()
        {
            director = GetComponent<PlayableDirector>();
            audio = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.B)) director.Play();
        }
    }
}
