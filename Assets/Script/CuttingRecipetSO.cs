using UnityEngine;
[CreateAssetMenu()]
public class CuttingReceiptSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cutitngProgressMax;
}
