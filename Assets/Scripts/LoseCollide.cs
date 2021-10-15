using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Resource.life <= 0)
        {
            SceneManager.LoadScene("Gameover");
            Debug.Log("gameover");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
            Resource.life -= 1;
        }
    }
}
