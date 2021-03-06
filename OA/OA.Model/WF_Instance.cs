//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace OA.Model
{
    using System;
    using System.Collections.Generic;
    
    
     using Newtonsoft.Json;
    public partial class WF_Instance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        
    	public WF_Instance()
        {
            this.WF_StepInfo = new HashSet<WF_StepInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<int> Info_Instance { get; set; }
        public string InstanceName { get; set; }
        public Nullable<System.DateTime> SubTime { get; set; }
        public string StartedBy { get; set; }
        public string Level { get; set; }
        public string SubForm { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public Nullable<int> WF_TempID { get; set; }
        public Nullable<int> ApplicationId { get; set; }
        public int Temp_Instance { get; set; }
    
        public virtual WF_Temp WF_Temp { get; set; }
    	  [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WF_StepInfo> WF_StepInfo { get; set; }
    }
}
