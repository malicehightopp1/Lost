using UnityEngine;

public class SGameMode : MonoBehaviour
{
    [SerializeField] private SPlayer mPlayerGameobjectPrefab;

    SPlayer mPlayerGameObject;
    void Awake()
    {
        SPlayerStart playerStart = FindFirstObjectByType<SPlayerStart>();
        if(!playerStart)
        {
            throw new System.Exception("Neep a player start in scene for the player spawn location and rotation"); //better equivelant for debug.log more visible error
        }
        mPlayerGameObject = Instantiate(mPlayerGameobjectPrefab, playerStart.transform.position, playerStart.transform.rotation);
    }
}
 