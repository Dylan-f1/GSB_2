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
    public int Id { get; set; }
    public int Id_users { get; set; }
    public string Name { get; set; }
    public string Firstname { get; set; }
    public int Age { get; set; }
    public bool Gender { get; set; }

    //ceci est le constructeur par défaut,
    //il permet de créer l'instance qui nous permettra d'accéder
    //aux méthodes et propriétés de la classe
    public Patient() { }
    //ceci est une surcharge du constructeur, elle permettra la création d'objet User
    // ceci sera instancié avec les variables passées en paramètres
    public Patient(int id,int id_users,string name, string firstname,int age, bool Gender)
    {
      this.Id = id;
      this.Id_users = id_users;
      this.Name = name;
      this.Firstname = firstname;
      this.Age = age;
      this.Gender = Gender;
    }
   }
}
