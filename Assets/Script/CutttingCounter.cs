using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CutttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs
    {
        public float progressNormalize;
    }
    [SerializeField] private CuttingReceiptSO[] cuttingRecepiSOArray;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalize = (float)cuttingProgress / cuttingReceiptSO.cutitngProgressMax,
                    });

                }
            }
        }
        else
        {
            if (player.HasKitchenObject()) {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress += 1;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            Debug.Log(OnAnyCut.GetInvocationList().Length);
            CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
            {
                progressNormalize = (float)cuttingProgress / cuttingReceiptSO.cutitngProgressMax,
            });
            if (cuttingProgress >= cuttingReceiptSO.cutitngProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutPutForInPut(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    private bool HasRecipeWithInput (KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        if (cuttingReceiptSO != null) { 
            return true;
        }
        return false;
    }
    private KitchenObjectSO GetOutPutForInPut(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingReceiptSO cuttingReceiptSO = GetCuttingReceiptSOWithInput(inputKitchenObjectSO);
        if (cuttingReceiptSO != null)
        {
            return cuttingReceiptSO.output;
        }
        return null;
    }
    private CuttingReceiptSO GetCuttingReceiptSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingReceiptSO cuttingReceiptSO in cuttingRecepiSOArray)
        {
            if (cuttingReceiptSO.input == inputKitchenObjectSO)
                return cuttingReceiptSO;
        }
        return null;
    }
}
