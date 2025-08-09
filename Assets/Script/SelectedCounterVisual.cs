using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter clearCounter;
    [SerializeField] private GameObject[] visualGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Instance_OnSelectedCounterChange;
    }
    private void Instance_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObject)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (var visualGameObject in visualGameObject)
            visualGameObject.SetActive(false);
    }
}
