using MECAI.Tests;
using MECAI.Volatilities;

namespace MECAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.SimpleImpliedVolatilityTest();

            double riskFreeRate = 0.132343187;
            string ammOptionsFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols\AMMOptions2016-04-04_56771925.csv";
            string underlyingsMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols\bbas3_underlying_book.csv";
            string optionsMarketDataFile = "";
            double daysToMaturity = 0;
            double volGuess = 0.00;
            string outputFile = "";
            VolSurface volSurface = new VolSurface();

            if (true)
            {
                optionsMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols\bbas3_april_book.csv";
                daysToMaturity = 11;
                volGuess = 0.70;
                outputFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols2\bbas3_april_vols.csv";
                //test.CalculateImpliedVolatilityToFile(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, outputFile, daysToMaturity, riskFreeRate, volGuess);
                test.CalculateImpliedVolatility(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, daysToMaturity, riskFreeRate, ref volSurface);
            }

            if (true)
            {
                optionsMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols\bbas3_may_book.csv";
                daysToMaturity = 30;
                volGuess = 0.70;
                outputFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols2\bbas3_may_vols.csv";
                //test.CalculateImpliedVolatilityToFile(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, outputFile, daysToMaturity, riskFreeRate, volGuess);
                test.CalculateImpliedVolatility(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, daysToMaturity, riskFreeRate, ref volSurface);
            }

            if (true)
            {
                optionsMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols\bbas3_june_book.csv";
                daysToMaturity = 54;
                volGuess = 0.70;
                outputFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols2\bbas3_june_vols.csv";
                //test.CalculateImpliedVolatilityToFile(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, outputFile, daysToMaturity, riskFreeRate, volGuess);
                test.CalculateImpliedVolatility(ammOptionsFile, underlyingsMarketDataFile, optionsMarketDataFile, daysToMaturity, riskFreeRate, ref volSurface);
            }

            return;
        }
    }
}
