using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_2.Models
{
    public class Regime
    {
        //ceci est une propriété, elle permet d'accéder en lecture et en écriture
        // à notre attribut de classe via les méthodes get et set
        public int Id_regime { get; set; }
        public string Label { get; set; }

        //ceci est le constructeur par défaut,
        //il permet de créer l'instance qui nous permettra d'accéder
        //aux méthodes et propriétés de la classe
        public Regime() { }

        //ceci est une surcharge du constructeur, elle permettra la création d'objet Regime
        // ceci sera instancié avec les variables passées en paramètres
        public Regime(int id_regime, string label)
        {
            this.Id_regime = id_regime;
            this.Label = label;
        }
    }
}
