using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _labelCount;
    [SerializeField] private int _enemyReward;
    [SerializeField] private int _bossReward;

    public void CountMoney()
    {
        _labelCount.text = (_spawner.KilledCount * _enemyReward).ToString();
    }
}
