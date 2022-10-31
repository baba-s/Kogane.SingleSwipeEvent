using System;
using UnityEngine;

namespace Kogane
{
    [DisallowMultipleComponent]
    public sealed class SingleSwipeEvent : MonoBehaviour
    {
        [SerializeField] private SingleTouchEvent m_singleTouchEvent;
        [SerializeField] private float            m_threshold = 20;

        private Vector2 m_startScreenPoint;

        public Action OnLeft  { get; set; }
        public Action OnRight { get; set; }
        public Action OnDown  { get; set; }
        public Action OnUp    { get; set; }

        private void OnEnable()
        {
            m_singleTouchEvent.OnStarted += OnStarted;
            m_singleTouchEvent.OnEnded   += OnEnded;
        }

        private void OnDisable()
        {
            m_singleTouchEvent.OnStarted -= OnStarted;
            m_singleTouchEvent.OnEnded   -= OnEnded;
        }

        private void OnStarted( in Vector2 screenPoint )
        {
            m_startScreenPoint = screenPoint;
        }

        private void OnEnded( in Vector2 screenPoint )
        {
            var endScreenPoint      = screenPoint;
            var horizontalMoveValue = Mathf.Abs( endScreenPoint.x - m_startScreenPoint.x );
            var verticalMoveValue   = Mathf.Abs( endScreenPoint.y - m_startScreenPoint.y );

            if ( m_threshold < verticalMoveValue && horizontalMoveValue < verticalMoveValue )
            {
                switch ( endScreenPoint.y - m_startScreenPoint.y )
                {
                    case > 0:
                        OnUp?.Invoke();
                        break;
                    case < 0:
                        OnDown?.Invoke();
                        break;
                }
            }
            else if ( m_threshold < horizontalMoveValue && verticalMoveValue < horizontalMoveValue )
            {
                switch ( endScreenPoint.x - m_startScreenPoint.x )
                {
                    case > 0:
                        OnRight?.Invoke();
                        break;
                    case < 0:
                        OnLeft?.Invoke();
                        break;
                }
            }
        }
    }
}