namespace Lab_2_2
{
    class Program
    {
        static void Main()
        {
            using (AppContext db = new())
            {
                db.SeedData();
            }
        }
    }
}