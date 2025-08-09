using UnityEngine;

public class StoveBurningFlashUi : MonoBehaviour
{
    private const string IS_FlASHING = "IsFlashing"; 
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FlASHING, false);
    }
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.IsFried() && e.progressNormalize >= burnShowProgressAmount;
        animator.SetBool(IS_FlASHING, show);
    }
    
}
