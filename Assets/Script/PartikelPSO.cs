using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    class PartikelPSO : MonoBehaviour
    {
        public double error;
        public double bestError;
        public Vector2 velocity;
        public Vector2 bestPosition;

        public PartikelPSO()
        {
        }

        public PartikelPSO(Vector2 _position, double _error, Vector2 _velocity, Vector2 _bestPosition, double _bestError)
        {
            this.transform.position = _position;
            this.error = _error;
            this.velocity = _velocity;
            this.bestPosition = _bestPosition;
            this.bestError = _bestError;
        }
    }
}
