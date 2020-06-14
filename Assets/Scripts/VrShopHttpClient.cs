using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class VrShopHttpClient
{
    //private const string BASE_URL = "localhost:8080/";
    private const string BASE_URL = "C://projects//Unity//repo//dummyServer/";
    private const string RESOURCES_PATH = "resources/";
    private const string PRODUCTS_PATH = "api/company/mycompany/products.json";

    public IEnumerator DownloadGameObject(ProductDetail productDetail, Action<GameObject> callBack)
    {
        var url = BASE_URL + RESOURCES_PATH + productDetail.Path;
        using (var request = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log("Get For remote server failed: "+request.error);
                yield return null;
            }
            else
            {
                var bundle = DownloadHandlerAssetBundle.GetContent(request);
                var productGameObject = bundle.LoadAsset<GameObject>(bundle.AllAssetNames().First());
                callBack(productGameObject);
            }
        }
    }

    public IEnumerator GetProducts(int page, Action<List<ProductDetail>> callBack)
    {
        //var url = $"{BASE_URL}{PRODUCTS_PATH}?page={page.ToString()}";
        var url = $"{BASE_URL}{PRODUCTS_PATH}";
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                yield return null;
            }
            else
            {
                var downloadedProductDetails = JsonUtility.FromJson<Products>(request.downloadHandler.text);
                callBack(downloadedProductDetails.productList);
            }
        }
    }
}
