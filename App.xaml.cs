
namespace PM2E18083
{
    public partial class App : Application
    { 
        static Controllers.Locaciones dbpersons;

    public static Controllers.Locaciones DataBase
    {
        get
        {
            if (dbpersons == null)
            {
                dbpersons = new Controllers.Locaciones();
            }
            return dbpersons;
        }
    }
    
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

