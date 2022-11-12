using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;

    public GameObject target;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            startPos = gameObject.transform.position;
            endPos = target.transform.position;

            startPos.z = 0;
            lineRenderer.SetPosition(0, startPos);

            endPos.z = 0;
            lineRenderer.SetPosition(1, endPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }
}
