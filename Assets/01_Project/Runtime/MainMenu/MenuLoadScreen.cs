using UnityEngine;
using UnityEngine.UI;

public class MenuLoadScreen : MonoBehaviour
{
    public Slider loadSlider;
    private AsyncOperation async;// SceneManager.LoadSceneAsync()這個方法的返回值類型是AsyncOperation
    private uint nowprocess = 0;// 當前進度，控制Slider的百分比  
    // 定義一個協程  
    public void LoadLevel(int sceneIndex)
    {
        CentralData.GetInst();
        //指定要讀取的場景 
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        //設定讀取完後不能自動跳場景
        async.allowSceneActivation = false;
    }

    void Update()
    {
        if (async == null)
        {
            return;
        }
        // 進度條需要到達的進度值  
        uint toProcess;
        // async.progress 你正在讀取的場景的進度值  0到0.9  
        // 如果當前的進度小於0.9，說明它還沒有讀取完成，就說明進度條還需要移動  
        // 如果，場景的數據加載完畢，async.progress 的值就會等於0.9  
        if (async.progress < 0.9f)
        {
            //  進度值  
            toProcess = (uint)(async.progress * 100);
        }
        // 如果能執行到這個else，說明已經加載完畢  
        else
        {
            // 手動設定進度值為100  
            toProcess = 100;
        }
        // 如果Slider的進度小於當前讀取場景的方法返回的進度  
        if (nowprocess < toProcess)
        {
            // 當前滑動條的進度加一  
            nowprocess++;
        }
        // 設定Slider.value  
        loadSlider.value = nowprocess / 100f;

        // 如果滑動條的值等於100，說明加載完畢  
        if (nowprocess == 100)
        {
            // 設定為true的時候，如果場景數據加載完畢，就可以自動跳轉場景  
            async.allowSceneActivation = true;
        }
    }
}
