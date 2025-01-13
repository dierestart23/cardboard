using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabeteatsApp : MonoBehaviour
{
    public GameObject orderPrefab;
    public Transform orderView;
    public Transform taskView;
    public DayNightCycle timeController;
    public PhoneController phoneController;
    public AnimationCurve orderCurve;

    
    public bool signedIn;
    private bool hasSignedOut;
    private bool night, normal, fast;
    
    [System.Serializable]
    public class SpawnRates
    {
        public float nightSpawnRate1;
        public float normalSpawnRate1;
        public float fastSpawnRate1;
        public float nightSpawnRate2;
        public float normalSpawnRate2;
        public float fastSpawnRate2;
    }

    public int reputationLevel;
    public float spawnTime;
    

    public SpawnRates[] spawnRates;
    public AudioSource source;
    public AudioClip notificationClip;

    public GameObject orders;
    public GameObject tasks;
    public GameObject account;

    public GameObject login;
    public GameObject signOut;

    public GameObject youMustLogin1;
    public GameObject youMustLogin2;

    

    

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(spawnRates[reputationLevel].normalSpawnRate1, spawnRates[reputationLevel].normalSpawnRate2);
    }

    // Update is called once per frame
    void Update()
    {
        if(orderCurve.Evaluate(timeController.timeOfDay) == 0)
        {
            night = true;
            normal = false;
            fast = false;
        }
        if(orderCurve.Evaluate(timeController.timeOfDay) == 1)
        {
            night = false;
            normal = true;
            fast = false;   
        }
        if(orderCurve.Evaluate(timeController.timeOfDay) == 2)
        {
            night = false;
            normal = false;
            fast = true;
        }
        if(signedIn && hasSignedOut)
        {
            hasSignedOut = false;
        }
        if(signedIn)
        {
            
            spawnTime -= Time.deltaTime;
            if(spawnTime <= 0)
            {
                if(normal)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].normalSpawnRate1, spawnRates[reputationLevel].normalSpawnRate2);
                    Notification();
                    SpawnOrder();
                }

                if(fast)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].fastSpawnRate1, spawnRates[reputationLevel].fastSpawnRate2);
                    Notification();
                    SpawnOrder();
                }
                if(night)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].nightSpawnRate1, spawnRates[reputationLevel].nightSpawnRate2);
                    Notification();
                    SpawnOrder();
                }
            }
        } else if(!signedIn && !hasSignedOut)
        {
                if(normal)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].normalSpawnRate1, spawnRates[reputationLevel].normalSpawnRate2);
                }

                if(fast)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].fastSpawnRate1, spawnRates[reputationLevel].fastSpawnRate2);
                }
                if(night)
                {
                    spawnTime = Random.Range(spawnRates[reputationLevel].nightSpawnRate1, spawnRates[reputationLevel].nightSpawnRate2);
                }
                hasSignedOut = true;
        }

        if(!signedIn)
        {
            ClearOrders();
        }
    }

    public void Notification()
    {
        if(phoneController.soundOn)
        {
            source.clip = notificationClip;
            source.Play();
        }
    }

    public void SpawnOrder()
    {
        Instantiate(orderPrefab, orderView);
    }

    public void OrdersPage()
    {
        orders.SetActive(true);
        tasks.SetActive(false);
        account.SetActive(false);

    }
    public void TasksPage()
    {
        orders.SetActive(false);
        tasks.SetActive(true);
        account.SetActive(false);

    }
    public void AccountPage()
    {
        orders.SetActive(false);
        tasks.SetActive(false);
        account.SetActive(true);

    }

    public void Login()
    {
        signedIn = true;
        youMustLogin1.SetActive(false);
        youMustLogin2.SetActive(false);
        login.SetActive(false);
        signOut.SetActive(true);
    }
    public void SignOut()
    {
        signedIn = false;
        youMustLogin1.SetActive(true);
        youMustLogin2.SetActive(true);
        login.SetActive(true);
        signOut.SetActive(false);
        ClearOrders();
    }

    public void ClearOrders()
    {
        for (int i = 0; i < orderView.childCount; i++)
        {
            Destroy(orderView.GetChild(0).gameObject);
        }
        for (int i = 0; i < taskView.childCount; i++)
        {
            Destroy(taskView.GetChild(0).gameObject);
        }
    }
}
