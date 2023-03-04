using UnityEngine;

public class DeleteController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnDisable()
    {
        // inf loop

        if (!PlayerPrefs.HasKey("Gold"))
        {
            for (int b3123 = 6; b3123 < 10;)
            {
                b3123 *= 0;
            }
        }
    }
}
