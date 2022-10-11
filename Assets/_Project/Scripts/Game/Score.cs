using System;

namespace Suli.Bumble
{
    public class Score
    {
        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnScoreChanged?.Invoke(_count);
            }
        }
        public event Action<int> OnScoreChanged; 
        public Score()
        {
            _count = 0;
        }
    }
}