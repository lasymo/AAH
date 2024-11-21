using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("End is Neigh");
    }    // Update is called once per frame
    void Update()
    {
        
    }
}
