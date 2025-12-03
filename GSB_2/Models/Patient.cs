using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class Patient
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id_patient { get; set; }
        public int Id_user { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Patient() { }

        //ceci est une surcharge du constructeur, elle permettra la création d'objet Patient
        // ceci sera instancié avec les variables passées en paramètres
        public Patient(int id_patient, int id_user, string name, string firstname, int age, string gender)
        {
            this.Id_patient = id_patient;
            this.Id_user = id_user;
            this.Name = name;
            this.Firstname = firstname;
            this.Age = age;
            this.Gender = gender;
        }
    }
}