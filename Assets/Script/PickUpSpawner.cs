using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject GoldCoin, Health, Stamina;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 4);

        if (randomNum == 1)
        {
            Instantiate(Health, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(Stamina, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            int randomAmountOfGold = Random.Range(1, 5);

            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(GoldCoin, transform.position, Quaternion.identity);
            }
        }
    }
}
