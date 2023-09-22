using UnityEngine;
using Firebase.Extensions;

public class FireBaseRemote : MonoBehaviour
{
    public static bool initialized;
    private void Awake()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (initialized)
            {
                return;
            }
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result == Firebase.DependencyStatus.Available)
                {
                    initialized = true;
                }
                else
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + task.Result);
                }
            });
        }
    }

}