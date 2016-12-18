using UnityEngine;
using System.Collections;

public class Kamera : MonoBehaviour
{

    public GameObject gracz;

    public bool lerp;
    public Vector3 startPos, target;
    public float lerpTime;

    public bool beforeLerp;

    void Start ()
    {
	
	}
	
	void Update ()
    {

        if (gracz.GetComponent<Gracz>().lerp)
        {
            beforeLerp = true;
        }

        if (!gracz.GetComponent<Gracz>().lerp && beforeLerp)
        {
            beforeLerp = false;
            startPos = transform.position;
            //startPos.z = 0;
            target = gracz.transform.position;
            lerp = true;
            lerpTime = 0f;
        }
        

        if (lerp)
        {
            Debug.Log("a");
            transform.position = Vector2.Lerp(startPos, target, lerpTime / (Vector2.Distance(startPos, target) / 15));
            lerpTime += Time.deltaTime;
            if (lerpTime > (Vector2.Distance(startPos, target) / 15))
            {
                lerp = false;
            }
            transform.Translate(new Vector3(0, 0, -10));
        }
    }
}
