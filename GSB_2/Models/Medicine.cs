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
        public string dosage { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string molecule { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Medicine(int v) { }
        //ceci est une surcharge du constructeur, elle permettra la création d'objet User
        // ceci sera instancié avec les variables passées en paramètres
        public Medicine(int id_medicine, int id_user, string dosage, string name, string description, string molecule)
        {
            this.Id = id_medicine;
            this.IdUser = id_user;
            this.dosage = dosage;
            this.name = name;
            this.description = description;
            this.molecule = molecule;
        }
    }
}
