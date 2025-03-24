using System.Diagnostics;
using System.Security.Cryptography;

namespace blazing_fast_otp_generator
{
    internal class Program
    {
        static void Main()
        {
            Benchmark(GenerateOTP, 4, "4-digit OTP");
            Benchmark(GenerateOTP, 6, "6-digit OTP");
        }

        static int GenerateOTP(int length)
        {
            if (length == 4) return GenerateSecureRandomNumber(1000, 9999);
            if (length == 6) return GenerateSecureRandomNumber(100000, 999999);

            throw new ArgumentException("OTP length must be 4 or 6 digits.");
        }

        static int GenerateSecureRandomNumber(int min, int max)
        {
            return RandomNumberGenerator.GetInt32(min, max + 1);
        }

        static void Benchmark(Func<int, int> otpGenerator, int length, string testName)
        {
            int iterations = 1_000_000_000;
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                otpGenerator(length);
            }

            stopwatch.Stop();
            double avgTime = stopwatch.Elapsed.TotalMilliseconds / iterations;

            Console.WriteLine($"{testName} Benchmark:");
            Console.WriteLine($"Total Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
            Console.WriteLine($"Avg Time per OTP: {avgTime:F6} ms");
            Console.WriteLine("----------------------------------------");
        }
    }
}
