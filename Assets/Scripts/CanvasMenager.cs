using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenager : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 offset;

    private List<ProductDetail> basket;
    private Transform descriptionGameObject;
    private Transform basketGameObject;
    private Canvas mainCanvas;
    private Canvas descriptionCanvas;
    private Canvas basketCanvas;
    private Text descriptionText;
    private Text nameText;
    private Text basketText;
    private Product currentProduct;

    private void Start()
    {
        basket = new List<ProductDetail>();
        descriptionGameObject = transform.GetChild(0);
        basketGameObject = transform.GetChild(1);

        mainCanvas = GetComponent<Canvas>();
        descriptionCanvas = descriptionGameObject.GetComponent<Canvas>();
        basketCanvas = basketGameObject.GetComponent<Canvas>();

        descriptionText = descriptionGameObject.transform.GetChild(0).GetComponent<Text>();
        nameText = descriptionGameObject.transform.GetChild(1).GetComponent<Text>();

        basketText = basketGameObject.transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.B))
            ShowBasket();
        if(mainCanvas.enabled)
            transform.position = playerPosition.position + offset;
    }

    public void ShowDescription(ProductDetail productDetail)
    {
        mainCanvas.enabled = true;
        descriptionCanvas.enabled = true;
        basketCanvas.enabled = false;
        nameText.text = productDetail.Name;
        descriptionText.text = productDetail.Description;
    }
    public void ShowBasket(ProductDetail productDetail=null)
    {
        if (productDetail != null)
            basket.Add(productDetail);

        mainCanvas.enabled = true;
        descriptionCanvas.enabled = false;
        basketCanvas.enabled = true;
        var basketList = "Basket: \n";
        foreach(var item in basket)
        {
            basketList += $"Name: {item.Name} Price: {item} $ \n";
        }
        basketList += $"Total: {basket.Select(x => x.Money).Sum().ToString()}";
        basketText.text = basketList;
    }

}
