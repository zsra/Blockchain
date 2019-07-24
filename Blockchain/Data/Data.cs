namespace Blockchain.Data
{
    public class Data
    {
        public string Id { get; set; }
        public Person Person { get; set; }
        public Wallet Wallet { get; set; }

        public Data(Person person, Wallet wallet)
        {
            this.Id = Cryptography.ID.GenerateId();
            this.Person = person;
            this.Wallet = wallet;
        }
    }
}
