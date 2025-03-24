using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Adress : BaseEntity<short>
    {
        public Adress() { }

        public Adress(string street, int number, string complement, short afiliateId, Affiliate afiliate)
        {
            Street = street;
            Number = number;
            Complement = complement;
            AfiliateId = afiliateId;
            Afiliate = afiliate;    
        }

        public string Street { get; private set; } = string.Empty;
        public int Number { get; private set; } = 0;
        public string Complement { get; private set; } = string.Empty;


        public short AfiliateId { get; private set; } = 0;
        public virtual Affiliate Afiliate { get; private set; } =default!;


        public void AlterStreet(string street)
        {
            Street = street;
        }

        public void AlterNumber(int number)
        {
            Number = number;
        }

        public void AlterComplement(string complement)
        {
            Complement = complement;
        }

        public void AlterAfiliate(short afiliateId, Affiliate afiliate)
        {
            AfiliateId = afiliateId;
            Afiliate = afiliate;
        }
    }
}