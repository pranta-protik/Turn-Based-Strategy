using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;
    
    private Vector3 _targetPosition;

    private void Awake()
    {
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

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
