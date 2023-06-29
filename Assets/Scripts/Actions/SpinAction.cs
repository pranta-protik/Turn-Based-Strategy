using System;
using UnityEngine;

public class SpinAction : BaseAction
{
    
    private float _totalSpinAmount;

    private void Update()
    {
        if (!_isActive) return;

        var spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, spinAddAmount, 0f);

        _totalSpinAmount += spinAddAmount;
        if (_totalSpinAmount >= 360f)
        {
            _isActive = false;
            _onActionComplete();
        }
    }

    public void Spin(Action onActionComplete)
    {
        _onActionComplete = onActionComplete;
        _isActive = true;
        _totalSpinAmount = 0f;
    }
    
    public override string GetActionName()
    {
        return "Spin";
    }
}
