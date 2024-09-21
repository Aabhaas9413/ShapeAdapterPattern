using Rectangle.Interface;

namespace Rectangle
{
    public class Calculate : IShape
    {
        public int CalculateArea(int a, int b)
        {
            return a * b;
        }

        public int CalculatePrameter(int a, int b)
        {
            return 2 * (a + b);
        }
    }
}
