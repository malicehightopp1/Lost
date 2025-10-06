using UnityEngine;

public class SGameMode : MonoBehaviour
{
    [SerializeField] private SPlayer mPlayerGameobjectPrefab;

    SPlayer mPlayerGameObject;
    public SPlayer mplayer => mPlayerGameObject;
    public static SGameMode mMainGameMode;
    private void OnDestroy()
    {
        if (mMainGameMode == this) //making persistant through levels
        {
            mMainGameMode = null;
        }
    }
    void Awake()
    {
        if(mMainGameMode != null) //making sure we only have one gamemode 
        {
            Destroy(gameObject);
        }

        mMainGameMode = this;

        SPlayerStart playerStart = FindFirstObjectByType<SPlayerStart>();
        if(!playerStart)
        {
            throw new System.Exception("Neep a player start in scene for the player spawn location and rotation"); //better equivelant for debug.log more visible error
        }
        mPlayerGameObject = Instantiate(mPlayerGameobjectPrefab, playerStart.transform.position, playerStart.transform.rotation);
    }
}
 