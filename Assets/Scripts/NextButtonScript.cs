using UnityEngine;

public class NextButtonScript : MonoBehaviour
{
    public delegate void NextHandler();
    public event NextHandler OnNext;

    public void NextClick()
    {
        if (OnNext != null)
            OnNext();
    }
}
