namespace AiXiu.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBUsers()
        {
            TBVideos = new HashSet<TBVideos>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(16)]
        public string NickName { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }

        public int? Sex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Birthday { get; set; }

        [StringLength(100)]
        public string Hobby { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreationTime { get; set; }

        [StringLength(100)]
        public string ADDress { get; set; }
        [JsonIgnore]

        public virtual TBLogins TBLogins { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBVideos> TBVideos { get; set; }
    }
}
