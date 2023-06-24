using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private int _maxMoveDistance = 4;
    
    private Vector3 _targetPosition;
    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _targetPosition = transform.position;
    }

    private void Update()
    {
        const float stoppingDistance = 0.1f;

        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            const float moveSpeed = 4f;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);

            const float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

            _unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            _unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(GridPosition gridPosition)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        var validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    
    public List<GridPosition> GetValidActionGridPositionList()
    {
        var validGridPositionList = new List<GridPosition>();

        var unitGridPosition = _unit.GetGridPosition();
        
        for (var x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (var z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x, z);
                var testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }
}