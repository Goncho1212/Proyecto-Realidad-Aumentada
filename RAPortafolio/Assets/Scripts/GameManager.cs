using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemMenu;
    public event Action OnARPosition;

    public static GameManager instance;

    public Action OnItemsMenu { get; internal set; }

    private void Awake(){
        if(instance != null && instance != this )
        {
            Destroy (gameObject);
        }else{
            instance = this;
        }
    }  //hace que solo exista una instnacia de gamemanager

    // Start is called before the first frame update
    void Start()
    {
        MainMenu();
    }

    // Update is called once per frame
    public void MainMenu(){
        OnMainMenu?.Invoke();
        Debug.Log("Main Menu Actived");
    }

    public void ItemsMenu(){
        OnItemMenu?.Invoke();
        Debug.Log("Items Menu Actived");
    }

    public void ARPosition(){
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    public void CloseApp(){
        Application.Quit();
    }
}
