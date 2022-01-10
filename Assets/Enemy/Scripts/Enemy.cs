using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
  {
    public int GoldReward { set {goldReward = value;} }
    public int GoldPenalty { set {goldPenalty = value;} }
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    private Assets.Bank.Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Assets.Bank.Bank>();
    }

    public void RewardGold()
    {
        if(bank == null)  return;
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if(bank == null) return;
        bank.Withdraw(goldPenalty);
    }
  }
}
