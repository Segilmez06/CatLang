using CatLang.Lang;

namespace CatLang
{
    internal class Program
    {
        public static List<Variable> vars = new();
        public static List<Part> parts = new();
        public static bool isRunning = true;
        
        static void Main(string[] args)
        {
            while (isRunning)
            {
                Console.Write("> ");
                string expression = Console.ReadLine().Trim();
                if (expression[0] != '#') // If line is not comment
                {
                    string call = expression.Split(':')[0].Trim();
                    string argumentString = "";
                    if (expression.Contains(":"))
                    {
                        argumentString = expression.Substring(call.Length + 1).Trim();
                    }
                    var method = typeof(InternalCalls).GetMethod(call);
                    if (method != null)
                    {
                        var result = (CallOut)method.Invoke(new InternalCalls(), new[] { argumentString });
                        if (result.Code != 0)
                        {
                            Exception e = (Exception)result.Output;
                            Console.WriteLine($"Command returned {result.Code}: {e.Message}");
                        }
                    }
                }
            }
        }
    }
}