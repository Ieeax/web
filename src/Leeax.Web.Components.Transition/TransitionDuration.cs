namespace Leeax.Web.Components.Presentation
{
    public readonly struct TransitionDuration
    {
        public TransitionDuration(int duration)
            : this(duration, duration)
        {
        }

        public TransitionDuration(int durationEnter, int durationLeave)
        {
            DurationEnter = durationEnter;
            DurationLeave = durationLeave;
        }

        public int DurationEnter { get; }

        public int DurationLeave { get; }

        public static implicit operator TransitionDuration(int value) => new TransitionDuration(value);
        public static implicit operator TransitionDuration((int, int) value) => new TransitionDuration(value.Item1, value.Item2);
    }
}