using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Assets.Bank
{
    public class Bank : MonoBehaviour
    {
        public InGameSettings settings;
        [SerializeField] private TextMeshProUGUI displayBalance;

        private void Awake()
        {
            settings.currentBalance = settings.startBalance;
            UpdateDisplay();
        }

        public void Deposit(int amount)
        {
            settings.currentBalance += Mathf.Abs(amount);
            UpdateDisplay();
        }

        public void Withdraw(int amount)
        {
            settings.currentBalance -= Mathf.Abs(amount);
            UpdateDisplay();

            if (settings.currentBalance < 0)
            {
                ReloadScene();
            }
        }

        private void UpdateDisplay()
        {
            displayBalance.text = "Gold:  " + settings.currentBalance;
        }

        private void ReloadScene()
        {
            Scene currentscene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentscene.buildIndex);
        }
    }
}
