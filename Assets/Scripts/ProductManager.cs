using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public CanvasMenager CanvasMenager;
    private List<Product> hotProducts;
    private VrShopHttpClient vrShopHttpClient;
    private static List<Vector3> locationHotProduct;

    private void Awake()
    {
        vrShopHttpClient = new VrShopHttpClient();
        locationHotProduct = new List<Vector3>()
        {
            new Vector3(8.75f, 3, 3.5f),
            new Vector3(8.75f, 3,-2.5f),
        };
    }

    void Start()
    {
        hotProducts = new List<Product>();
        StartCoroutine(GetHotProducts());
    }

    private IEnumerator GetHotProducts()
    {
        yield return vrShopHttpClient.GetProducts(0, SpawnHotObject);
    }

    private void SpawnHotObject(List<ProductDetail> productsDetails)
    {
        var i = 0;
        foreach (var productDetail in productsDetails)
        {
            var pos = locationHotProduct[i];
            StartCoroutine(vrShopHttpClient.DownloadGameObject(productDetail, productGameObject =>
            {
                var newCreatedGameObject = Instantiate(productGameObject, pos, Quaternion.identity);
                var product = newCreatedGameObject.GetComponent<Product>();
                product.ProductDetail = productDetail;
                hotProducts.Add(product);
            }));
            i++;
        }
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
