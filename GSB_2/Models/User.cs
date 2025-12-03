using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class User
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id_user { get; set; }
        public string Firstname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public User() { }

        //ceci est une surcharge du constructeur, elle permettra la création d'objet User
        // ceci sera instancié avec les variables passées en paramètres
        public User(int id_user, string firstname, string name, string email, string password, bool role)
        {
            this.Id_user = id_user;
            this.Firstname = firstname;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
    }
}