namespace DigitalLibrary.Data.Logic
{
    public static class PercentageCalculator
    {
        public static double CalculatePersentage(int firstNumber, int secondNumber)
        {
            double allUploads = (double)firstNumber + (double)secondNumber;
            if (allUploads > 0)
            {
                double rating = ((double)secondNumber / allUploads) * 100;
                return rating;
            }

            return 0;
        }
    }
}
