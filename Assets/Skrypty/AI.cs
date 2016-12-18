using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    float bestDist;
    Vector3 bestPos;

    public GameObject target;
    public float step;

    void Start ()
    {
        StartCoroutine("_ai");
    }

    void checkDistanse (Vector3 v)
    {
        if (Vector2.Distance(transform.position + v, target.transform.position) + Vector2.Distance(transform.position + v, transform.position) < bestDist)
        {
            bestDist = Vector2.Distance(transform.position + v, target.transform.position) + Vector2.Distance(transform.position + v, transform.position);
            bestPos = transform.position + v;
        }
    }
	
    IEnumerator _ai()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);
            if (transform.position == target.transform.position) continue;
            bestDist = Mathf.Infinity;
            checkDistanse(new Vector3(-step, step, 0));
            checkDistanse(new Vector3(0, step, 0));
            checkDistanse(new Vector3(step, step, 0));
            checkDistanse(new Vector3(-step, 0, 0));
            checkDistanse(new Vector3(step, 0, 0));
            checkDistanse(new Vector3(-step, -step, 0));
            checkDistanse(new Vector3(0, -step, 0));
            checkDistanse(new Vector3(step, -step, 0));
            transform.position = bestPos;
        }
        
    }

	void Update ()
    {
        
    }
}
