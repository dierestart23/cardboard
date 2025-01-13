using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class UserData
{
    public string username;
    public string address;
    public Texture pfp;
    public Transform location;
}


[CreateAssetMenu(fileName = "DiabeteatsUserData", menuName = "Quest/DiabeteatsUserData")]
public class DiabeteatsUserData : ScriptableObject
{
    public UserData[] userData;
}
