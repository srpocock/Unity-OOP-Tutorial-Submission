using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    private int selectedNumber = 0;
    public GameObject[] animalPrefabs;

    private Animal selectedAnimal;

    public GameObject selectionHighlight;
    // Start is called before the first frame update
    void Start()
    {
        selectedAnimal = animalPrefabs[0].GetComponent<Animal>();
        selectedAnimal.isSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
                       
            // Deselect animal prefabs
            for (int i = 0; i < animalPrefabs.Length; i++)
            {
                animalPrefabs[i].GetComponent<Animal>().isSelected = false;
            }

            // Change selection number
            selectedNumber = (selectedNumber + 1) % animalPrefabs.Length;
            selectedAnimal = animalPrefabs[selectedNumber].GetComponent<Animal>();
            selectedAnimal.isSelected = true;

        }

        selectionHighlight.transform.position = new Vector3(selectedAnimal.transform.position.x, 0, selectedAnimal.transform.position.z);

    }
}
