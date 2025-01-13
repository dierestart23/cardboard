using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FoodPlace
{
    public string name;
    public string address;
    public Texture pfp;
    public GameObject location;
}

[CreateAssetMenu(fileName = "FoodPlacesData", menuName = "Quest/FoodPlaceData")]
public class FoodPlacesData : ScriptableObject
{
    public FoodPlace[] foodPlaces;
}