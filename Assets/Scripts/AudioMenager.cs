
using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    private void Awake() {
       DontDestroyOnLoad(transform.gameObject);
   }
}
