using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseData
{
    public GameObject doorPosition;
    public string address; // Address for the house
}

[System.Serializable]
public class FoodData
{
    public GameObject orderPosition;
    public string address; // Address for the food delivery
}

public class AdressesData : MonoBehaviour
{
    public FoodPlacesData foodPlaceDataObject; // Reference to FoodPlacesData ScriptableObject
    public DiabeteatsUserData diabeteatsUserDataObject; // Reference to DiabeteatsUserData ScriptableObject
    public HouseData[] cityData;
    public FoodData[] foodPlaceData;

    private void OnValidate()
    {
        // Synchronize FoodData with FoodPlacesData
        if (foodPlaceDataObject != null)
        {
            int foodPlacesCount = foodPlaceDataObject.foodPlaces.Length;

            if (foodPlaceData == null || foodPlaceData.Length != foodPlacesCount)
            {
                FoodData[] newFoodPlaceData = new FoodData[foodPlacesCount];
                for (int i = 0; i < foodPlacesCount; i++)
                {
                    if (foodPlaceData != null && i < foodPlaceData.Length && foodPlaceData[i] != null)
                    {
                        newFoodPlaceData[i] = foodPlaceData[i];
                    }
                    else
                    {
                        newFoodPlaceData[i] = new FoodData();
                    }
                    newFoodPlaceData[i].address = foodPlaceDataObject.foodPlaces[i].address;
                }
                foodPlaceData = newFoodPlaceData;
                Debug.Log($"Updated foodPlaceData to match FoodPlacesData count and addresses.");
            }
            else
            {
                for (int i = 0; i < foodPlacesCount; i++)
                {
                    if (foodPlaceData[i] != null)
                    {
                        foodPlaceData[i].address = foodPlaceDataObject.foodPlaces[i].address;
                    }
                }
            }
        }

        // Synchronize HouseData with DiabeteatsUserData
        if (diabeteatsUserDataObject != null)
        {
            int userCount = diabeteatsUserDataObject.userData.Length;

            if (cityData == null || cityData.Length != userCount)
            {
                HouseData[] newCityData = new HouseData[userCount];
                for (int i = 0; i < userCount; i++)
                {
                    if (cityData != null && i < cityData.Length && cityData[i] != null)
                    {
                        newCityData[i] = cityData[i];
                    }
                    else
                    {
                        newCityData[i] = new HouseData();
                    }
                    newCityData[i].address = diabeteatsUserDataObject.userData[i].address;
                }
                cityData = newCityData;
                Debug.Log($"Updated cityData to match DiabeteatsUserData count and addresses.");
            }
            else
            {
                for (int i = 0; i < userCount; i++)
                {
                    if (cityData[i] != null)
                    {
                        cityData[i].address = diabeteatsUserDataObject.userData[i].address;
                    }
                }
            }
        }
    }
}
