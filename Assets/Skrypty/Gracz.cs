using UnityEngine;
using System.Collections;

public class Gracz : MonoBehaviour
{

    public GameObject pointer, pointerSprite, ropeSprite;

    public bool lerp;
    Vector3 startPos, target;
    float lerpTime;


    void Start()
    {

    }

    void Update()
    {
        //myszka i raycast----------------------------------------------------------------------------------------------------
        if (Input.GetMouseButton(0))
        {
            pointer.SetActive(true);
            Vector3 myszka = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(myszka.y - transform.position.y, myszka.x - transform.position.x) * (180 / Mathf.PI);
            pointer.transform.rotation = Quaternion.Euler(0, 0, angle);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, pointer.transform.right, 5);
            if (hit.collider)
            {
                pointerSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                pointerSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
            }
        }
        else
        {
            pointer.SetActive(false);
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, pointer.transform.right, 5);
                if (hit.collider)
                {
                    startPos = transform.position;
                    target = hit.point;
                    lerp = true;
                    lerpTime = 0f;
                    ropeSprite.SetActive(true);
                    Debug.Log("aaa");
                }
            }
        }

        //lerp-----------------------------------------------------------------------------------------------------------------
        if (lerp)
        {
            transform.position = Vector2.Lerp(startPos, target, lerpTime / (Vector2.Distance(startPos, target) / 3));
            lerpTime += Time.deltaTime;
            ropeSprite.transform.localScale = new Vector3(Vector2.Distance(transform.position, target), 1, 1);
            float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * (180 / Mathf.PI);
            ropeSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (lerpTime > (Vector2.Distance(startPos, target) / 3))
            {
                lerp = false;
                ropeSprite.SetActive(false);
            }
        }

        //escape
        if (Input.GetKeyDown("escape"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
