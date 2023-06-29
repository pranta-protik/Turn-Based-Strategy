using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectedGameObject;

    private BaseAction _baseAction;
    
    public void SetBaseAction(BaseAction baseAction)
    {
        _baseAction = baseAction;
        _textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    public void UpdateSelectedVisual()
    {
        var selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        
        _selectedGameObject.SetActive(selectedBaseAction == _baseAction);
    }
}
