using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiabeteatsOrders : MonoBehaviour
{
    private static string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public bool isActive;

    public FoodPlacesData foodPlaceData;
    public DiabeteatsUserData AppUserData;

    private AdressesData adressData;

    public TMP_Text foodPlaceName;
    public TMP_Text foodPlaceAdress;
    public RawImage foodPlacePFP;

    public TMP_Text userName;
    public TMP_Text userAdress;
    public RawImage userPFP;

    public Texture userNoPfp;

    public TMP_Text orderTime;
    public TMP_Text orderNumber;

    public TMP_Text cashAmount;
    public float cashFloat;

    private PhoneController phoneController;
    private DiabeteatsApp appController;

    public float timeBeforeDelete;
    private float timeOfOrder;
    public GameObject orderPackage;

    private int foodIndex;
    private int userIndex;
    private string orderCode;


    public static string GenerateCode()
    {
        System.Text.StringBuilder code = new System.Text.StringBuilder(5);

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, characters.Length);
            code.Append(characters[randomIndex]);
        }

        return code.ToString();
    }



    void Start()
    {
        GenerateQuest();
    }

    void Update()
    {
        if(phoneController.dayNightCycle.timeOfDay >= timeOfOrder + timeBeforeDelete && !isActive)
        {
            Destroy(gameObject);
        }
    }

    public void GenerateQuest()
    {

        string randomCode = GenerateCode();
        orderNumber.text = randomCode;
        orderCode = randomCode;
        
        phoneController = FindObjectOfType<PhoneController>();
        appController = FindObjectOfType<DiabeteatsApp>();
        adressData = FindObjectOfType<AdressesData>();

        int randomFoodPlaceIndex = Random.Range(0, foodPlaceData.foodPlaces.Length);
        FoodPlace selectedPlace = foodPlaceData.foodPlaces[randomFoodPlaceIndex];
        foodIndex = randomFoodPlaceIndex;

        foodPlaceName.text = selectedPlace.name;
        foodPlaceAdress.text = selectedPlace.address;
        foodPlacePFP.texture = selectedPlace.pfp;


        int randomUserIndex = Random.Range(0, AppUserData.userData.Length);
        UserData selectedUser = AppUserData.userData[randomUserIndex];
        userIndex = randomUserIndex;

        cashFloat = 10 + Vector3.Distance(adressData.cityData[randomUserIndex].doorPosition.transform.localPosition, adressData.foodPlaceData[randomFoodPlaceIndex].orderPosition.transform.localPosition);
        float cashAmountRound = Mathf.Round(cashFloat * 10) / 10;
        cashAmount.text = cashAmountRound.ToString() + ("$");

        userName.text = selectedUser.username;
        userAdress.text = selectedUser.address;
        if(selectedUser.pfp != null)
        {
            userPFP.texture = selectedUser.pfp;
        } else {
            userPFP.texture = userNoPfp;
        }
        
        orderTime.text = phoneController.timeDisplay.text;
        timeOfOrder = phoneController.timeOfDay;
    }  

    public void StartTask()
    {
        transform.SetParent(appController.taskView);
        isActive = true;
        GameObject newFoodPackage = Instantiate(orderPackage, adressData.foodPlaceData[foodIndex].orderPosition.transform.position, orderPackage.transform.rotation);
        FoodPackage foodPackageScript = newFoodPackage.GetComponent<FoodPackage>();
        foodPackageScript.orderNumber = orderCode;
    }


}
