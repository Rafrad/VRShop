using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPointer : MonoBehaviour
{
    public float defaulLenth = 1f;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaulLenth);

        if (hit.collider != null)
            Debug.Log("We hit someting");
        lineRenderer.SetPosition(0, transform.position);

        lineRenderer.SetPosition(1, transform.position + (transform.forward * defaulLenth));
    }

}
