using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Assets.Bank
{
    public class Bank : MonoBehaviour
    {
        public int CurrentBalance { get { return currentBalance; } }
        public int StartBalance { set { startBalance = value; } }
        [SerializeField] private int startBalance = 150;
        [SerializeField] private int currentBalance;
        [SerializeField] private TextMeshProUGUI displayBalance;

        private void Awake()
        {
            currentBalance = startBalance;
            UpdateDisplay();
        }

        public void Deposit(int amount)
        {
            currentBalance += Mathf.Abs(amount);
            UpdateDisplay();
        }

        public void Withdraw(int amount)
        {
            currentBalance -= Mathf.Abs(amount);
            UpdateDisplay();

            if (currentBalance < 0)
            {
                ReloadScene();
            }
        }

        private void UpdateDisplay()
        {
            displayBalance.text = "Gold:  " + currentBalance;
        }
        
        private void ReloadScene()
        {
            Scene currentscene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentscene.buildIndex);
        }
    }
}
