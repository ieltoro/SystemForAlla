using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public Firebasemanager FB;
    public GameObject[] canvas;
    public InputField searchInput;
    
    public void ChangeUI(int page)
    {
        foreach(GameObject g in canvas)
        {
            g.SetActive(false);
        }

        canvas[page].SetActive(true);


    }


    public void SearchUser()

    {
        FB.SearchUser(searchInput.text);
        print(searchInput.text + " , searched");
    }

    

}
