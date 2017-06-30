namespace Wacton.Japangolin.Sentences.UI.Mains
{
    using System.Collections.Generic;
    using System.Windows.Input;

    public class KonamiCode
    {
        private readonly List<Key> konamiKeys = new List<Key> { Key.Up, Key.Up, Key.Down, Key.Down, Key.Left, Key.Right, Key.Left, Key.Right, Key.B, Key.A };
        private int currentPosition = -1;

        public bool IsComplete => this.currentPosition == this.konamiKeys.Count - 1;

        public void PressKey(Key key)
        {
            if (this.IsComplete)
            {
                return;
            }

            var firstKonamiKey = this.konamiKeys[0];
            var nextKonamiKey = this.konamiKeys[this.currentPosition + 1];

            if (key == nextKonamiKey) // if key matches next in sequence, increment position
            {
                this.currentPosition++;
            }
            else if (key == firstKonamiKey) // else if key matches first in sequence, set to start of sequence
            {
                this.currentPosition = 0;
            }
            else // else, set to before sequence start
            {
                this.Reset();
            }
        }

        public void Reset()
        {
            this.currentPosition = -1;
        }
    }
}