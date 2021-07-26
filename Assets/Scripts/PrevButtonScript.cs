using UnityEngine;

public class PrevButtonScript : MonoBehaviour
{
    public delegate void PrevHandler();
    public event PrevHandler OnPrev;

    public void PrevClick()
    {
        if (OnPrev != null)
            OnPrev();
    }
}
