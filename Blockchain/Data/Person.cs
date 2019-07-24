using System;

namespace Blockchain.Data
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }

        public Person(string name, string adress, DateTime b_date)
        {
            this.Id = Cryptography.ID.GenerateId();
            this.Name = name;
            this.Address = adress;
            this.Birthdate = b_date;
        }
    }
}
