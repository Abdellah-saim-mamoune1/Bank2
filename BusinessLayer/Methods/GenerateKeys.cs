using bankApI.Data;

namespace bankApI.BusinessLayer.Methods
{
    public static  class GenerateKeys
    {
        private static readonly Random _random = new Random();

       
        public static string GenerateId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
      

        public static string GenerateNumberId(int length)
        {
            const string chars = "0123456789";
            var r = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
            return r;
        }


      

    }
}
