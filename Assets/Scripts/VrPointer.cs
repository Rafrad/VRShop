using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPointer : MonoBehaviour
{
    public float defaultLength = 1f;
    public GameObject productManagerGameObject;
    public GameObject Player;
    private LineRenderer lineRenderer;
    private ProductManager productManager;
    private bool drawLine = false;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        productManager = productManagerGameObject.GetComponent<ProductManager>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            Debug.Log("Button RIndexTriger");
            drawLine = !drawLine;
            lineRenderer.enabled = !lineRenderer.enabled;
        }
        if (drawLine)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
            if (OVRInput.GetUp(OVRInput.RawButton.A) && drawLine)
            {
                RaycastHit hit;
                var ray = new Ray(transform.position, transform.forward);
                Physics.Raycast(ray, out hit, defaultLength);
                HandleRayCastHit(hit);
            }
        }
    }
    private void HandleRayCastHit(RaycastHit hit)
    {
        if (hit.collider == null)
            return;
        if (hit.transform.gameObject.CompareTag("Product"))
        {
            var product = hit.transform.GetComponent<Product>();
            Debug.Log($"hited product {product.ProductDetail.id}");
            productManager.SelectProduct(product, Player.transform);
        }
    }

}
