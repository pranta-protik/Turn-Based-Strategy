using System.Collections.Generic;
using System.Linq;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;
    private List<Unit> _unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
        _unitList = new List<Unit>();
    }

    public override string ToString()
    {
        var unitString = _unitList.Aggregate("", (current, unit) => current + (unit + "\n"));
        return _gridPosition + "\n" + unitString;
    }

    public void AddUnit(Unit unit)
    {
        _unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return _unitList;
    }

    public bool HasAnyUnit()
    {
        return _unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return _unitList[0];
        }

        return null;
    }
}
