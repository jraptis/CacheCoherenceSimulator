using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class glow : MonoBehaviour {

    float t;
	// Use this for initialization
	void Start () {        
        t = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - t > 0.5f)
        {
            StartCoroutine(glowme());
            t = Time.time;
        }
	}

    IEnumerator glowme()
    {
        GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.25f);
    }
}
