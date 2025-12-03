using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class Prescription
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id_prescription { get; set; }
        public int Id_user { get; set; }
        public int Id_patient { get; set; }
        public DateTime Validity { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Prescription() { }

        //ceci est une surcharge du constructeur, elle permettra la création d'objet Prescription
        // ceci sera instancié avec les variables passées en paramètres
        public Prescription(int id_prescription, int id_user, int id_patient, DateTime validity)
        {
            this.Id_prescription = id_prescription;
            this.Id_user = id_user;
            this.Id_patient = id_patient;
            this.Validity = validity;
        }
    }
}