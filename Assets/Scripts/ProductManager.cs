using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public CanvasMenager CanvasMenager;
    private List<Product> products;

    void Start()
    {
        products = new List<Product>();
        for(var i =0; i < this.transform.childCount; i++)
            products.Add(this.transform.GetChild(0).GetComponent<Product>());
        MockProductDetail();
    }
    private void MockProductDetail()
    {
        products[0].ProductDetail = new ProductDetail()
        {
            id = "A",
            Name = "Antic Vase",
            Description = "Beatifull vase",
            Money = 12.23M
        };
    }
    public void SelectProduct(Product product, Transform transform)
    {
        product.SelectProduct(transform);
        CanvasMenager.ShowDescription(product.ProductDetail);
    }
    public void ShowProduct(ProductDetail productDetail)
    {
        CanvasMenager.ShowDescription(productDetail);
    }
}
