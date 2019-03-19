using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStarter : MonoBehaviour {

    private void Awake()
    {
        Player.Instance.FindSpawnPointFunc();
        StartCoroutine(WaitToStartLeveling());
    }

    private IEnumerator WaitToStartLeveling()
    {
        yield return new WaitForEndOfFrame();

        LevelManager.Instance.StartLevelUpProcess();
    }
}
