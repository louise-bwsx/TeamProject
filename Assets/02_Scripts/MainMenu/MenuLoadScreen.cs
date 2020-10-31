using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoadScreen : MonoBehaviour
{
    public Slider loadSlider;
    //public Image loadIcon;

    //private void Update()
    //{

    //    loadIcon.rectTransform.Rotate(Vector3.forward * Time.maximumDeltaTime * 20);
    //}
    public void LoadLevel(int sceneIndex)
    {
        CentralData.GetInst();
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        //最後會轉
        //Time.deltaTime;
        //Time.fixedDeltaTime
        //Time.fixedUnscaledDeltaTime
        //Time.frameCount
        //不會動
        //Time.captureDeltaTime
        //Time.captureFramerate
        //loadIcon.rectTransform.Rotate(Vector3.forward * Time. * 20);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
