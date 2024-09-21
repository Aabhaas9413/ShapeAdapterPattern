using Rectangle.Interface;

namespace Square
{
    public class Calculate : IShape
    {
        public int CalculateArea(int a, int b)
        {
            return a*a;
        }

        public int CalculatePrameter(int a, int b)
        {
            return 4*a;
        }
    }
}
