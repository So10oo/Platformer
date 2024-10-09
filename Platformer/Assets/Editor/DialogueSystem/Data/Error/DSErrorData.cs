using UnityEngine;

namespace DS.Data.Error
{
    public class DSErrorData
    {
        public Color Color { get; set; }

        public DSErrorData()
        {
            GenerateRandomColor();
        }

        private void GenerateRandomColor()
        {
            Color = Color.HSVToRGB(Random.value, 1, 1);
        }
    }
}