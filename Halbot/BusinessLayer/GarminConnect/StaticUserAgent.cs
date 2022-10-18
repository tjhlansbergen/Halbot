using System;

namespace Halbot.BusinessLayer.GarminConnect
{
    public class StaticUserAgent
    {
        private readonly Random _random = new Random(DateTime.UtcNow.Millisecond);

        public string New => Generate();

        private string Generate()
        {
            var minor = _random.Next(1000, 9999);
            return
                $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.{minor}.45 Safari/537.36";
        }
    }
}
