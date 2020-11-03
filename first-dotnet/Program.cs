class Program
{
    static void Main(string[] args)
    {
        // loop over the args from command line
        for(int ii = 0; ii < args.Length; ii++)
        {
        string thisArg = args[ii];
        System.Console.WriteLine(thisArg);
        }
        
        System.Console.WriteLine("Also. Hey there world. What up.");
    }

}