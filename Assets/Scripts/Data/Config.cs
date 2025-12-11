using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// khu lưu trữ class json
[System.Serializable]
public class location
{
   public string id;
   public string name;
}
[System.Serializable]
public class creen
{
   public string ID;
   public string name;
   public List<location> location;
}
[System.Serializable]
public class screens
{
    public List<creen> playscreens;
}
public class Config : MonoBehaviour
{

}
