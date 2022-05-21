using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLoader : MonoBehaviour {

	void Start () {
        StartCoroutine(LoadGameCourotine());
	}
	

    IEnumerator LoadGameCourotine()
    {
        AsyncOperation oparation = SceneManager.LoadSceneAsync("Game");

        while(!oparation.isDone)
        {
            float progress = Mathf.Clamp01(oparation.progress / 0.9f);

            yield return null;
        }
    }
}
