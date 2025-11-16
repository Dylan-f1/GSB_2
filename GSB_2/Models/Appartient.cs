using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class Appartient
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id_prescription { get; set; }
        public int Id_medicine { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Appartient() { }
        //ceci est une surcharge du constructeur, elle permettra la création d'objet User
        // ceci sera instancié avec les variables passées en paramètres
        public Appartient(int id_prescription, int id_medicine)
        {
            this.Id_prescription = id_prescription;
            this.Id_medicine = id_medicine;
        }
    }
}
