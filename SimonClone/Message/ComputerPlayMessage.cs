using SimonClone.Enum;

namespace SimonClone.Message
{
    public class ComputerPlayMessage
    {
        public SimonButton Color { get; private set; }

        public ComputerPlayMessage(SimonButton color)
        {
            Color = color;
        }
    }
}
