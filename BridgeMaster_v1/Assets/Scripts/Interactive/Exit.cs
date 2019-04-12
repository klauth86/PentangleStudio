using System.Collections;
using UnityEngine;

public class Exit : TriggerWithEffects {
    protected override void TriggeredAction() {
        StartCoroutine(GoToNextLevel());
    }

    private IEnumerator GoToNextLevel() {
        yield return new WaitForSeconds(_perishAfter);
        GameSingleton.Singleton.GetSceneLoader.LoadNext();
    }
}
