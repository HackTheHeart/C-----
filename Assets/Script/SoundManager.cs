using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUMES = "SoundEffectVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipSO audioClipSO;
    private float volume = 1.0f;
    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUMES, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        CutttingCounter.OnAnyCut += CutttingCounter_OnAnyCut;
        Player.Instance.OnPickSomething += Instance_OnPickSomething;
        BaseCounter.OnAnyObjectDrop += BaseCounter_OnAnyObjectDrop;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectDrop(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipSO.ObjectPickUp, Player.Instance.transform.position);
    }

    private void CutttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CutttingCounter cutttingCounter = sender as CutttingCounter;
        PlaySound(audioClipSO.chop, cutttingCounter.transform.position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.instance;
        PlaySound(audioClipSO.deliveryFail, deliveryCounter.transform.position);
    }
    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.instance;
        PlaySound(audioClipSO.deliverySuccess, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)],position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
    public void PlayFootStepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipSO.footstep, position, volume);
    }
    public void PlayCountDownSound()
    {
        PlaySound(audioClipSO.warning, Vector3.zero);
    }
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipSO.warning, position);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUMES, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}

