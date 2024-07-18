using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject GoldCoin, Health, Stamina;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 4);
        GameObject itemToSpawn = null;

        switch (randomNum)
        {
            case 1:
                itemToSpawn = Health;
                break;
            case 2:
                itemToSpawn = Stamina;
                break;
            case 3:
                itemToSpawn = GoldCoin;
                int randomAmountOfGold = Random.Range(1, 5);
                for (int i = 0; i < randomAmountOfGold; i++)
                {
                    Instantiate(itemToSpawn, transform.position, Quaternion.identity);
                }
                return; // Exit the method after spawning the gold coins
        }

        if (itemToSpawn != null)
        {
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        }
    }
}
