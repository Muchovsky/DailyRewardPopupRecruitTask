using UnityEngine;

public class CheatComponent : MonoBehaviour
{
    [SerializeField] bool hideInRelease = true;

    void Awake()
    {
#if UNITY_EDITOR || TEST_BUILD
        return;
#endif
        if (hideInRelease)
        {
            gameObject.SetActive(false);
        }
    }
}