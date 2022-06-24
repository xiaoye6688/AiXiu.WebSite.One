namespace AiXiu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBVideos
    {
        [Key]
        [StringLength(100)]
        public string VideoId { get; set; }

        public int? UserId { get; set; }

        [StringLength(20)]
        public string Headline { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [StringLength(20)]
        public string CoverURL { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(100)]
        public string UploadTime { get; set; }

        public virtual TBUsers TBUsers { get; set; }
    }
}
