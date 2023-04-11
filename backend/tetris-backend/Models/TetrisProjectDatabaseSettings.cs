namespace tetris_backend.Models
{
    public class TetrisProjectDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;

        public string ShopItemCollectionName { get; set; } = null!;

        public string VerificationCollectionName { get; set; } = null!;
    }
}
