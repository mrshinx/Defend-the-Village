using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chain : MonoBehaviour
{
    GameObject ResearchManagerObj;
    public GameObject[] prerequisiteNodes;
    void Start()
    {

        ResearchManagerObj = GameObject.Find("Research Manager");
        ResearchManagerObj.GetComponent<ResearchManager>().Reset += ResetColor;

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnMouseDown()
    {
        if (TowerSelector.currentTarget != null)
        {
            if (prerequisiteNodes.Length != 0)
            {
                foreach (GameObject prerequisiteNode in prerequisiteNodes)
                {
                    if (prerequisiteNode.GetComponent<Image>().color.a == 1f)
                    {
                        if (gameObject.GetComponent<Image>().color.a == 0.5f)
                        {
                            Activate();
                        }
                        else
                        {
                            Disable();
                        }
                        break;
                    }
                }
            }
            else
            {
                if (gameObject.GetComponent<Image>().color.a == 0.5f)
                {
                    Activate();
                }
                else
                {
                    Disable();
                }
            }
        }
    }

    private void ResetColor()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 0.5f;
        gameObject.GetComponent<Image>().color = tmp;
    }

    public void ReColor()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 1f;
        gameObject.GetComponent<Image>().color = tmp;
    }

    private void Activate()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 1f;
        gameObject.GetComponent<Image>().color = tmp; // brighten color

        ResearchManagerObj.GetComponent<ResearchManager>().ChainEnable(true, gameObject, false);
    }

    private void Disable()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 0.5f;
        gameObject.GetComponent<Image>().color = tmp; // darken color

        ResearchManagerObj.GetComponent<ResearchManager>().ChainEnable(false, gameObject, true);
    }
}
