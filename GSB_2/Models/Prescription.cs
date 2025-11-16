using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;

namespace GSB_2.Models
{
    public class Prescription
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id { get; set; }
        public int Id_users { get; set; }
        public int Id_patients { get; set; }
        public int quantity { get; set; }
        public DateTime validity { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Prescription() { }
        //ceci est une surcharge du constructeur, elle permettra la création d'objet User
        // ceci sera instancié avec les variables passées en paramètres
        public Prescription(int id, int id_users, int id_patients, int quantity, DateTime validity)
        {
            this.Id = id;
            this.Id_users = id_users;
            this.Id_patients = id_patients;
            this.quantity = quantity;
            this.validity = validity;
        }
    }
}
