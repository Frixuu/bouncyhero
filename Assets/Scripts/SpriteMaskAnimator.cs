using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Frixu.BouncyHero.Managers;
using Unity.Entities;
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
            director.stopped += delegate { mask.enabled = false; };
            World.Active.GetExistingSystem<LifeManager>().Killed += delegate
            {
                Play();
            };
        }

        private void Play()
        {
            director.Play();
            mask.enabled = true;
            //World.Active.GetExistingSystem<LifeManager>().Alive = true;
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Play();
                World.Active.GetExistingSystem<LifeManager>().Alive = false;
            }
        }
    }
}