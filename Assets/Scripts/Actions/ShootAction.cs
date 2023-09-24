using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    [SerializeField] private int _maxShootDistance = 7;
    
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
    
    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        var validGridPositionList = new List<GridPosition>();

        var unitGridPosition = _unit.GetGridPosition();
        
        for (var x = -_maxShootDistance; x <= _maxShootDistance; x++)
        {
            for (var z = -_maxShootDistance; z <= _maxShootDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x, z);
                var testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                var testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                if (testDistance > _maxShootDistance)
                {
                    continue;
                }
                
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                var targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == _unit.IsEnemy())
                {
                    continue;
                }
                
                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }
    
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        _onActionComplete = onActionComplete;
        _isActive = true;
        _totalSpinAmount = 0f;
    }
}
