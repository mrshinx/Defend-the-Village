using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupContent : MonoBehaviour {

    TextMeshPro textMesh;
    float disappearTimer;
    float disappearTimerMax;
    float disappearSpeed;
    float x;
    float y;
    public bool isCrit = false;
    public bool isBounty = false;
    Color textColor;

    // Use this for initialization

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    void Start () {
        if (isCrit)
        {
            disappearTimer = 0.5f;
            disappearTimerMax = disappearTimer;
            disappearSpeed = 3f;
            x = Random.Range(-3f, 3f);
            y = Random.Range(-5f, 2f);
        }

        if (isBounty)
        {
            disappearTimer = 0.8f;
            disappearTimerMax = disappearTimer;
            disappearSpeed = 3f;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (isBounty)
        {
            transform.position += new Vector3(0, 2) * Time.deltaTime;
            disappearTimer -= Time.deltaTime;
            if (disappearTimer >= disappearTimerMax / 2)
            {
                transform.localScale += Vector3.one * Time.deltaTime;
            }
            if (disappearTimer < disappearTimerMax / 2)
            {
                transform.localScale -= Vector3.one * Time.deltaTime;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a < 0) Destroy(gameObject);
            }
        }
        if (isCrit)
        {
            
            disappearTimer -= Time.deltaTime;
            transform.position += new Vector3(x,y) * Time.deltaTime;
            if (disappearTimer >= disappearTimerMax / 2)
            {
                transform.localScale += Vector3.one * Time.deltaTime;
            }
            if (disappearTimer < disappearTimerMax / 2)
            {
                transform.localScale -= Vector3.one * Time.deltaTime;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a < 0) Destroy(gameObject);
            }
        }

    }

    public void Setup(float value)
    {
        int rounded = (int)System.Math.Round(value, 0);
        textMesh.SetText(rounded.ToString());
        textColor = textMesh.color;
    }
}
