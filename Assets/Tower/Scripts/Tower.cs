using System.Collections;
using UnityEngine;

namespace Assets.Tower
{
    public class Tower : MonoBehaviour
{
    public int Cost { set {cost = value;} }
    public float BuildDelay { set{buildDelay = value;} }
    [SerializeField] private int cost = 75;
    [SerializeField] private float buildDelay = 1f;

    private void Start() 
    {
        StartCoroutine(Build());
    }

    private IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
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
        Assets.Bank.Bank bank = FindObjectOfType<Assets.Bank.Bank>();

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
}
