using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductDetail
{
    public string Id;
    public string Name;
    public string Description;
    public string Path;
    public float Money; 
}

[System.Serializable]
public class Products
{
    public List<ProductDetail> productList;
}