using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class busrd_m : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (this.name == "dotorange_mf(Clone)") StartCoroutine(runanimf());
        else if (this.name == "dotorange_mf2(Clone)") StartCoroutine(runanimf2());
        else if (this.name == "dotorange_mb(Clone)") StartCoroutine(runanimb());
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

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6 / speed);
        yield return new WaitForSeconds(speed / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(7 / speed,0);
        yield return new WaitForSeconds(speed * 10 / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6 / speed);
        yield return new WaitForSeconds(speed / 12);


        Destroy(this.gameObject, 0f);
    }


    IEnumerator runanimf()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 33f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6 / speed);
        yield return new WaitForSeconds(speed / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(7 / speed, 0);
        yield return new WaitForSeconds(speed * 10 / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6 / speed);
        yield return new WaitForSeconds(speed / 12);


        Destroy(this.gameObject, 0f);
    }


    IEnumerator runanimf2()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 33f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 / speed);
        yield return new WaitForSeconds(speed / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(-7 / speed, 0);
        yield return new WaitForSeconds(speed * 10 / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 / speed);
        yield return new WaitForSeconds(speed / 12);               

        Destroy(this.gameObject, 0f);
    }


    IEnumerator runanimb()
    {
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 100f / speed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 / speed);
        yield return new WaitForSeconds(speed / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(-7 / speed, 0);
        yield return new WaitForSeconds(speed * 10 / 12);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 / speed);
        yield return new WaitForSeconds(speed / 12);

        Destroy(this.gameObject, 0f);
    }
}
