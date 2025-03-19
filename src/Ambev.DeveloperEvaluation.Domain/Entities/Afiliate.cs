using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Afiliate : BasePostgreEntity<short>
    {
        public Afiliate() { }   

        public Afiliate(string name , string description, int cnpj, Adress adress) 
        {
            Name = name;
            Description = description;
            Cnpj = cnpj;    
            Adress = adress;    
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int Cnpj { get; private set; } = 0;  

        public virtual Adress Adress { get; private set; } = default!;


        public void AlterName(string name)
        {
            Name = name;         
        }

        public void AlterDescription(string description)
        {
            Description = description;
        }

        public void AlterCnpj(int cnpj)
        {
            Cnpj = cnpj;
        }   

        public void AlterAdress(Adress adress)
        {
            Adress = adress;
        }
    }
}
