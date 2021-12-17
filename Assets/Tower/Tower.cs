using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;
    int placedTowers = 0;

    void Start() 
    {
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        foreach(Transform child in transform) // Disabling child
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child) // Disabling all grandchilds in child
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }

    }
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null) return false;

        if(bank.CurrentBalance >= cost)
        {
             Instantiate(tower.gameObject, position, Quaternion.identity);
             bank.Withdraw(cost);
             return true;
        }

        return false;
    }
}