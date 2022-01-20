using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    private int selectedNumber = 0;
    public GameObject[] animalPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
                       
            // Deselect animal prefabs
            for (int i = 0; i < animalPrefabs.Length; i++)
            {
                animalPrefabs[i].isSelected = false;
            }

            // Change selection number
            selectedNumber = (selectedNumber + 1) % animalPrefabs.Length;
            animalPrefabs[selectedNumber].isSelected = true;


        }
    }
}
