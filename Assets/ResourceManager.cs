using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.instance.onResourceUpdateArrived += UpdateResources;
    }

    private void UpdateResources(Gardarike.Resources resources)
    {
        Debug.Log("FFLSKDJFLKSJDF:SLKJDF");
        // TODO gold
        //PlayerPrefs.SetInt(GlobalConstants.GOLD, resources.)
        PlayerPrefs.SetInt(GlobalConstants.STONE, (int) resources.Stone);
        PlayerPrefs.SetInt(GlobalConstants.WOOD, (int) resources.Wood);
        PlayerPrefs.SetInt(GlobalConstants.LEATHER, (int) resources.Leather);
        PlayerPrefs.SetInt(GlobalConstants.FOOD, (int) resources.Food);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
