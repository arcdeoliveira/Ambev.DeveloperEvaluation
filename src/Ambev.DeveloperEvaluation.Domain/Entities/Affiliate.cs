using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Affiliate : BaseEntity<short>
    {
        public Affiliate() { }   

        public Affiliate(string name , string description, string cnpj, Adress adress) 
        {
            Name = name;
            Description = description;
            Cnpj = cnpj;    
            Adress = adress;    
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;  

        public virtual Adress Adress { get; private set; } = default!;


        public void AlterName(string name)
        {
            Name = name;         
        }

        public void AlterDescription(string description)
        {
            Description = description;
        }

        public void AlterCnpj(string cnpj)
        {
            Cnpj = cnpj;
        }   

        public void AlterAdress(Adress adress)
        {
            Adress = adress;
        }
    }
}
