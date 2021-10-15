using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharDeath : MonoBehaviour {
    int a;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void DeathAnim(GameObject deadNPC, Transform deadPos, float time, AudioClip deathSound, float volume)
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, volume);
        GameObject tempAnim = Instantiate(deadNPC, deadPos.position + new Vector3(0,0,-1), Quaternion.identity );
        Destroy(tempAnim, time);
    }

}
