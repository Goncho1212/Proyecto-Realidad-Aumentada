using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItenName ;  //aca se puede definir otros datos para colocar en el item
    public Sprite ItemImage;
    public string ItemDescription;
    public GameObject Item3DModel;

    
}
