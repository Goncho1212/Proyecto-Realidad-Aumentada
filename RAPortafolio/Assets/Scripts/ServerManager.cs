using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{
    [SerializeField] private String jsonURL;
    [SerializeField] private ItemButtonManager itemButtonManager; 
    [SerializeField] private GameObject buttonsContainer;
    [Serializable]

    public struct Items
    {
        [Serializable]
        public struct Item
        {
            public String Name;
            public String Description;
            public String URLBundleModel; //link descarga
            public String URLImageModel; //imagen
        }

         public Item[] items;
    }

    public Items newItemsCollection = new  Items(); 


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetJsonData());
        GameManager.instance.OnItemMenu += CreateButtons; 
    }

    private void CreateButtons (){
        foreach (var item in newItemsCollection.items)
        {
            ItemButtonManager itemButton;

            itemButton = Instantiate(itemButtonManager, buttonsContainer.transform);
            itemButton.name = item.Name;
            itemButton.ItemName = item.Name;
            itemButton.ItemDescription=item.Description;
            itemButton.URLBundleModel = item.URLBundleModel;
            StartCoroutine(GetBundleImage(item.URLImageModel, itemButton));
            
        }
         GameManager.instance.OnItemMenu -= CreateButtons; 
    }

   
    IEnumerator GetJsonData(){
        UnityWebRequest serverRequest = UnityWebRequest.Get(jsonURL);
        yield return serverRequest.SendWebRequest();
        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            newItemsCollection = JsonUtility.FromJson<Items>(serverRequest.downloadHandler.text);

        }else
        {
            Debug.Log("Error");
        }
    }

    IEnumerator GetBundleImage(string urlImage, ItemButtonManager button){
        UnityWebRequest serverRequest = UnityWebRequest.Get(urlImage);
        serverRequest.downloadHandler = new DownloadHandlerTexture();
        yield return serverRequest.SendWebRequest();
        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            button.ImageBundle.texture = ((DownloadHandlerTexture) serverRequest.downloadHandler).texture;

        }else
        {
            Debug.Log("Error");
        }
    }
}
