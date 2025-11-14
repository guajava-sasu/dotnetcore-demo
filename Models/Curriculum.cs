using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ModustaAPI.Models
{
    public class Curriculum
    {
        public Curriculum()
        {
            Id = ObjectId.GenerateNewId();
            Atouts = new List<string>();
            Certifications = new List<string>();
            Loisirs = new List<string>();
            Formation = new List<Formation>();
            Langues = new List<Langue>();
            Clients = new List<Client>();
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("contact")]
        public Contact? Contact { get; set; }

        [BsonElement("competences")]
        public Competences? Competences { get; set; }

        [BsonElement("atouts")]
        public List<string> Atouts { get; set; }

        [BsonElement("certifications")]
        public List<string> Certifications { get; set; }

        [BsonElement("loisirs")]
        public List<string> Loisirs { get; set; }

        [BsonElement("formation")]
        public List<Formation> Formation { get; set; }

        [BsonElement("langues")]
        public List<Langue> Langues { get; set; }

        [BsonElement("clients")]
        public List<Client> Clients { get; set; }

        [BsonElement("etiquette")]
        public string Etiquette { get; set; } = string.Empty;

        [BsonElement("utilisateur")]
        public string Utilisateur { get; set; } = string.Empty;

        [BsonElement("nom")]
        public string Nom { get; set; } = string.Empty;

        [BsonElement("prenom")]
        public string Prenom { get; set; } = string.Empty;

        [BsonElement("titre")]
        public string Titre { get; set; } = string.Empty;

        [BsonElement("annees_experience")]
        public int? AnneesExperience { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        [BsonElement("image")]
        public string Image { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class Contact
    {
        [BsonElement("telephone")]
        public Telephone Telephone { get; set; } = new Telephone();

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("modusta")]
        public string Modusta { get; set; } = string.Empty;
        [BsonElement("github")]
        public string Github { get; set; } = string.Empty;

        [BsonElement("linkedin")]
        public string LinkedIn { get; set; } = string.Empty;

    }

    public class Telephone
    {
        [BsonElement("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [BsonElement("fixe")]
        public string Fixe { get; set; } = string.Empty;
    }

    public class Competences
    {
        public Competences()
        {
            Outils = new List<string>();
            Langages = new List<string>();
            Framework = new List<string>();
            Methodes = new List<string>();
        }

        [BsonElement("outils")]
        public List<string> Outils { get; set; }

        [BsonElement("langages")]
        public List<string> Langages { get; set; }

        [BsonElement("framework")]
        public List<string> Framework { get; set; }

        [BsonElement("methodes")]
        public List<string> Methodes { get; set; }
    }

    public class Formation
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("annee_de_sortie")]
        public string AnneeDeSortie { get; set; } = string.Empty;

        [BsonElement("titre")]
        public string Titre { get; set; } = string.Empty;

        [BsonElement("etablissement")]
        public string Etablissement { get; set; } = string.Empty;

        [BsonElement("debut")]
        public DateTime? Debut { get; set; }

        [BsonElement("fin")]
        public DateTime? Fin { get; set; }

        [BsonElement("formationSuivie")]
        public string FormationSuivie { get; set; } = string.Empty;

        [BsonElement("activiteClub")]
        public string ActiviteClub { get; set; } = string.Empty;

        [BsonElement("niveau")]
        public string Niveau { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class Langue
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("nom")]
        public string Nom { get; set; } = string.Empty;

        [BsonElement("langue")]
        public string LangueNom { get; set; } = string.Empty;

        [BsonElement("niveau")]
        public string Niveau { get; set; } = string.Empty;
    }

    public class Equipe
    {
        public Equipe()
        {
          /*  Titre = string.Empty;
            Effectif = 0;
            Role = string.Empty;*/
        }

        [BsonElement("titre")]
        public string? Titre { get; set; }

        [BsonElement("effectif")]
        public string? Effectif { get; set; }
        [BsonElement("nombre")]
        public int? Nombre { get; set; }
        //[BsonElement("role")]
        //public string? Role { get; set; }

        [BsonElement("moi")]
        public bool? Moi { get; set; }
    }

    public class Client
    {
        public Client()
        {
            Entreprise = string.Empty;
            Poste = string.Empty;
            Projet = string.Empty;
            Equipe = new List<Equipe>();
            Mission = new List<string>();
            Environnement = new Environnement();
        }

        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("entreprise")]
        public string Entreprise { get; set; }

        [BsonElement("debut")]
        public DateTime? Debut { get; set; }

        [BsonElement("fin")]
        public DateTime? Fin { get; set; }

        [BsonElement("poste")]
        public string Poste { get; set; }

        [BsonElement("projet")]
        public string Projet { get; set; }

        [BsonElement("equipe")]
        public List<Equipe>? Equipe { get; set; }

        [BsonElement("mission")]
        public List<string> Mission { get; set; }

        [BsonElement("environnement")]
        public Environnement Environnement { get; set; }
    }

    public class Environnement
    {
        public Environnement()
        {
            Bdds = new List<string>();
            Languages = new List<string>();
            Logiciels = new List<string>();
            Framework = new List<string>();
            Ide = new List<string>();
            Autre = new List<string>();
        }

        [BsonElement("bdds")]
        public List<string> Bdds { get; set; }

        [BsonElement("languages")]
        public List<string> Languages { get; set; }

        [BsonElement("logiciels")]
        public List<string> Logiciels { get; set; }

        [BsonElement("framework")]
        public List<string> Framework { get; set; }

        [BsonElement("ide")]
        public List<string> Ide { get; set; }

        [BsonElement("autre")]
        public List<string> Autre { get; set; }
    }
}
