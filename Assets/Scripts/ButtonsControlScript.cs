using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsControlScript : MonoBehaviour
{
    [SerializeField] GameObject stockNewsPanel;
    [SerializeField] Image closeButtonImage, prevButtonImage, nextButtonImage;
    [SerializeField] TextMeshProUGUI nowText;

    public void ClosePanel()
    {
        StartCoroutine(UnloadUnusedAssets());
    }

    IEnumerator UnloadUnusedAssets()
    {
        for (int i = 0; i < stockNewsPanel.transform.childCount; i++)
            Destroy(stockNewsPanel.transform.GetChild(i).gameObject);

        var unloader = Resources.UnloadUnusedAssets();
        while(!unloader.isDone)
            yield return null;

        stockNewsPanel.SetActive(false);
        closeButtonImage.enabled = false;
        prevButtonImage.enabled = false;
        nextButtonImage.enabled = false;
        nowText.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
