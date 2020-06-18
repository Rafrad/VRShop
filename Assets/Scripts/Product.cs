using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public float yInit = 3f;
    public float rotationSpeed = 1f;
    public float verticalSpeed = 1f;
    public float verticalDifference = 0.2f;
    public float MaxLengthBetweenProductAndPlayer = 100f;
    public float teleportDistanceFromPlayer = 1;
    public float flyingTime = 3;

    private ProductState state;
    private bool upMove = true;
    private Vector3 initPos;
    private Vector3 currentPos;
    private Transform playerTransform;
    private float currentFlyingTime;

    public ProductDetail ProductDetail { get; set; }

    private void Start()
    {
        initPos = transform.position;
        transform.position =  new Vector3(initPos.x, yInit, initPos.z);
        ToShelf();
    }

    private void Update()
    {
        if (state == ProductState.Selected && IsPlayerLostObject())
            ToShelf();
        if (state != ProductState.Grabbed)
            Levitate();
        if (state == ProductState.Flying)
            HandleFlyingState();
    }

    public void SelectProduct(Transform transform)
    {
        if (state == ProductState.Grabbed)
            return;
        state = ProductState.Selected;
        playerTransform = transform;
        currentPos = playerTransform.position + transform.forward * teleportDistanceFromPlayer;
    }
    private void HandleFlyingState()
    {
        currentFlyingTime -= Time.deltaTime;
        if (currentFlyingTime < 0)
            ToShelf();   
    }

    private bool IsPlayerLostObject()
    {
        return (playerTransform.position - currentPos).magnitude > MaxLengthBetweenProductAndPlayer;
    }

    private void ToShelf()
    {
        state = ProductState.OnShelf;
        currentPos = initPos;
        transform.rotation = Quaternion.identity;
    }

    private void Levitate()
    {
        if (upMove && currentPos.y + verticalDifference <= transform.position.y)
            upMove = false;
        if (!upMove && currentPos.y - verticalDifference >= transform.position.y)
            upMove = true;
        var factor = upMove ? 1f : -1f;
        transform.position = new Vector3(currentPos.x, factor * verticalSpeed + transform.position.y, currentPos.z);
        //transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    public void Grab()
    {
        state = ProductState.Grabbed;
    }

    public void GrabEnd()
    {
        state = ProductState.Flying;
        currentFlyingTime = flyingTime;
    }

    private enum ProductState
    {
        OnShelf,
        Selected,
        Grabbed,
        Flying
    }
}
