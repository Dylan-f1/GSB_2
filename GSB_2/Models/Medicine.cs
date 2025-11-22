using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class Medicine
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Dosage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Molecule { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Medicine(int v) { }
        //ceci est une surcharge du constructeur, elle permettra la création d'objet User
        // ceci sera instancié avec les variables passées en paramètres
        public Medicine(int id_medicine, int id_users, string dosage, string name, string description, string molecule)
        {
            this.Id = id_medicine;
            this.IdUser = id_users;
            this.Dosage = dosage;
            this.Name = name;
            this.Description = description;
            this.Molecule = molecule;
        }
    }
}
