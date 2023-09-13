namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabaseType="databasetype";
        
        public DatabaseTypeEnum DatabaseType;

        //Default olarak mssql seçili olsun 
        public DatabaseTypeEnum GetDefaultDatabaseType => DatabaseTypeEnum.SqlServer;
    }
}
