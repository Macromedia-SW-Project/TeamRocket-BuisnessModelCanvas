using System;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class Inventory : AbstractSingleton<Inventory>
    {
        [SerializeField] GenericGameEventListener m_CoinEventListener;
        [SerializeField] GenericGameEventListener m_KeyEventListener;
        [SerializeField] GenericGameEventListener m_WinEventListener;
        [SerializeField] GenericGameEventListener m_LoseEventListener;

        int m_TempCoins;
        int m_TotalGold;
        float m_TempXp;
        float m_TotalXp;
        int m_TempKeys;

        const float k_MilestoneFactor = 1.2f;

        Hud m_Hud;
        LevelCompleteScreen m_LevelCompleteScreen;

        [SerializeField] TextMeshProUGUI goldValueText;

        void Start()
        {
            m_CoinEventListener.EventHandler = OnCoinPicked;
            m_KeyEventListener.EventHandler = OnKeyPicked;
            m_WinEventListener.EventHandler = OnWin;
            m_LoseEventListener.EventHandler = OnLose;

            m_TempCoins = 0;
            m_TotalGold = SaveManager.Instance.Currency;
            m_TempXp = 0;
            m_TotalXp = SaveManager.Instance.XP;
            m_TempKeys = 0;

            m_LevelCompleteScreen = UIManager.Instance.GetView<LevelCompleteScreen>();
            m_Hud = UIManager.Instance.GetView<Hud>();

            Debug.Log("HUD and LevelCompleteScreen references set.");
            Debug.Log($"Initial Total Gold: {m_TotalGold}");
            UpdateGoldUI();
        }

        void OnEnable()
        {
            m_CoinEventListener.Subscribe();
            m_KeyEventListener.Subscribe();
            m_WinEventListener.Subscribe();
            m_LoseEventListener.Subscribe();
            Debug.Log("Subscribed to events.");
        }

        void OnDisable()
        {
            m_CoinEventListener.Unsubscribe();
            m_KeyEventListener.Unsubscribe();
            m_WinEventListener.Unsubscribe();
            m_LoseEventListener.Unsubscribe();
            Debug.Log("Unsubscribed from events.");
        }

        void OnCoinPicked()
        {
            if (m_CoinEventListener.m_Event is ItemPickedEvent coinPickedEvent)
            {
                m_TempCoins += coinPickedEvent.Count;
                m_Hud.GoldValue = m_TempCoins;
                UpdateGoldUI();
                Debug.Log($"Coins picked: {coinPickedEvent.Count}, Total Temp Coins: {m_TempCoins}");
            }
            else
            {
                throw new Exception($"Invalid event type!");
            }
        }

        void OnKeyPicked()
        {
            if (m_KeyEventListener.m_Event is ItemPickedEvent keyPickedEvent)
            {
                m_TempKeys += keyPickedEvent.Count;
                Debug.Log($"Key picked: {keyPickedEvent.Count}, Total Temp Keys: {m_TempKeys}");
            }
            else
            {
                throw new Exception($"Invalid event type!");
            }
        }

        void OnWin()
        {
            m_TotalGold += m_TempCoins;
            m_TempCoins = 0;
            SaveManager.Instance.Currency = m_TotalGold;

            m_LevelCompleteScreen.GoldValue = m_TotalGold;
            m_LevelCompleteScreen.XpSlider.minValue = m_TotalXp;
            m_LevelCompleteScreen.XpSlider.maxValue = k_MilestoneFactor * (m_TotalXp + m_TempXp);
            m_LevelCompleteScreen.XpValue = m_TotalXp + m_TempXp;

            m_LevelCompleteScreen.StarCount = m_TempKeys;

            Debug.Log($"Level Complete: Total Gold: {m_TotalGold}, Total XP: {m_TotalXp + m_TempXp}, Keys: {m_TempKeys}");

            m_TotalXp += m_TempXp;
            m_TempXp = 0f;
            SaveManager.Instance.XP = m_TotalXp;
            UpdateGoldUI();
        }

        void OnLose()
        {
            m_TempCoins = 0;
            m_TotalXp += m_TempXp;
            m_TempXp = 0f;
            SaveManager.Instance.XP = m_TotalXp;
            Debug.Log($"Level Failed: Reset Temp Coins and XP, Total XP: {m_TotalXp}");
            UpdateGoldUI();
        }

        void Update()
        {
            if (m_Hud.gameObject.activeSelf)
            {
                m_TempXp += PlayerController.Instance.Speed * Time.deltaTime;
                m_Hud.XpValue = m_TempXp;
                Debug.Log($"Updating XP: {m_TempXp}");

                if (SequenceManager.Instance.m_CurrentLevel is LoadLevelFromDef loadLevelFromDef)
                {
                    m_Hud.XpSlider.minValue = 0;
                    m_Hud.XpSlider.maxValue = loadLevelFromDef.m_LevelDefinition.LevelLength;
                }
            }
        }

        void UpdateGoldUI()
        {
            if (goldValueText != null)
            {
                goldValueText.text = m_TotalGold.ToString();
                Debug.Log($"Updated Gold UI: {m_TotalGold}");
            }
        }
    }
}
