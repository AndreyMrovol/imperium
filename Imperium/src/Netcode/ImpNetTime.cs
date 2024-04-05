#region

using Unity.Netcode;

#endregion

namespace Imperium.Netcode;

// ReSharper disable MemberCanBeMadeStatic.Global
// This is a network behaviour so the members have to not be static
public class ImpNetTime : NetworkBehaviour
{
    internal static ImpNetTime Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    internal void RequestTimeServerRpc()
    {
        SyncTimeClientRpc(Imperium.GameManager.TimeSpeed.Value, Imperium.GameManager.TimeIsPaused.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    internal void UpdateTimeServerRpc(float timeSpeed, bool isPaused)
    {
        SyncTimeClientRpc(timeSpeed, isPaused);
    }

    [ClientRpc]
    private void SyncTimeClientRpc(float timeSpeed, bool isPaused)
    {
        Imperium.GameManager.TimeSpeed.Set(timeSpeed, skipSync: true);
        Imperium.GameManager.TimeIsPaused.Set(isPaused, skipSync: true);
    }
}