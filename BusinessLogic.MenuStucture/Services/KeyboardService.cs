using System.Linq;

namespace BusinessLogic.MenuStucture.Services
{
    class KeyboardService
    {
        public string[][] GetRangeButtonsArray(string[] array, int range)
        {
            int count = array.Length / range + 1;

            return Enumerable.Range(0, count).
                Select(i => array.Skip(i * range)
                    .Take(range)
                    .ToArray()).
                ToArray();
        }
    }
}
