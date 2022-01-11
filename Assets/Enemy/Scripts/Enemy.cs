using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public InGameSettings settings;

        private Assets.Bank.Bank bank;

        private void Start()
        {
            bank = FindObjectOfType<Assets.Bank.Bank>();
        }

        public void RewardGold()
        {
            if (bank == null) return;
            bank.Deposit(settings.goldReward);
        }

        public void StealGold()
        {
            if (bank == null) return;
            bank.Withdraw(settings.goldPenalty);
        }

    }

}
