// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BGMMgr : MonoBehaviour
// {
//     private int audio_idx;
//     public AudioClip[] audioClips;
//     // Start is called before the first frame update
//     void Start()
//     {
//         audio_idx = 0;
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void cycle_bgm()
//     {
//         AudioSource audSrc = GetComponent<AudioSource>();
//         audio_idx++;
//         if(audio_idx == 3)
//         {
//             audio_idx = 0;
//         }

//         audSrc.clip = audioClips[audio_idx];
//         audSrc.Play();


//     }
// }
