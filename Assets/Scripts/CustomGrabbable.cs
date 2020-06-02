using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrabbable : OVRGrabbable
{
    private Product product;

    override protected void Start()
    {
        base.Start();
        product = GetComponent<Product>();
    }
    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        product.Grab();
    }
    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        product.GrabEnd();
    }
}
