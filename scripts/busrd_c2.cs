using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class busrd_c2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (this.name == "dotorange_c2f(Clone)") StartCoroutine(runanimf());
            else if (this.name == "dotorange_c2b(Clone)") StartCoroutine(runanimb());
                 else if (this.name == "dotorange_c2f2(Clone)") StartCoroutine(runanimf2());
                      else StartCoroutine(runanim());
    }
	
	// Update is called once per frame
	void Update () {
	  
	}

    IEnumerator runanim()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 100f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(-4.5f / speed, 0);
        yield return new WaitForSeconds(speed * 2 / 5);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -9.5f / speed);
        yield return new WaitForSeconds(speed * 3 / 5);
        Destroy(this.gameObject, 0f);
    }

    IEnumerator runanimf()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 33f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(-4.5f / speed, 0);
        yield return new WaitForSeconds(speed * 2 / 5);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -9.5f / speed);
        yield return new WaitForSeconds(speed * 3 / 5);
        Destroy(this.gameObject, 0f);
    }

    IEnumerator runanimf2()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 33f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 9.5f / speed);
        yield return new WaitForSeconds(speed * 3 / 5);

        GetComponent<Rigidbody2D>().velocity = new Vector2(4.5f / speed, 0);
        yield return new WaitForSeconds(speed * 2 / 5);
        
        Destroy(this.gameObject, 0f);
    }

    IEnumerator runanimb()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 100f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 9.5f / speed);
        yield return new WaitForSeconds(speed * 3 / 5);

        GetComponent<Rigidbody2D>().velocity = new Vector2(4.5f / speed, 0);
        yield return new WaitForSeconds(speed * 2 / 5);
        
        Destroy(this.gameObject, 0f);
    }
}
