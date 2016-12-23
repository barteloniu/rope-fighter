using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform target;
    float speed = 10;
    Vector3[] path;
    Vector3 lastTargetPos = new Vector3(0, 0, 1);
    int targetIndex;

    void Start()
    {
        StartCoroutine("CheckGraczPos");
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator CheckGraczPos()
    {
        while (true)
        {
            if(target.position != lastTargetPos)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                lastTargetPos = target.position;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if(transform.position == currentWaypoint)
            {
                targetIndex++;
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            if(transform.position != path[path.Length - 1])
            {
                float angle = Mathf.Atan2(currentWaypoint.y - transform.position.y, currentWaypoint.x - transform.position.x) * (180 / Mathf.PI);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            for(int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);
                if(i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
