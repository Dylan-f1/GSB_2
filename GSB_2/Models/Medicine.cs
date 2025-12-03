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
        public int Id_medicine { get; set; }
        public int Id_user { get; set; }
        public int Dosage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Molecule { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Medicine() { }

        //ceci est une surcharge du constructeur, elle permettra la création d'objet Medicine
        // ceci sera instancié avec les variables passées en paramètres
        public Medicine(int id_medicine, int id_user, int dosage, string name, string description, string molecule)
        {
            this.Id_medicine = id_medicine;
            this.Id_user = id_user;
            this.Dosage = dosage;
            this.Name = name;
            this.Description = description;
            this.Molecule = molecule;
        }
    }
}