using System.Collections;
using UnityEngine;
// using OddBitOut.DevResources;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class StockNews : MonoBehaviour
{
    TextAsset jsonTextAsset;
    int startingCell = 0;
    CellsList cells = new CellsList();

    CloseButton closeButton;
    ArrowButton arrowButton;

    [SerializeField]
    GameObject cellPrefab;
    [SerializeField]
    Image closeButtonImage, prevButtonImage, nextButtonImage;

    public PrevButtonScript prevButtonScript;
    public NextButtonScript nextButtonScript;
    public SwipeScript swipeScript;

    void Start()
    {
        LoadAssets();
        prevButtonScript.OnPrev += OnPrev;
        nextButtonScript.OnNext += OnNext;
        swipeScript.OnSwipe += OnSwipe;
    }

    void OnSwipe(float deltaX)
    {
        if (deltaX < 0f)
            OnNext();
        else if (deltaX > 0f)
            OnPrev();
    }

    void OnPrev()
    {
        if (startingCell - 6 >= 0)
            startingCell -= 6;
        else
            startingCell = 0;
        StopAllCoroutines();
        StartCoroutine(LoadCells(startingCell));
    }

    void OnNext()
    {
        if (cells.items.Count > startingCell + 6)
        {
            startingCell += 6;
            StopAllCoroutines();
            StartCoroutine(LoadCells(startingCell));
        }
    }

    void LoadAssets()
    {
        LoadJSON("PlayMe8.json");
        ReadJSON();
        StartCoroutine(LoadCells(startingCell));
    }

    IEnumerator LoadCells(int startingCell)
    {
        CellContent cell;
        GameObject cellObject;

        closeButtonImage.sprite = Resources.Load<Sprite>("Buttons/" + closeButton.closeButton);
        prevButtonImage.sprite = Resources.Load<Sprite>("Buttons/" + arrowButton.arrowButton);
        nextButtonImage.sprite = Resources.Load<Sprite>("Buttons/" + arrowButton.arrowButton);

        for (int i = startingCell, idx = 0; i < startingCell + 6; i++, idx++)
        {
            if (i < cells.items.Count)
                cell = cells.items[i];
            else
                cell = null;
            if (gameObject.transform.childCount < 6)
            {
                cellObject = Instantiate(cellPrefab);
                cellObject.transform.SetParent(gameObject.transform);
                cellObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
                cellObject = gameObject.transform.GetChild(idx).gameObject;

            Image productImage = cellObject.transform.Find("Product Image").GetComponent<Image>();
            TextMeshProUGUI descriptionText = productImage.transform.Find("Description Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI qtyText = productImage.transform.Find("Qty Text").GetComponent<TextMeshProUGUI>();
            Image coinImage = productImage.transform.Find("Coin Image").GetComponent<Image>();
            TextMeshProUGUI priceText = coinImage.transform.Find("Price Text").GetComponent<TextMeshProUGUI>();
            RawImage userImage = cellObject.transform.Find("User Image").GetComponent<RawImage>();
            Image starImage = userImage.transform.Find("Star Image").GetComponent<Image>();
            TextMeshProUGUI ratingText = starImage.transform.Find("Rating Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI userNameText = userImage.transform.Find("User Text").GetComponent<TextMeshProUGUI>();

            if (i < cells.items.Count)
            {
                productImage.sprite = Resources.Load<Sprite>("Products/" + cell.sprite);
                descriptionText.SetText(cell.description);
                qtyText.SetText("x" + cell.qty.ToString());
                coinImage.sprite = Resources.Load<Sprite>("Money/Money1");
                priceText.text = cell.price.ToString();
                StartCoroutine(DownloadImage(cell.playerImage, userImage));
                starImage.sprite = Resources.Load<Sprite>("Money/Star");
                ratingText.text = cell.playerRating.ToString();
                userNameText.SetText(cell.playerName);
            }
            else
            {
                productImage.sprite = null;
                descriptionText.SetText("");
                qtyText.SetText("");
                coinImage.sprite = null;
                priceText.text = "";
                userImage.texture = null;
                starImage.sprite = null;
                ratingText.text = "";
                userNameText.SetText("");
            }

            yield return null;
        }
    }

    IEnumerator DownloadImage(string url, RawImage image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.LogError(request.error);
        else
            image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;

    }

    void LoadJSON(string path)
    {
        string filePath = "JSON/" + path.Replace(".json", "");
        jsonTextAsset = Resources.Load<TextAsset>(filePath);
    }

    void ReadJSON()
    {
        cells = JsonUtility.FromJson<CellsList>(jsonTextAsset.text);
        closeButton = JsonUtility.FromJson<CloseButton>(jsonTextAsset.text);
        arrowButton = JsonUtility.FromJson<ArrowButton>(jsonTextAsset.text);
    }
}
