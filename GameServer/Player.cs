using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public float inputHorizontal;
        public float inputVertical;
        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
        }

        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;

            _inputDirection.X -= inputHorizontal;
            _inputDirection.Y += inputVertical;

            Move(_inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            Vector3 _forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0, 1, 0)));

            Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
            position += _moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        public void SetInput(float _inputHorizontal, float _inputVertical, Quaternion _rotation)
        {
            inputHorizontal = _inputHorizontal;
            inputVertical = _inputVertical;
            rotation = _rotation;
        }
    }
}
