using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoadScreen : MonoBehaviour
{
    public Slider loadSlider;
    public Image loadIcon;
    public void LoadLevel(int sceneIndex)
    {
        CentralData.GetInst();
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            loadIcon.rectTransform.Rotate(Vector3.forward * Time.unscaledDeltaTime);
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
