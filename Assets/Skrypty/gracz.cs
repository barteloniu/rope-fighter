using UnityEngine;
using System.Collections;

public class gracz : MonoBehaviour
{

    public GameObject sprite;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            sprite.SetActive(true);
            Vector3 myszka = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(myszka.y - transform.position.y, myszka.x - transform.position.x) * (180 / Mathf.PI);

            sprite.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            sprite.SetActive(false);
        }
	}
}
