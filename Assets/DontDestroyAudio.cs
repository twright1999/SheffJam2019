using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
public class DontDestroyAudio : MonoBehaviour
{
    void Awake()
	{
		GameObject.DontDestroyOnLoad(gameObject);
	}
}
