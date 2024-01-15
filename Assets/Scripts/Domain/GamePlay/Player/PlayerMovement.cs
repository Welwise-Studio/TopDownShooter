using UnityEngine;

namespace Domain.GamePlay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private Vector3 _velocity;

        private void FixedUpdate()
        {
            _rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime);
        }

        public void Move(Vector3 velocity)
        {
            _velocity = velocity;
        }
        public void LookAt(Vector3 lookPoint)
        {
            Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            transform.LookAt(heightCorrectedPoint);
        }
    }
}
