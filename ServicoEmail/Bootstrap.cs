namespace ServicoEmail
{
    class Bootstrap
    {
        public void Start()
        {
            Database database = new Database();
            database.ConnectionDatabase();
        }

        public void Stop()
        {
            //dispose...
        }
    }
}
