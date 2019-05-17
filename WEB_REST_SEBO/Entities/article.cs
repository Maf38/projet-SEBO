//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------



namespace WEB_REST_SEBO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public article()
        {
            this.ligne_de_commande = new HashSet<ligne_de_commande>();
            this.ligne_de_livraison = new HashSet<ligne_de_livraison>();
        }
        [DataMember]
        public string reference { get; set; }
        [DataMember]
        public string titre { get; set; }
        [DataMember]
        public string editeur { get; set; }
        [DataMember]
        public string auteur { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public double prix { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public bool reassort { get; set; }
        [DataMember]
        public int qteStock { get; set; }
        public int qteCommande { get; set; }
        public int seuil { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string NomPromo { get; set; }
    
        public virtual genre genre { get; set; }
        public virtual promo promo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ligne_de_commande> ligne_de_commande { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ligne_de_livraison> ligne_de_livraison { get; set; }
    }
}
