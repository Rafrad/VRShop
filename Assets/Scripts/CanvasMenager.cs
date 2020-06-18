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
    private Transform endGameObject;
    private Canvas mainCanvas;
    private Canvas descriptionCanvas;
    private Canvas basketCanvas;
    private Canvas endCanvas;
    private Text descriptionText;
    private Text nameText;
    private Button addToBasketButton;
    private Text basketText;
    private Button buyButton;
    private ProductDetail currentProduct;

    private void Start()
    {
        basket = new List<ProductDetail>();
        descriptionGameObject = transform.GetChild(0);
        basketGameObject = transform.GetChild(1);
        endGameObject = transform.GetChild(2);

        mainCanvas = GetComponent<Canvas>();
        descriptionCanvas = descriptionGameObject.GetComponent<Canvas>();
        basketCanvas = basketGameObject.GetComponent<Canvas>();
        endCanvas = endGameObject.GetComponent<Canvas>();

        descriptionText = descriptionGameObject.transform.GetChild(0).GetComponent<Text>();
        nameText = descriptionGameObject.transform.GetChild(1).GetComponent<Text>();
        addToBasketButton = descriptionGameObject.GetChild(2).GetComponent<Button>();

        basketText = basketGameObject.transform.GetChild(0).GetComponent<Text>();
        buyButton = basketGameObject.GetChild(1).GetComponent<Button>();
    }

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.B))
            ShowBasket();
        if (mainCanvas.enabled)
        {
            transform.position = playerPosition.position + playerPosition.forward * 2;
            transform.rotation = playerPosition.rotation;
        }
    }

    public void ShowDescription(ProductDetail productDetail)
    {
        mainCanvas.enabled = true;
        descriptionCanvas.enabled = true;
        basketCanvas.enabled = false;
        addToBasketButton.enabled = true;
        buyButton.enabled = false;
        nameText.text = productDetail.Name;
        descriptionText.text = productDetail.Description;
        currentProduct = productDetail;
    }
    public void ShowBasket()
    {
        mainCanvas.enabled = true;
        descriptionCanvas.enabled = false;
        basketCanvas.enabled = true;
        addToBasketButton.enabled = false;
        buyButton.enabled = true;
        var basketList = "";
        foreach(var item in basket)
        {
            basketList += $"Name: {item.Name} Price: {item.Money} $ \n";
        }
        basketList += $"Total: {basket.Select(x => x.Money).Sum().ToString()}";
        basketText.text = basketList;
    }
    public void ClickAddToBasketButtonHandler()
    {
        basket.Add(currentProduct);
        ShowBasket();
    }
    public void ClickBuyButtonHandler()
    {
        mainCanvas.enabled = true;
        basketCanvas.enabled = false;
        descriptionCanvas.enabled = false;
        endCanvas.enabled = true;
    }
}
